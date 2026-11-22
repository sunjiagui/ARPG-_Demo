using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ns
{
	///<summary>
	///存储点的数据，交错数组
	///</summary>
	public class WayLineControl : MonoSingleton<WayLineControl>
	{
		private Vector3[][] wayRoot;
		int lineCount;
		int pointCount;

		public Vector3 GetRandowPoint()
        {
            int randomLineCount = Random.Range(0, lineCount);
            int randomPointCount = Random.Range(0, pointCount);
            return wayRoot[randomLineCount][randomPointCount];
            
        }
        protected override void Init()
        {
            InitWayRoot();
        }
        private void InitWayRoot()
        {
            lineCount = transform.childCount;
            wayRoot = new Vector3[lineCount][];
            for (int i = 0; i < wayRoot.Length; i++)
            {
                Transform pointTF = transform.GetChild(i);
                pointCount = pointTF.childCount;
                wayRoot[i] = new Vector3[pointCount];
                for(int j = 0; j < pointCount; j++)
                {
                    wayRoot[i][j] = pointTF.GetChild(j).position;
                }
            }
        }
    }
}

