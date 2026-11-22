using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ns
{
	///<summary>
	///查找窗口,添加窗口
	///</summary>
	public class UIManager : MonoSingleton<UIManager>
	{
        Dictionary<string, UIWindow> allGameWindow;
        [HideInInspector]
        public Transform rootTransform;

        /// <summary>
        /// 游戏开始后将所有窗口通过类型添加
        /// </summary>
        protected override void Init()
        {
            base.Init();
            rootTransform = transform;
            allGameWindow = new Dictionary<string, UIWindow>();
            UIWindow[] allWindow = FindObjectsOfType<UIWindow>();
            for (int i = 0; i < allWindow.Length; i++)
            {
                AddWindow(allWindow[i]);//添加缓存
                allWindow[i].SetVisiable(false);//将窗口显影关掉
            }
            
            
        }
        /// <summary>
        /// 根据窗口类型名称添加窗口
        /// </summary>
        /// <param name="window"></param>
        public void AddWindow(UIWindow window) 
        {
            allGameWindow.Add(window.GetType().Name, window);
        }
        /// <summary>
        /// 通过窗口类型名称获得窗口
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public UIWindow GetWindow(string windowTypeName) 
        {
            if (!allGameWindow.ContainsKey(windowTypeName)) return null;
            return allGameWindow[windowTypeName] as UIWindow;
        }

    }
}

