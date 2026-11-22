using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFSM
{
    ///<summary>
    ///
    ///</summary>
    public class NoHealthTrigger : FSMTrigger
    {
        public override void Init()
        {
            TriggerID = TriggerID.NoHealth;
        }

        public override bool IsTrigger(FSMBase fSMBase)
        {
            return fSMBase.enemyStatus.hp <= 0;
        }
    }
}

