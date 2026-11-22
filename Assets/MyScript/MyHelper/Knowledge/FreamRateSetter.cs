using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ns
{
	///<summary>
	///
	///</summary>
	public class FreamRateSetter : MonoBehaviour
	{
        private void Awake()
        {
            Application.targetFrameRate = 120;
            QualitySettings.vSyncCount = 0;
        }
    }
}

