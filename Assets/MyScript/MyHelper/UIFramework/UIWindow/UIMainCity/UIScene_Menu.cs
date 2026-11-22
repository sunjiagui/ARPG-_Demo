using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ns
{
	///<summary>
	///
	///</summary>
	public class UIScene_Menu : UIWindow
	{
		UIEventListener skillButton;

        private void Awake()
        {
			skillButton = GetUIEventListener("Button_Skill");
        }

        private void OnEnable()
        {
            skillButton.PointClike += SkillButton_PointClike;
        }
        private void OnDisable()
        {
            skillButton.PointClike -= SkillButton_PointClike;
        }

        /// <summary>
        /// 显示技能面板
        /// </summary>
        /// <param name="obj"></param>
        private void SkillButton_PointClike(UnityEngine.EventSystems.PointerEventData obj)
        {
            AudioManager.Instance.PlaySoundArow(AudioClipsManager.Instance.buttonClip);
            UIManager.Instance.GetWindow("UIScene_SkillSelect").SetVisiable (true);
        }
    }
}

