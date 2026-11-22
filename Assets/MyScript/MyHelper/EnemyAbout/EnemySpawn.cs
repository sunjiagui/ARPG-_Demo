using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ns
{
	///<summary>
	///
	///</summary>
	public class EnemySpawn : MonoSingleton<EnemySpawn>
	{
        public GameObject enemyObj;
        public int spawnCount = 0;

        private void Start()
        {
            for(int i = 0;  i < spawnCount; i++)
            {
                GenerateEnemy();
            }
        }




        public void GenerateEnemy()
        {
            Vector3 point = WayLineControl.Instance.GetRandowPoint();
            
            GameObjectPool.Instance.CreatObject("EnemyPerfab", enemyObj, point, transform.rotation);


        }
    }
}

