using System;
using UnityEngine;

namespace ns
{
    /// <summary>
    /// 单个技能的数据（对应原数据库 T_Skill1 / T_Skill2 的一行）。
    /// 只用于编辑配置，运行时会被转换成 SkillData。
    /// </summary>
    [Serializable]
    public class SkillConfigEntry
    {
        [Header("基础信息")]
        public int skillID;
        public string skillName;
        [TextArea(2, 4)] public string description;
        public string iconName;

        [Header("数值")]
        public float durationTime;
        public float damageInterval;
        public float damage;
        public int coolTime;
        public int costSP;
        public float attackDistance;
        public string attackTargets = "Enemy";

        [Header("表现 / 释放")]
        public string prefabName;
        public SelectorType attackType = SelectorType.Fly;
        public string animationName;
        public int nextBatterId;
        public float moveSpeed;
        public float height;
        public float forwardDis;
        public float whenTimeCanInvokeOther;

        [Header("命中效果（可选，和旧逻辑的 SkillEffect 对应）")]
        public string[] skillImpacts;
    }

    /// <summary>
    /// 一个职业的技能表（2 个技能），替代 T_Skill1 / T_Skill2。
    /// 资产放在 Resources/SkillConfig/Job{JobID}_SkillSet.asset。
    /// </summary>
    [CreateAssetMenu(fileName = "JobX_SkillSet", menuName = "ARPG/Skill Set Config")]
    public class SkillSetConfig : ScriptableObject
    {
        [Tooltip("和 T_Character.JobID 对应：1=战士，2=法师")]
        public int jobID;

        public SkillConfigEntry[] skills = new SkillConfigEntry[2];
    }
}
