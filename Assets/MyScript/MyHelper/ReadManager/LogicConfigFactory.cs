using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ns
{
	///<summary>
	///文件config的解析
	///</summary>
	public static class LogicConfigFactory
	{
        static string fileName;
		static Dictionary<string, Dictionary<string, string>> cache;

        /// <summary>
        /// 将配置信息加载进入字典
        /// </summary>
        /// <returns></returns>
        public static IEnumerator Initial()
        {
            fileName = StreamingFileName.Config;

            //通过文件读取器拿到配置文件（暂时不复制到持久化路径）
            ContentHelper content = new();
            cache = new();

            yield return ConfigFileHelper.GetTxtFromStreaming(fileName , (s) => { content.TextContent = s; });
            string target = "";

            ConfigFileHelper.ParseConfig(content.TextContent, (s) =>
            {
                
                if (s.StartsWith('['))
                {
                    target = s.Substring(1, s.Length - 2);
                    cache.Add(target.Trim (), new Dictionary<string, string>());
                }
                else if(s.Length > 1)
                {
                    string[] targetpart = s.Split('=');
                    cache[target].Add(targetpart[0].Trim(), targetpart[1].Trim());
                }
            });
            
        }

        public static string GetInfo(string main, string key)
        {
            return cache[main][key];
        }
    }
}

