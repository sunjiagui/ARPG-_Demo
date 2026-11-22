using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ns
{
    ///<summary>
    ///	子类需要拿到所有的交互并绑定事件		提供方法：控制显影，拿到可交互元素
    ///</summary>
    public abstract class UIWindow : MonoBehaviour
	{
        protected Dictionary<string, UIEventListener> UIEventListenerDic;
        

        /// <summary>
		/// 根据名称拿到该窗口的可交互元素
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
        public UIEventListener GetUIEventListener(string name)
		{
			UIEventListenerDic ??= new();
			if (!UIEventListenerDic.ContainsKey(name))
			{
				Transform targetTF = transform.FindChildByName(name);
				UIEventListener targetUIEventListeren = UIEventListener.GetUIEventListener(targetTF);
				UIEventListenerDic.Add(name, targetUIEventListeren);
			}
            return UIEventListenerDic[name];
        }

		/// <summary>
		/// 设置窗口显影
		/// </summary>
		/// <param name="visiable"></param>
		/// <param name="delay"></param>
        public void SetVisiable(bool visiable)
		{
			gameObject.SetActive(visiable);
            
        }

		
		
	}
}

