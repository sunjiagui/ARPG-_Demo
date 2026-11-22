using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ns
{
	///<summary>
	///
	///</summary>
	public class UIMainCity : MonoBehaviour
	{
        private void Start()
        {
            UIManager .Instance .GetWindow ("UIScene_Menu").SetVisiable (true);
            UIManager.Instance.GetWindow("UIScene_Money").SetVisiable(true);
            UIManager.Instance.GetWindow("UIScene_Map").SetVisiable(true);
            UIManager.Instance.GetWindow("UIScene_RoleSimpleInfo").SetVisiable(true);
        }
    }
}

