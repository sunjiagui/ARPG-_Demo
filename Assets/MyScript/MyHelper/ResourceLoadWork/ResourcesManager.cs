//using System;

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.Networking;

namespace ns
{
    ///<summary>
    ///
    ///</summary>
    public static class ResourcesManager
    {
        public static string fileName;
        private static ContentHelper content;
        public static Dictionary<string, string> recPathDic;//资源路径字典 名称：路径
        public static Dictionary<string, object> recDic;//资源字典 名称：资源
        
        public static IEnumerator Initial()
        {
            recPathDic = new Dictionary<string, string>();
            recDic = new Dictionary<string, object>();
            fileName = StreamingFileName.SkillEffectConfigMap;
            yield return ConfigFileHelper.GetTxtFromStreaming(fileName, text =>
            {
                content = new ContentHelper
                {
                    TextContent = text
                    
                };

            });
            ConfigFileHelper.ParseConfig(content.TextContent, text =>
            {
                string[] keyValue = text.Split('=');
                recPathDic.Add(keyValue[0], keyValue[1]);
                
            });
        }

        //提供资源
        public static T Load<T>(string name) where T : UnityEngine.Object
        {
            if (recDic.ContainsKey(name))
            {
                return recDic[name] as T;
            }
            else
            {
                if (name == string.Empty) return null;
                UnityEngine.Object target = Resources.Load(recPathDic[name]);//该方法无需拓展名
                if (target != null)
                {
                    recDic.Add(name, target);
                    return target as T;
                }
                else
                {
                    Debug.Log("资源加载可能有问题");
                    return null;
                }
            }
        }
    }
}

