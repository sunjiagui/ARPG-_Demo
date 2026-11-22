using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ns
{
	///<summary>
	///
	///</summary>
	public class UIScene_Battle : UIWindow 
	{
		UIEventListener closeButton;
        UIEventListener adventualButton;
        bool isLoad;
         
        private void Awake()
        {
			closeButton = GetUIEventListener("Button_Closed");
            adventualButton = GetUIEventListener("Button_Stage");
        }

        private void OnEnable()
        {
            closeButton.PointClike += CloseButton_PointClike;
            adventualButton.PointClike += AdventualButton_PointClike;
        }

        private void OnDisable()
        {
            closeButton.PointClike -= CloseButton_PointClike;
            adventualButton.PointClike -= AdventualButton_PointClike;
        }

        private void AdventualButton_PointClike(UnityEngine.EventSystems.PointerEventData obj)
        {
            if (isLoad) return;
            //¿ªÆôÕ½¶·³¡¾°
            isLoad = true;
            AudioManager.Instance.PlaySoundArow(AudioClipsManager.Instance.buttonClip);
            SceneManager.LoadSceneAsync(5);
        }
        private void CloseButton_PointClike(UnityEngine.EventSystems.PointerEventData obj)
        {
            AudioManager.Instance.PlaySoundArow(AudioClipsManager.Instance.buttonClip);
            SetVisiable(false);
        }
       
    }
}

