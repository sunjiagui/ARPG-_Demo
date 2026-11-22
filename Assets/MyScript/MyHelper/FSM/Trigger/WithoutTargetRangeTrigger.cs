using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFSM
{
    /// <summary>
    /// 丢失目标
    /// </summary>
    public class WithoutTargetRangeTrigger : FSMTrigger
    {
        public override void Init()
        {
            TriggerID = TriggerID.WithoutTargetRange;
        }

        public override bool IsTrigger(FSMBase fSMBase)
        {
            return fSMBase.enemyNeedTarget == null;
        }
    }
}
