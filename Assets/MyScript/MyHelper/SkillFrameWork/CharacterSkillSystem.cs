using System;
using UnityEngine;

namespace ns
{
    /// <summary>
    /// 角色技能系统：
    /// 只负责准备当前技能、播放动画；真正的技能表现由动画事件 OnSkillTrigger 触发
    /// 绑定统一放在 OnEnable/OnDisable，不在 SkillAttake 里反复绑定。
    /// </summary>
    public class CharacterSkillSystem : MonoBehaviour
    {
        SkillManager skillManager;
        Animator anim;
        AnimationEvent animationEvent;
        SkillData data;

        private void Awake()
        {
            anim = GetComponent<Animator>();
            animationEvent = GetComponent<AnimationEvent>();
            skillManager = GetComponent<SkillManager>();
        }

        private void OnEnable()
        {
            // 玩家：整局只绑定一次，动画事件触发时生成当前技能
            if (animationEvent != null)
            {
                animationEvent.OnSkillTrigger += HandleSkillTrigger;
            }
        }

        private void OnDisable()
        {
            if (animationEvent != null)
            {
                animationEvent.OnSkillTrigger -= HandleSkillTrigger;
            }
        }

       

        /// <summary>
        /// 准备技能 + 播放动画，不在这里绑定动画事件。
        /// </summary>
        public SkillData SkillAttake(int skillID)
        {
            data = skillManager.PrepareSkill(skillID);
            if (data == null) return null;

            anim.SetBool(data.skillAnimationName, true);
            return data;
        }

       

        /// <summary>
        /// 玩家动画命中帧触发（动画事件方法 SkillTrigger）。
        /// </summary>
        private void HandleSkillTrigger()
        {
            if (data == null) return;

            skillManager.Generate(data);
            data = null; // 当前技能只触发一次表现，避免旧技能重复生成
        }

        

    }
}
