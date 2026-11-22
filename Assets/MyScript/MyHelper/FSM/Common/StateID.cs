using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFSM
{
	///<summary>
	///状态ID（枚举）
	///</summary>
	public enum StateID
	{
		/// <summary>
		/// 轮到这个状态要切回自己输入的初始状态
		/// </summary>
        Default,

        /// <summary>
        /// 不存在的状态？？？
        /// </summary>
        None,
		

		Attake,

		Idle,
		/// <summary>
		/// 巡逻
		/// </summary>
		Patrolling,

		Dead,
		/// <summary>
		/// 追逐
		/// </summary>
		Persuit



	}

}

