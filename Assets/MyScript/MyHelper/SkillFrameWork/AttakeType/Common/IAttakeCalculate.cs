using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ns
{
	///<summary>
	///规范技能的判定
	///</summary>
	public interface IAttakeCalculate
	{
		/// <summary>
		/// 技能判定方法
		/// </summary>
		/// <param name="skillDeployer">该脚本开协程拿信息</param>
		/// <param name="SkillSelfPosition">技能自身位置</param>
		/// <returns></returns>
		void Calculate(SkillDeployer skillDeployer ,Transform SkillSelfPosition);//有的核算方法需要协程，，要脚本
		
		

		
	}
}

