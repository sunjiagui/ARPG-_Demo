using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ns
{
	///<summary>
	///
	///</summary>
	public class BattleScene : MonoBehaviour
	{
        private void Start()
        {
            UIManager.Instance.GetWindow("RoleSimpleInfo").SetVisiable (true);
            UIManager.Instance.GetWindow("Money").SetVisiable(true);
            UIManager.Instance.GetWindow("Attake").SetVisiable(true);
        }
    }
}

