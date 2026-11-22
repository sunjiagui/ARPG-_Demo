using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ns
{
	///<summary>
	///方便以后这些配置文件变化以后将散落的字符串快速定位
	///</summary>
	public static class StreamingFileName
	{
		//技能效果配置文件名称，目前resourceManage再用
		public static string SkillEffectConfigMap { get { return "ResourcesConfigMap.txt"; } }

        //config 登陆文案等
        public static string Config { get { return "LogicConfig.txt"; } }


    }
}

