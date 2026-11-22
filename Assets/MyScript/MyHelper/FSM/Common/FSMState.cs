using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace MyFSM
{
    ///<summary>
    ///每个状态都会对应不同的条件所以需要有映射表（哪些条件会与该状态对应）当然还需要条件的对象（用于提供判断）还需要有每个状态的进入 当中 离开方法
    ///</summary>
    public abstract class FSMState
	{
        public StateID StateID { get; set; }
        public Dictionary<TriggerID, StateID> map;
        public List<FSMTrigger> triggers;
        public FSMState()
        {
            map = new();
            triggers = new();
            Init();
        }
        public abstract void Init();

        /// <summary>
        /// 判断是否切换状态
        /// </summary>
        public void Reason(FSMBase fsmBase)
        {
            foreach (var item in triggers)
            {
                if (item.IsTrigger(fsmBase))
                {
                    StateID stateID = map[item.TriggerID];
                    fsmBase.ChangeState(stateID);
                }
            }
            return;
        }
        public void AddMap(TriggerID triggerID, StateID stateID)
        {
            map.Add(triggerID, stateID);
            CreatTrigger(triggerID);
        }

        private void CreatTrigger(TriggerID triggerID)
        {
            //规范命名：MyFSM.枚举值+Trigger
            string name = string.Format("MyFSM." + triggerID.ToString() + "Trigger");
            Type type = Type.GetType(name);
            object target = Activator.CreateInstance(type);
            triggers.Add(target as FSMTrigger);
        }
        public virtual void EnterState(FSMBase fSMBase)
        {

        }
        public virtual void ActionState(FSMBase fSMBase)
        {

        }
        public virtual void ExitState(FSMBase fSMBase)
        {

        }
        
    }
}