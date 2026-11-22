using System;
using UnityEngine;

namespace ns
{
    /// <summary>
    /// 动画事件桥：所有角色共用。
    /// 动画剪辑里只在命中帧调用 SkillTrigger()，再转发给 CharacterSkillSystem。
    /// </summary>
    public class AnimationEvent : MonoBehaviour
    {
        public event Action OnSkillTrigger;

        // 动画剪辑里的 Event Function 填 SkillTrigger
        public void SkillTrigger()
        {
            OnSkillTrigger?.Invoke();
        }
    }
}
