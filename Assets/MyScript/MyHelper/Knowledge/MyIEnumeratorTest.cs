using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ns
{
	///<summary>
	///
	///</summary>
	public class MyIEnumeratorTest : MonoBehaviour
	{
        public int moveSpeed = 5;
        public Transform[] wayLine;
        public IEnumerator MoveTarget(Transform[] wayline)
		{
            for(int i = 0; i < wayline.Length; i++)
            {
                yield return StartCoroutine(Fun01(wayline[i].position));
            }
            
			

		}
        public IEnumerator Fun01(Vector3 wayPoint)
        {
            while((transform.position - wayPoint).sqrMagnitude > 1)
            {
                transform.position = Vector3.MoveTowards(transform.position, wayPoint, Time.deltaTime * moveSpeed);
                yield return null;
            }
            
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.Space))
            {
                StartCoroutine(MoveTarget(wayLine));
            }
        }



        //IEnumerator iterator;

        //      private void OnGUI()
        //      {
        //          if(GUILayout.Button("启动"))
        //	{
        //              iterator = Fun01();
        //	}
        //          if (GUILayout.Button("执行一次"))
        //          {
        //		iterator.MoveNext();
        //          }
        //          if (GUILayout.Button("执行一次"))
        //          {
        //		print(iterator.Current);
        //          }
        //      }
        //      public IEnumerator Fun01()
        //{
        //          print(3);
        //          for (int i = 0; i < 10; i++)
        //	{
        //		print(i);
        //		yield return 1;
        //	}

        //}
    }
}

