using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace ns
{
	///<summary>
	///
	///</summary>
	public static class ArrayHelper 
	{
		public static T Find<T>(this T[] list, Func<T, bool> func) 
		{
			for(int i = 0;  i < list.Length; i++)
			{
				if (func(list[i])) return list[i];
			}
			return default;
        }
		public static T GetMin<T, Q>(this T[] list, Func<T, Q> func) where Q : IComparable<Q>
		{
			T minItem = list[0];
			for(int i = 0; i< list.Length-1; i++)
			{
				if (func(minItem).CompareTo(func(list[i + 1])) > 0) 
                {
                    minItem = list[i + 1];
                }
            }
			return minItem;
		}

	}
}

