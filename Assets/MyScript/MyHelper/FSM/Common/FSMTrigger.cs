using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyFSM;

namespace MyFSM
{
	///<summary>
	///
	///</summary>
	public abstract class FSMTrigger
	{
		public TriggerID TriggerID { get; set; }
		/// <summary>
		/// 通过构造函数和抽象方法解决赋值问题 解决代码重复问题（如果不这样做每个子类都要重新写一个构造函数和初始化方法）
		/// </summary>
		public FSMTrigger()
		{
			Init();
		}
		public abstract void Init();
		public abstract bool IsTrigger(FSMBase fSMBase);
		


    }
}

