using UnityEngine;

namespace ns
{
    ///<summary>
    /// 技能数据管理：每个对象一份（玩家/敌人各自一个），从 SkillSetConfig 初始化。
    /// 不再使用 MonoSingleton，避免敌人和主角互相抢占。
    /// </summary>
    public class SkillManager : MonoBehaviour
    {
        [Header("JobID：0=由外部手动 Init，>0=Awake 自动加载")]
        public int jobID;

        public SkillData[] skillDatas;

        /// <summary>玩家自己的技能管理器，给技能面板等 UI 用。</summary>
        public static SkillManager PlayerSkillManager { get; set; }

        private const string ConfigPathFormat = "SkillConfig/Job{0}_SkillSet";
        private bool initialized;

        private void Awake()
        {
            if (jobID > 0) InitSkill(jobID);
        }

        public void GenerateSkill(int ID)
        {
            SkillData data = PrepareSkill(ID);
            if (data != null) Generate(data);
        }

        public void Generate(SkillData data)
        {
            if (data == null) return;
            EnsureInitialized();

            StartCoroutine(CalculateCoolTime(data));
            GameObject targetPerfab = GameObjectPool.Instance.CreatObject(
                data.perfabName,
                data.perfab,
                transform.position + transform.forward * data.forwardMeter + transform.up * data.height,
                transform.rotation);
            if (targetPerfab == null) return;

            SkillDeployer skillDeployer = targetPerfab.GetComponent<SkillDeployer>();
            if (skillDeployer == null)
            {
                Debug.LogError("[SkillManager] prefab missing SkillDeployer: " + data.perfabName);
                return;
            }
            skillDeployer.SkillData = data;
            skillDeployer.DeployerSkill();

            CharacterStatus status = data.owner != null ? data.owner.GetComponent<CharacterStatus>() : null;
            if (status != null) status.sp -= data.sp;

            GameObjectPool.Instance.CollectObject(targetPerfab, data.durationTime);
        }

        private System.Collections.IEnumerator CalculateCoolTime(SkillData data)
        {
            data.remainCoolTime = data.coolTime;
            for (int i = 0; i < data.coolTime; i++)
            {
                yield return new WaitForSeconds(1);
                data.remainCoolTime--;
            }
        }

        public SkillData PrepareSkill(int ID)
        {
            EnsureInitialized();
            if (skillDatas == null) return null;

            SkillData target = skillDatas.Find<SkillData>(s => s.skillID == ID);
            if (target == null || target.remainCoolTime > 0) return null;

            CharacterStatus status = target.owner != null ? target.owner.GetComponent<CharacterStatus>() : null;
            if (status != null && status.sp - target.sp < 0) return null;
            return target;
        }

        public SkillData[] GetSkillDatas()
        {
            return skillDatas;
        }

        private void EnsureInitialized()
        {
            if (!initialized && jobID > 0) InitSkill(jobID);
        }

        /// <summary>
        /// 从 Resources/SkillConfig/Job{jobID}_SkillSet.asset 初始化技能数据。
        /// </summary>
        public void InitSkill(int jobID)
        {
            this.jobID = jobID;
            string path = string.Format(ConfigPathFormat, jobID);
            SkillSetConfig config = Resources.Load<SkillSetConfig>(path);
            if (config == null)
            {
                Debug.LogError("[SkillManager] missing skill config: Resources/" + path);
                skillDatas = new SkillData[0];
                initialized = true;
                return;
            }
            if (config.skills == null || config.skills.Length == 0)
            {
                Debug.LogError("[SkillManager] skill config is empty: " + path);
                skillDatas = new SkillData[0];
                initialized = true;
                return;
            }

            skillDatas = new SkillData[config.skills.Length];
            for (int i = 0; i < skillDatas.Length; i++)
            {
                skillDatas[i] = CreateRuntimeData(config.skills[i]);
            }
            initialized = true;
        }

        private SkillData CreateRuntimeData(SkillConfigEntry entry)
        {
            SkillData data = new SkillData();
            data.skillID = entry.skillID;
            data.skillName = entry.skillName;
            data.description = entry.description;
            data.skillIconName = entry.iconName;

            data.durationTime = entry.durationTime;
            data.attakeDelta = entry.damageInterval;
            data.attakeRatio = entry.damage;
            data.coolTime = entry.coolTime;
            data.sp = entry.costSP;
            data.attakeDistance = entry.attackDistance;
            data.attakeTags = string.IsNullOrEmpty(entry.attackTargets)
                ? new string[0]
                : entry.attackTargets.Split(',');

            data.perfabName = entry.prefabName;
            data.AttakeType = entry.attackType;
            data.skillAnimationName = entry.animationName;
            data.whenTimeCanInvokeOther = entry.whenTimeCanInvokeOther;
            data.forwardMeter = entry.forwardDis;
            data.height = entry.height;
            data.moveSpeed = entry.moveSpeed;
            data.skillImpacts = entry.skillImpacts;

            data.owner = gameObject;
            data.perfab = ResourcesManager.Load<GameObject>(entry.prefabName);
            if (data.perfab == null)
            {
                Debug.LogError("[SkillManager] missing skill prefab: " + entry.prefabName);
            }
            return data;
        }
    }
}
