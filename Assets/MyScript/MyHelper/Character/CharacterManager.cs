using UnityEngine;

namespace ns
{
    /// <summary>
    /// 初始化角色模型和技能数据。玩家和敌人都可以挂 SkillManager，各自初始化。
    /// </summary>
    public class CharacterManager : MonoBehaviour
    {
        Character character;
        GameObject heroInstance;

        private void Awake()
        {
            character = DBConfigFactory.Instance.QueryDB<Character>(1);
            InitModle();
            InitCharacterSkill();
        }

        void InitCharacterSkill()
        {
            if (heroInstance == null) return;
            SkillManager manager = heroInstance.GetComponentInChildren<SkillManager>();
            if (manager == null)
            {
                Debug.LogError("[CharacterManager] hero missing SkillManager");
                return;
            }
            manager.InitSkill(character.JobID);
            SkillManager.PlayerSkillManager = manager;
        }

        void InitModle()
        {
            string heroName = "Hero_" + character.JobModel;
            GameObject heroGo = ResourcesManager.Load<GameObject>(heroName);
            if (heroGo == null)
            {
                Debug.LogError("[CharacterManager] hero prefab missing: " + heroName);
                return;
            }
            heroInstance = Instantiate(heroGo, transform.position, transform.rotation, transform);
        }
    }
}
