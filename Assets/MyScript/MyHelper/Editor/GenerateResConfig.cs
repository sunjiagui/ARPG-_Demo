using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace ns
{
	///<summary>
	///将技能预制件的路径打包成文件，并提供简单的静态实现 ，文件被换了位置也可以使用编译器静态方法配出路径
	///</summary>
	public class GenerateResConfig : Editor
	{
		[MenuItem("Tools/Resources/Generate ResConfig File") ]
		public static void Generate()
		{
			string[] targetPath = AssetDatabase.FindAssets("t:Object", new string[] {"Assets/Resources"});
			for (int i = 0; i < targetPath.Length; i++)
			{
                targetPath[i] = AssetDatabase.GUIDToAssetPath(targetPath[i]);
                string name = Path.GetFileNameWithoutExtension(targetPath[i]);
				string path = targetPath[i].Replace("Assets/Resources/", string.Empty);//去除前缀
				path = Path.ChangeExtension (path, null);
                targetPath[i] = name + "=" + path;
            }
			File.WriteAllLines("Assets/StreamingAssets/ResourcesConfigMap.txt", targetPath);
			AssetDatabase.Refresh();//刷新



        }
        
    }
}

