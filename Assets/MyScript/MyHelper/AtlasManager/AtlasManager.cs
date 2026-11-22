using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

namespace ns
{
	///<summary>
	///
	///</summary>
	public static class AtlasManager
	{
		static readonly Dictionary<string, Sprite> spriteDic;
		static readonly SpriteAtlas spriteAtlas;
		static AtlasManager()
		{
			spriteAtlas = ResourcesManager.Load<SpriteAtlas>("SpriteAtlas_Skill");
			spriteDic = new Dictionary<string, Sprite>();
		}
		
		public static Sprite GetSpriteMain(string name)
		{

			if(spriteDic.ContainsKey(name)) return spriteDic[name];
            Sprite sprite = spriteAtlas.GetSprite(name);
			spriteDic.Add(name, sprite);
			return spriteDic[name];

        }


	}
}

