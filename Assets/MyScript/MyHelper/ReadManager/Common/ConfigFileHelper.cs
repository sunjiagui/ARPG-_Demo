using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

namespace ns
{
	///<summary>
	///提供读取文件解析文件的方法
	///</summary>
	public static class ConfigFileHelper
	{
        /// <summary>
        /// 将得到的内容返回到传入委托的中，需要在委托中使用变量存储下来（找个帮助类的字段存储，做到公共性）
        /// </summary>
        /// <param name="fileName">位于流文件夹下的文件名要加后缀</param>
        /// <param name="fileContent">文本会返回进这个委托，找个变量存储起来</param>
        /// <returns></returns>
        public static IEnumerator GetTxtFromPersistent(string fileName, Action<string> fileContent)
        {
            string path = Path.Combine (Application.persistentDataPath , fileName);
            Uri uri = new(path);

            using var www = UnityWebRequest.Get(uri);
            yield return www.SendWebRequest();//下载结果完成 可能是成功 超时 失败
           
            if (www.result == UnityWebRequest.Result.Success)//如果下载结果是成功
            {
                fileContent.Invoke(www.downloadHandler.text);
            }
            else
            {
                Debug.LogError("读取失败: " + www.error + "，路径: " + path);
                yield break; // 失败就直接终止协程
            }

        }
        public static IEnumerator GetTxtFromStreaming(string fileName, Action<string> fileContent)
        {
            string path = Path.Combine(Application.streamingAssetsPath, fileName);
            Uri uri = new(path);

            using var www = UnityWebRequest.Get(uri);
            yield return www.SendWebRequest();//下载结果完成 可能是成功 超时 失败

            if (www.result == UnityWebRequest.Result.Success)//如果下载结果是成功
            {
                fileContent.Invoke(www.downloadHandler.text);
            }
            else
            {
                Debug.LogError("读取失败: " + www.error + "，路径: " + path);
                yield break; // 失败就直接终止协程
            }

        }


        /// <summary>
        /// 能自动把所有空行剔除
        /// </summary>
        /// <param name="fileContent"></param>
        /// <param name="parseMain"></param>
        public static void ParseConfig(string fileContent, Action<string> parseMain)
        {
            //适用于配置文件较为复杂性能更好
            using StringReader reader = new(fileContent);//自动根据换行符拆分成多行（任意换行符）(当有两个换行符时第二个会返回空字符)
            string targetGet;
            while ((targetGet = reader.ReadLine()) != null) //该方法用一次自动换行
            {
                if (string.IsNullOrEmpty(targetGet)) continue;
                parseMain.Invoke(targetGet);
            }
            //"PlayerSecondSkill=SkillPerfabs/PlayerSecondSkill\r\nPlayerSkill=SkillPerfabs/PlayerSkill\r\n"
            //string[] keyValue = targetGet.Split('=');
            //recPathDic.Add(keyValue[0], keyValue[1]);

            //recPathDic.Clear();
            //string[] theGet = target.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);//选项作用自动去除空白字符上方字符串切割会在末位多一个空白字符
            //foreach (var item in theGet)
            //{
            //    string[] targetGet = item.Split('=' , StringSplitOptions.RemoveEmptyEntries);
            //    if(targetGet.Length == 2) recPathDic.Add(targetGet[0], targetGet[1]);
            //}


        }
    }
}

