using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace ns
{
	///<summary>
	///为文本赋值
	///</summary>
	public class SelectHeroScene_CharacterInfoPanel : UIWindow
	{
        TextMeshProUGUI Key_Weapon;
        TextMeshProUGUI Value_Weapon;
        TextMeshProUGUI Key_Job;
        TextMeshProUGUI Value_Job;
        TextMeshProUGUI Role_Describtion;
        



        //找相应的文本进行赋值
        private void Awake()
        {
            //找文本
            Key_Weapon = transform.FindChildByName("Key_Weapon").GetComponent <TextMeshProUGUI>();
            Value_Weapon = transform.FindChildByName("Value_Weapon").GetComponent<TextMeshProUGUI>();
            Key_Job = transform.FindChildByName("Key_Job").GetComponent<TextMeshProUGUI>();
            Value_Job = transform.FindChildByName("Value_Job").GetComponent<TextMeshProUGUI>();
            Role_Describtion = transform.FindChildByName("Role_Describtion").GetComponent<TextMeshProUGUI>();


            Key_Weapon.text = "武器";
            Key_Job.text = "职业";
            
        }
        public void ChangeValue_Job(string value)
        {
            Value_Job.text = value;
        }
        public void ChangeValue_Weapon(string value)
        {
            Value_Weapon .text = value;
        }
        public void ChangeRole_Describtion(string value)
        {
            Role_Describtion .text = value;
        }
    }
}

