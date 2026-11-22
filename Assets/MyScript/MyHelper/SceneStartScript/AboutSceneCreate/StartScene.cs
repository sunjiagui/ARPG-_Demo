using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ns
{
	///<summary>
	///
	///</summary>
	public class StartScene : MonoBehaviour
	{

        private void Start()
        {
            UIManager.Instance.GetWindow("StartScene_FirstPanel").SetVisiable(true);
        }



    }
}

