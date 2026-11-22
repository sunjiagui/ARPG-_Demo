using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

namespace ns
{
	///<summary>
	///初始化，通过技能管理器中的技能数据，赋值按钮ID，图片 技能ID ，单击事件：向窗口通知自身ID
	///</summary>
	public class SkillInfo : MonoBehaviour
	{
        Image skill_Icon;
        Transform checkMark;
		TextMeshProUGUI skillName;
		UIEventListener pointEvent;
        public int Index { get; set; } 
		

        public void Init()
		{
            checkMark = transform.FindChildByName("Checkmark");
            checkMark .gameObject .SetActive(false);
            skillName = transform.FindChildByName("SkillName").GetComponent<TextMeshProUGUI>();
            skill_Icon = transform.FindChildByName("Skill_Icon").GetComponent<Image>();
            pointEvent = GetComponent<UIEventListener>();

        }

        public void SetSkillName(string name)
		{
            skillName.text = name;
		}

        public void SetIcon(string name)
		{
            skill_Icon.sprite = AtlasManager.GetSpriteMain(name);
		}
        public Transform GetCheckMark()
        {
            return checkMark;
        }
		
       


        public UIEventListener GetEvent()
        {
            return pointEvent;
        }
        
        public Sprite GetIcon()
        {
            return skill_Icon.sprite;
        }


    }
}

