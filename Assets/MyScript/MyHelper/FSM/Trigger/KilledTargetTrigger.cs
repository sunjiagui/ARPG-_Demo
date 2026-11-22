using ns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFSM
{
    internal class KilledTargetTrigger : FSMTrigger
    {
        public override void Init()
        {
            TriggerID = TriggerID.KilledTarget;
        }

        public override bool IsTrigger(FSMBase fSMBase)
        {
            return fSMBase.enemyNeedTarget.GetComponent<PlayerStatus>().hp <= 0;
        }
    }
}
