using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ns
{
	///<summary>
	///
	///</summary>
	public class TriggerEffect : MonoBehaviour
	{
        

        public void OnTriggerEnter(Collider other)
        {
            EnemySpawn.Instance.GenerateEnemy();
        }
    }
}

