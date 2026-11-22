using System.Collections;

using System.Collections.Generic;

using TMPro;

using UnityEngine;

using UnityEngine.UI;



namespace ns

{

	///<summary>

	///

	///</summary>

	public class UIScene_SkillSelect : UIWindow

	{

		TextMeshProUGUI skillDescribtion;

		SkillInfo[] skillInfos;

		SkillSelect[] skillSelects;

		SkillData[] skillDatas;

        readonly string blackIcon = "UIAtlas_49";

        bool isSelect;

        UIEventListener closeButton;

        

        int SkillDataID = -1;//选择的具体技能按钮对应的技能列表中的具体哪个技能

        Info[] infos;

        private void Awake()

        {

            infos = new Info[4];

            skillDescribtion = transform .FindChildByName("SkillDescribtion_text").GetComponent<TextMeshProUGUI>();

			skillInfos = GetComponentsInChildren<SkillInfo>();

			skillSelects = GetComponentsInChildren<SkillSelect>();

            closeButton = GetUIEventListener("Button_Closed");

        }

        private void OnEnable()

        {

            closeButton.PointClike += CloseButton_PointClike;

            InitInfos();

            InitSeclects();

        }
        private void OnDisable()

        {

            closeButton.PointClike -= CloseButton_PointClike;

            DisposeEvent();



        }

        private void CloseButton_PointClike(UnityEngine.EventSystems.PointerEventData obj)

        {

            AudioManager.Instance.PlaySoundArow(AudioClipsManager.Instance.buttonClip);

            SetVisiable (false);

        }
        //初始化左侧技能按钮(图标，技能ID，技能名称，单击事件)

        void InitInfos()

		{

            skillDatas = SkillManager.PlayerSkillManager.GetSkillDatas();

			for (int i = 0; i < skillInfos.Length; i++)

			{

				skillInfos[i].Init();

				skillInfos[i].Index = i;

				skillInfos[i].SetIcon(skillDatas[i].skillIconName);

				

				skillInfos[i].SetSkillName(skillDatas[i].skillName);

                skillInfos[i].GetEvent().PointClike += SkillInfo_PointClick;

			}
		}
        void InitSeclects()
        {

            for (int i = 0; i < skillSelects.Length; i++)

            {
				SkillSelect skillSelect = skillSelects[i];
				skillSelect.Init();
                skillSelect.Index = i;
                skillSelect.SetIcon(blackIcon);//黑色衬底图
                skillSelect.GetListener().PointClike += SkillSelect_PointClick;
            }

        }
        private void SkillSelect_PointClick(UnityEngine.EventSystems.PointerEventData obj)
        {
            //按钮声音

            AudioManager.Instance.PlaySoundArow(AudioClipsManager.Instance.buttonClip);

            if (!isSelect) return; //如果没选择左边技能直接返回

            //拿到上次按下的技能按钮清洗相同图片

            string target = skillDatas[SkillDataID].skillIconName;

            CleraIcon(target);



            SkillSelect select = obj.pointerClick.GetComponent<SkillSelect>();

            //为select图片赋值

            select.SetIcon(target);



            //改变当前按钮存储的技能索引

            select.SkillID = SkillDataID;



            //并且为技能信息管理器赋值，以便下一个场景使用

            NoticeDataInfo();



            isSelect = false;

            //清除选择按钮的高亮

            skillInfos[SkillDataID].GetCheckMark ().gameObject .SetActive(isSelect);

            //清除技能介绍

            ChangeSkillDes("");


        }

        private void SkillInfo_PointClick(UnityEngine.EventSystems.PointerEventData obj)

        {

            //声音
            AudioManager.Instance.PlaySoundArow(AudioClipsManager.Instance.buttonClip);
            //设置描述

            SkillInfo info = obj.pointerClick.GetComponent<SkillInfo>();

            int currentSkillInfoNum = info.Index;

			ChangeSkillDes(skillDatas[currentSkillInfoNum].description);

            //如果之前按下按钮则关闭之前按钮高亮

            if (SkillDataID != -1) skillInfos[SkillDataID].GetCheckMark().gameObject .SetActive(false);

            //通知当前按下的技能按钮的id方便为机能状态单击事件查找

            SkillDataID = currentSkillInfoNum;

            //将当前按钮高亮

            isSelect = true;

            info.GetCheckMark ().gameObject .SetActive(isSelect);

        }

		void ChangeSkillDes(string des)
		{

			skillDescribtion .text = des;

		}
        private void CleraIcon(string target)

        {

            for (int i = 0; i < skillSelects.Length; i++)

            {

                SkillSelect select = skillSelects[i];

                if (select.GetIcon() == AtlasManager.GetSpriteMain(target)) select.SetIcon(blackIcon);
            }

        }



        private void NoticeDataInfo()

        {

            int count = 0;//为Info数组按顺序添加数据

            for (int i = 0; i < skillSelects.Length; i++)

            {



                SkillSelect _select = skillSelects[i];

                if (_select.GetIcon() == AtlasManager.GetSpriteMain(blackIcon)) continue;



                Info info = new()

                {

                    ID = _select.Index,

                    SkillData = skillDatas[_select.SkillID]

                };
                infos[count++] = info;
            }
            SkillDataInfo.SetInfo(infos);

        }



        private void DisposeEvent()

        {

            for (int i = 0; i < skillInfos.Length; i++)

            {

                skillInfos[i].GetEvent().PointClike -= SkillInfo_PointClick;

            }

            for (int i = 0; i < skillSelects.Length; i++)

            {

                skillSelects[i].GetListener().PointClike -= SkillSelect_PointClick;

            }

        }

    }

}



