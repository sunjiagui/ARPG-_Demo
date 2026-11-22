using MyFSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFSM
{
    ///<summary>
    ///
    ///</summary>
    public class IdleState : FSMState
    {
        public override void Init()
        {
            StateID = StateID.Idle;
        }
        public override void EnterState(FSMBase fSMBase)
        {
            base.EnterState(fSMBase);
        }
    }
}

