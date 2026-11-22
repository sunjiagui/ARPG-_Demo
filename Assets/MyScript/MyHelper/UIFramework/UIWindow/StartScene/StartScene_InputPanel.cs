using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ns
{
	///<summary>
	///输入姓名界面
	///</summary>
	public class StartScene_InputPanel : UIWindow
	{
        TMP_InputField userName;
        UIEventListener yesButton;
        UIEventListener noButton;

        private void Awake()
        {
            userName = transform.FindChildByName("InputField (TMP)").GetComponent<TMP_InputField>();
            yesButton = GetUIEventListener("Yes_Image");
            noButton = GetUIEventListener("No_Image");
        }

        private void OnEnable()
        {
            yesButton.PointClike += YesButton_PointClike;
            noButton.PointClike += NoButton_PointClike;
        }
        private void OnDisable()
        {
            yesButton.PointClike -= YesButton_PointClike;
            noButton.PointClike -= NoButton_PointClike;
        }

        /// <summary>
        /// 进入下一场景
        /// </summary>
        /// <param name="obj"></param>
        private void YesButton_PointClike(UnityEngine.EventSystems.PointerEventData obj)
        {
            if (string.IsNullOrEmpty(userName.text)) return;
            //产生按钮声音
            AudioManager.Instance.PlaySoundArow(AudioClipsManager.Instance.buttonClip, false);
            Account account = new()
            {
                AccountID = 1,
                AccountName = userName.text
            };
            
            DBConfigFactory.Instance.UpdateOne(account);


            //加载新场景
            SceneManager.LoadSceneAsync(0);
        }

        private void NoButton_PointClike(UnityEngine.EventSystems.PointerEventData obj)
        {
            //产生按钮声音
            AudioManager.Instance.PlaySoundArow(AudioClipsManager.Instance.buttonClip, false);
            //关闭显影
            SetVisiable(false);
        }
    }
}

