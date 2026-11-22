using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace ns
{
	///<summary>
	///
	///</summary>
	public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
		private static T instance;
		public static T Instance
		{
			get
			{
				if(instance == null)
				{
					instance = FindObjectOfType<T>();
					//当没挂脚本时
					if(instance == null)
					{
						GameObject instanceObj = new("Singleton of" + typeof(T));
						instanceObj.AddComponent<T>(); //此时才会调用脚本中的Awake
                        //DontDestroyOnLoad(instanceObj); 感觉不用加
                    }
					else
					{
						instance.Init();
					}
				}
				return instance;
			}
		}

        protected void Awake()
        {
			//子类会毫无保留的继承awake，子类挂载在物体上父类不用挂
			if (instance == null)
			{
                instance = this as T;
                Init();
            }
        }

		protected virtual void Init()
		{
			
		}
    }
}

