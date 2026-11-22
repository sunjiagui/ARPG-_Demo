using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
namespace ns
{
	///<summary>
	///
	///</summary>
	
	public class Info
	{
		public int ID {  get; set; } //在第几个技能位置
		public SkillData SkillData { get; set; } //技能数据
	}
	public static class SkillDataInfo
	{
		private static Info[] dataInfo;
		public static void SetInfo(Info[] infos)
		{
			dataInfo = infos;
		}
		public static Info[] GetInfo()
		{
			return dataInfo;
		}


	}
}

