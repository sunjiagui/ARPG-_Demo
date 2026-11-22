using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;
using System.IO;
using UnityEngine.Networking;
using System;

namespace ns
{
	///<summary>
	///提供键表以及增删改查
	///</summary>
	public class DBConfigFactory : MonoSingleton <DBConfigFactory>
	{
		SQLiteConnection connection;
        /// <summary>
        /// 建立数据库的链接
        /// </summary>
        protected override void Init()
        {
            base.Init();
            DontDestroyOnLoad(gameObject);
            //连接数据库
            StartCoroutine(ConnectDB());
        }

        /// <summary>
        /// 连接数据库
        /// </summary>
        /// <returns></returns>
        private IEnumerator ConnectDB()
        {
            //1.将StreamingAssets文件夹下的数据库文件复制到在持久化路径下
            //数据库的初始位置，流文件夹
            string DBStreamingPath = Path.Combine(Application.streamingAssetsPath, "ARPG.db");
            //数据库的写入文件夹位置即持久化路径
            string DBPersistPath = Path.Combine(Application.persistentDataPath, "ARPG.db");

#if UNITY_STANDALONE || UNITY_EDITOR   
            DBStreamingPath = "file:///" + DBStreamingPath;
# endif
            if (!File.Exists(DBPersistPath))
            {
                //通过UnityWebRequest读取
                using var www = UnityWebRequest.Get(DBStreamingPath);
                //发送下载请求并等待
                yield return www.SendWebRequest();
                if (www.result == UnityWebRequest.Result.Success)
                {
                    File.WriteAllBytes(DBPersistPath, www.downloadHandler.data);
                }
            }
            if (File.Exists(DBPersistPath))
            {
                //2.连接数据库
                connection = new SQLiteConnection(DBPersistPath);
            }

        }    
        
        /// <summary>
        /// 根据类型提供主键值拿到查询对象从而获取值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pk"></param>
        /// <returns></returns>
        public T QueryDB<T>(object pk)where T : new()
        {
            return connection.Find<T>(pk);
            
        }

        public object QueryDB(object pk, TableMapping map)
        {
            return connection.Find(pk, map);
        }

        public TableMapping GetMapping(Type type)
        {
            return connection.GetMapping(type);
        }

        /// <summary>
        /// 根据对象自动获取是哪张表插入数据
        /// </summary>
        /// <param name="o"></param>
        public void InsertOne(object o)
        {
            connection.Insert(o);
        }
        /// <summary>
        /// 根据对象自动获取是哪张表更改数据
        /// </summary>
        /// <param name="o"></param>
        public void UpdateOne(object o)
        {
            connection.Update(o);
        }
        private void OnDestroy()
        {
            connection.Dispose();
        }

    }
}

