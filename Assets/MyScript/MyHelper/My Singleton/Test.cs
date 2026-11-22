using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ns
{
	///<summary>
	///
	///</summary>
	public class Test : MonoBehaviour
	{
        private void Start()
        {
            XXManager.Instance.ManagerFun();
            //UIManager.Instance.Init();
            //UIManager.Instance.AddWindow();
            //UIManager.Instance.GetWindow();
        }
    }
}

