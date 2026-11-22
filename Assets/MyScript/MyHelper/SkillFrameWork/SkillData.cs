using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ns
{
    ///<summary>
    ///只记录技能的数据
    ///</summary>
    public enum SelectorType
    {
        Sector,//以人物为中心 以技能数据攻击距离为半径的判定
        Fly//以飞行技能为中心 技能数据攻击距离为半径的判定
    }
    [System.Serializable]
    public class SkillData
	{
        
        /// <summary>
        /// 近战or远程攻击
        /// </summary>
        public SelectorType AttakeType { get; set; }

        /// <summary>
        /// 技能ID
        /// </summary>
        public int skillID;

        /// <summary>
        /// 技能名称
        /// </summary>
        public string skillName;

        /// <summary>
        /// 技能介绍
        /// </summary>
        public string description;

        /// <summary>
        /// 技能图标名称
        /// </summary>
        public string skillIconName;

        /// <summary>
        /// 技能持续时间
        /// </summary>
        public float durationTime;

        /// <summary>
        /// 伤害时间间隔，持续性伤害技能使用
        /// </summary>
        public float attakeDelta;

        /// <summary>
        /// 攻击比率
        /// </summary>
        public float attakeRatio;

        /// <summary>
        /// 冷却时间
        /// </summary>
        public int coolTime;

        /// <summary>
        /// 技能消耗法力值
        /// </summary>
        public int sp;

        /// <summary>
        /// 飞行物技能使用
        /// </summary>
        public float attakeDistance;

        /// <summary>
        /// 攻击目标标签
        /// </summary>
        public string[] attakeTags;

        /// <summary>
        /// 技能预制件名称
        /// </summary>
        public string perfabName;

        /// <summary>
        /// 技能动画控制参数名称
        /// </summary>
        public string skillAnimationName;


        /// <summary>
        /// 技能释放间隔，防止多次触发，要根据动画事件时机设置
        /// </summary>
        public float whenTimeCanInvokeOther;

        /// <summary>
        /// 技能初始位置在人物的正前方多少米（可负）
        /// </summary>
        public float forwardMeter;

        /// <summary>
        /// 技能初始位置高度
        /// </summary>
        public float height;


        /// <summary>
        /// 技能飞行速度
        /// </summary>
        public float moveSpeed;


        /// <summary>
        /// 释放技能的人
        /// </summary>
        public GameObject owner;

        /// <summary>
        /// 技能效果，名称必须按规定添加
        /// </summary>
        public string[] skillImpacts;





        public GameObject[] attakeTargets;//攻击目标
        
        public int remainCoolTime;//剩余冷却时间
        
        public GameObject perfab;//技能预制件



    }
}

