using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ns
{
	///<summary>
	///
	///</summary>
	public class StartScene_FirstPanel : UIWindow
	{
        UIEventListener StartGame_Button;
        UIEventListener Continue_Button;


        private void Awake()
        {
            StartGame_Button = GetUIEventListener("StartGame_Button");
            Continue_Button = GetUIEventListener("GoGame_Button");

        }


        private void OnEnable()
        {
            //开始新游戏按钮
            StartGame_Button.PointClike += StartGame_Button_PointClike;
            Continue_Button.PointClike += Continue_Button_PointClike;

        }

        

        private void OnDisable()
        {
            //开始新游戏按钮
            StartGame_Button.PointClike -= StartGame_Button_PointClike;
            Continue_Button.PointClike -= Continue_Button_PointClike;

        }

        //开始新游戏按钮
        private void StartGame_Button_PointClike(UnityEngine.EventSystems.PointerEventData obj)
        {
            //产生按钮声音
            AudioManager.Instance.PlaySoundArow(AudioClipsManager.Instance.buttonClip, false);

            //判断有无存档 (通过用户名判断)

            //无 => 显示二级界面输入名称
            if (string.IsNullOrEmpty ( DBConfigFactory.Instance.QueryDB<Account>(1).AccountName))
            {
                UIManager.Instance.GetWindow("StartScene_InputPanel").SetVisiable(true);
            }
            //有 => 是否删档（二级界面） 
            else
            {
                UIManager.Instance.GetWindow("Scene01_MindPanel").SetVisiable(true);
            }


        }
        private void Continue_Button_PointClike(UnityEngine.EventSystems.PointerEventData obj)
        {
            //产生按钮声音
            AudioManager.Instance.PlaySoundArow(AudioClipsManager.Instance.buttonClip, false);

            //没存档
            if (string.IsNullOrEmpty(DBConfigFactory.Instance.QueryDB<Account>(1).AccountName))
            {
                UIManager.Instance.GetWindow("Scene01_NoDataPanel").SetVisiable(true);
                StartCoroutine(TimeLog(2));
            }
            //有存档
            else SceneManager.LoadSceneAsync(4);

        }

        IEnumerator TimeLog(float time)
        {
            yield return new WaitForSeconds(time);
            UIManager.Instance.GetWindow("Scene01_NoDataPanel").SetVisiable(false);
        }

    }
}

