using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ns
{
	///<summary>
	///
	///</summary>
	public class GameMain : MonoBehaviour
	{
        private IEnumerator Start()
        {
            //让配置文件解析好
            yield return LogicConfigFactory.Initial();
            //resourceManger初始化
            yield return ResourcesManager.Initial();
            //开启下一个场景
            SceneManager.LoadSceneAsync(0);
        }
    }
}

