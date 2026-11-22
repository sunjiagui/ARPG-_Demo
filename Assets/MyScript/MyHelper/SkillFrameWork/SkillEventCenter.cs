using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace ns
{
	///<summary>
	///只关心将事件通知出去，
	///</summary>
	public static class SkillEventCenter 
	{
        public static event Action<PointerEventData> Attake_performed;
		
		public static event Action Move_performed;
		public static event Action Move_canceled;

		public static void Attake_performedInvoke(PointerEventData obj)
		{
			Attake_performed?.Invoke(obj);
        }

        
        public static void Move_performedInvoke()
        {
            Move_performed?.Invoke();
        }
        public static void Move_canceledInvoke()
        {
            Move_canceled?.Invoke();
        }
    }
}

