using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFSM
{
    ///<summary>
    ///
    ///</summary>
    public class SawTargetTrigger : FSMTrigger
    {
        public override void Init()
        {
            TriggerID = TriggerID.SawTarget;
        }

        public override bool IsTrigger(FSMBase fSMBase)
        {
            return fSMBase.enemyNeedTarget != null;
        }
    }
}

