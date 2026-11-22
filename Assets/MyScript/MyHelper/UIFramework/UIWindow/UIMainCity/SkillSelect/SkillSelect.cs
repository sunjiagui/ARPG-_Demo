using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ns
{
	///<summary>
	///单击事件：缓存技能ID ，图片排序 ; 初始化：图片选择黑色底片，id 
	///</summary>
	public class SkillSelect : MonoBehaviour
	{
		Image image;
		UIEventListener pointEvent;
		public int SkillID { get; set; }
		public int Index {  get; set; }
		public void Init()
		{
            image = transform.FindChildByName("SkillIcon").GetComponent<Image>();
            pointEvent = GetComponent<UIEventListener>();
        }

		public UIEventListener GetListener()
		{
			return pointEvent;
		}

		public void SetIcon(string name)
		{
            image .sprite = AtlasManager.GetSpriteMain(name);
        }

		public Sprite GetIcon()
		{
			return image.sprite;
		}


	}
}

