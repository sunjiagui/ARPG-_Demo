using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFSM
{
    public class CompletePatrolTrigger : FSMTrigger
    {
        public override void Init()
        {
            TriggerID = TriggerID.CompletePatrol;
        }

        public override bool IsTrigger(FSMBase fSMBase)
        {
            return false;
        }
    }
}
