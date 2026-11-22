using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ns
{
	///<summary>
	///场景加载都是加载这个过渡场景，该场景被加载之后会更具索引加载下一个场景
	///</summary>
	public class LoadingScene : MonoBehaviour
	{
        Scrollbar scr;
        
        //按顺序加载场景，并显示进度条
        private void Start()
        {
            //拿到进度条
            scr = GetComponentInChildren<Scrollbar>();
            //取消滑块的交互
            scr.interactable = false;
            if (SceneNumManger.num <= SceneManager.sceneCountInBuildSettings - 1)
            {
                
                //加载场景
                var op = SceneManager.LoadSceneAsync(SceneNumManger.num++);
                //禁止自动切换场景
                op.allowSceneActivation = false;

                //进度条
                StartCoroutine(Log(scr, op));

                //进度条结束时手动结束
                op.allowSceneActivation = true;
            }
        }
        /// <summary>
        /// 进度条
        /// </summary>
        /// <param name="scrollbar"></param>
        /// <param name="operation"></param>
        /// <returns></returns>
        private IEnumerator Log(Scrollbar scrollbar, AsyncOperation operation)//显示进度
        {
            while (scrollbar.value < 1)
            {
                scrollbar.value = operation.progress;
                yield return null;
            }

        }
    }
}

