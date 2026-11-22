using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ns
{
	///<summary>
	///
	///</summary>
	public class SelectHeroScene : MonoBehaviour
	{
        private void Start()
        {
            UIManager.Instance.GetWindow("SelectHeroScene_FirstPanel").SetVisiable(true);
            UIManager.Instance.GetWindow("SelectHeroScene_CharacterInfoPanel").SetVisiable(true);

        }
    }
}

