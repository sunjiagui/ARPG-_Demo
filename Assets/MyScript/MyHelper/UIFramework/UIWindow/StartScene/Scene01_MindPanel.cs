using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ns
{
	///<summary>
	///是否删除存档开始游戏
	///</summary>
	public class Scene01_MindPanel : UIWindow 
	{
        UIEventListener yesButton;
        UIEventListener noButton;

        private void Awake()
        {
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
        /// 删除存档
        /// </summary>
        /// <param name="obj"></param>
        private void YesButton_PointClike(UnityEngine.EventSystems.PointerEventData obj)
        {
            //产生按钮声音
            AudioManager.Instance.PlaySoundArow(AudioClipsManager.Instance.buttonClip, false);

            //关闭本窗口显影
            SetVisiable(false);

            //删除用户名，金币等游戏存档
            Account account = new()
            {
                AccountID = 1,
                AccountName = null
            };
            DBConfigFactory.Instance.UpdateOne(account);//删除用户名

            //删除英雄信息
            Character character = new()
            {
                CharacterID = 1,
                JobID = 0,
                JobModel = null
            };
            DBConfigFactory.Instance.UpdateOne(character);


            //显示输入名称窗口 
            UIManager.Instance.GetWindow("StartScene_InputPanel").SetVisiable(true);
            
        }

        private void NoButton_PointClike(UnityEngine.EventSystems.PointerEventData obj)
        {
            //产生按钮声音
            AudioManager.Instance.PlaySoundArow(AudioClipsManager.Instance.buttonClip, false);
            //关闭本窗口显影
            SetVisiable(false);
        }
    }
}

