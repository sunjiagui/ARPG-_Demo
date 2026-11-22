using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace MyFSM
{
    ///<summary>
    ///
    ///</summary>
    public class DeadState : FSMState
    {
        public override void Init()
        {
            StateID = StateID.Dead;
        }
        public override void EnterState(FSMBase fSMBase)
        {
            base.EnterState(fSMBase);
            fSMBase.enabled = false;
        }
    }
}

