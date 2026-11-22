using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
namespace ns
{
	///<summary>
	///
	///</summary>
	public class SelectHeroScene_FirstPanel : UIWindow 
	{
        UIEventListener Creat_Image;
        Toggle zhanshi_Toggle;
        Toggle sorceress_Toggle;
        /// <summary>
        /// 记录的上一次按钮
        /// </summary>
        string lastHeroName;
        //是否已经选则英雄
        bool isSelect;
        
        Transform RoleTF;
        readonly string RoleTFTag = "RoleRoot";

        private void Awake()
        {
            Creat_Image = GetUIEventListener("Creat_Image");   //开始游戏按钮
            zhanshi_Toggle = transform.FindChildByName("zhanshi_Toggle_1").GetComponent<Toggle>();//战士开关
            sorceress_Toggle = transform.FindChildByName("sorceress_Toggle_2").GetComponent<Toggle>();//法师开关
            RoleTF = GameObject.FindGameObjectWithTag(RoleTFTag).transform;
        }

        private void OnEnable()
        {
            Creat_Image.PointClike += Creat_Image_PointClike;
            zhanshi_Toggle.onValueChanged.AddListener(OnHeroChange);
            sorceress_Toggle .onValueChanged .AddListener(OnHeroChange);
        }

        

        private void OnDisable()
        {
            Creat_Image.PointClike -= Creat_Image_PointClike;
            zhanshi_Toggle .onValueChanged .RemoveListener (OnHeroChange);
            sorceress_Toggle.onValueChanged.RemoveListener (OnHeroChange);
        }
        /// <summary>
        /// 游戏开始
        /// </summary>
        /// <param name="obj"></param>
        private void Creat_Image_PointClike(UnityEngine.EventSystems.PointerEventData obj)
        {
            if (!isSelect) return;
            
            //产生按钮声音
            AudioManager.Instance.PlaySoundArow(AudioClipsManager.Instance.buttonClip, false);

            //加载加载场景
            SceneManager.LoadSceneAsync(0);

           
        }
        

        /// <summary>
        /// 英雄更换
        /// </summary>
        /// <param name="b"></param>
        private void OnHeroChange(bool b)
        {
            isSelect = true;
            //按钮声音
            AudioManager.Instance.PlaySoundArow(AudioClipsManager.Instance.buttonClip);
            //命名规范：按钮：zhanshi_Toggle_1  人物：Hero_sorceress

            //拿到当前按下的按钮名称
            if (!EventSystem.current.currentSelectedGameObject) return;
            string currentButtonName = EventSystem.current.currentSelectedGameObject.name;
            string[] target = currentButtonName.Split('_');
            string heroName = target[0];
            int heroID = int.Parse(target[2]);

            //更改人物展示
            ChangeView(b, heroName);
            //更改配置信息
            ChangeRoleInfo(heroName);
            //更改数据库，让主场景人物变换
            //将ID为1的角色职业改为根据按钮名称，方便后续加载
            TellBattle(heroName , heroID);
        }

        private static void TellBattle(string heroName , int jobID)
        {
            Character character = new()
            {
                CharacterID = 1,
                JobID = jobID,
                JobModel = heroName
            };
            DBConfigFactory.Instance.UpdateOne(character);
        }

        private static void ChangeRoleInfo(string heroName)
        {
            SelectHeroScene_CharacterInfoPanel textWindow = UIManager.Instance.GetWindow("SelectHeroScene_CharacterInfoPanel") as SelectHeroScene_CharacterInfoPanel;
            textWindow.ChangeValue_Job(LogicConfigFactory.GetInfo("Job", heroName + "_Job"));
            textWindow.ChangeValue_Weapon(LogicConfigFactory.GetInfo("Job", heroName + "_Weapon"));
            textWindow.ChangeRole_Describtion(LogicConfigFactory.GetInfo("Job", heroName + "_Describtion"));
        }

        private void ChangeView(bool b , string heroName)
        {
            if (!b && lastHeroName != null) RoleTF.FindChildByName(lastHeroName).gameObject.SetActive(b);
            if (b)
            {
                string heroPlusName = "Hero_" + heroName;
                lastHeroName = heroPlusName;
                RoleTF.FindChildByName(heroPlusName).gameObject.SetActive(b);
            }
        }




    }
}

