using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ns
{
	///<summary>
	///
	///</summary>
	public static class TransformHelper 
	{
        public static Transform FindChildByName(this Transform tf, string name)  //2026.1.29
        {
            Transform target = tf.Find(name);
            if (target != null) return target;
            for (int i = 0; i < tf.childCount; i++)
            {
                Transform finallyTarget = tf.GetChild(i).FindChildByName(name);
                if (finallyTarget != null) return finallyTarget;
            }
            return null;
            //注释：this是拓展了transform的方法 如str.Reverse 让人以为就是字符串类中的方法 但其实是静态工具库中的方法
        }
    }
}

