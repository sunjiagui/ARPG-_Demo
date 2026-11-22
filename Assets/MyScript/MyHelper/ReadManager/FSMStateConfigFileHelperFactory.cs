using MyFSM;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace ns
{
    ///<summary>
    ///状态机专属的（因为解析方法不一样所以不通用）加工工厂。 外部通过开启协程Initial方法获取该类的初始化，后通过GetMap拿到状态配置
    ///</summary>
    public class FSMStateConfigFileHelperFactory
    {
        public bool isInit;
        private ContentHelper content;
        private Dictionary<string, Dictionary<string, Dictionary<string, string>>> MapList { get; set; }
        private string lastKey;
        public FSMStateConfigFileHelperFactory()
        {
            MapList = new();
        }


        /// <summary>
        /// 获取状态机所需配置信息字典
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public Dictionary<string, Dictionary<string, string>> GetMap(string fileName)
        {
            if (MapList.ContainsKey(fileName)) return MapList[fileName];
            return null;
        }

        /// <summary>
        /// 外部调用时提供配置文件名，该工厂中的字典将被初始化，最后通过调用GetMap拿到状态机所需的配置字典
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public IEnumerator Initial(string fileName)
        {
            yield return ConfigFile(fileName);
        }


        /// <summary>
        /// 通过配置文件帮助器获取与解析配置文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private IEnumerator ConfigFile(string fileName)
        {
            Dictionary<string, Dictionary<string, string>> map = new();
            content = new();
            yield return ConfigFileHelper.GetTxtFromStreaming(fileName, file => content.TextContent = file);
            
            ConfigFileHelper.ParseConfig(content.TextContent, fileContent =>
            {
                if (fileContent.StartsWith('['))
                {
                    lastKey = fileContent.Substring(1, fileContent.Length - 2);
                    map.Add(lastKey, new Dictionary<string, string>());
                }
                else
                {
                    string[] target = fileContent.Split('>');
                    map[lastKey].Add(target[0], target[1]);
                }
            });
            MapList.Add(fileName, map);
            isInit = true;
        }
    }
}

