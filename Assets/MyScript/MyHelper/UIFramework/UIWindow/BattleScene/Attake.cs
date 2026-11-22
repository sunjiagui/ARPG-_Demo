using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
namespace ns
{
	///<summary>
	///
	///</summary>
	public class Attake : UIWindow
	{
		SkillSelect[] skillSelects;
        readonly string blackIcon = "UIAtlas_49";
        private void Awake()
        {

			skillSelects = GetComponentsInChildren<SkillSelect>();
			for(int i = 0;i< skillSelects.Length; i++)
			{
				skillSelects[i].Init();
				skillSelects[i].SetIcon(blackIcon);

            }

			Info[] infos = SkillDataInfo.GetInfo();
			for(int i  = 0; i < infos.Length; i++)
			{
				Info info = infos[i];
				if(info == null ) continue;
				SkillSelect select = skillSelects[info.ID];
				
                select.SetIcon(info.SkillData.skillIconName);
				select.SkillID = info.SkillData.skillID;
                select.GetListener().PointClike += Attake_PointClike;
            }



        }

        private void Attake_PointClike(PointerEventData obj)
        {
            SkillEventCenter .Attake_performedInvoke(obj);
        }
    }
}

