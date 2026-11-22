using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ns
{
	///<summary>
	///
	///</summary>
	public class UIScene_Map : UIWindow 
	{
		UIEventListener button_Battle;
        private void Awake()
        {
			button_Battle = GetUIEventListener("Button_Battle");
        }
        private void OnEnable()
        {
            button_Battle.PointClike += Button_Battle_PointClike;
        }
        private void OnDisable()
        {
            button_Battle.PointClike -= Button_Battle_PointClike;
        }

        //显示副本界面
        private void Button_Battle_PointClike(UnityEngine.EventSystems.PointerEventData obj)
        {
            AudioManager.Instance.PlaySoundArow(AudioClipsManager.Instance.buttonClip);
            UIManager.Instance.GetWindow ("UIScene_Battle").SetVisiable (true);
        }
    }
}

