using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFSM
{
    public class ExitAttakeRangeTrigger : FSMTrigger
    {
        public override void Init()
        {
            TriggerID = TriggerID.ExitAttakeRange;
        }

        public override bool IsTrigger(FSMBase fSMBase)
        {
            if (fSMBase.enemyNeedTarget != null)
            {
                return (fSMBase.enemyNeedTarget.position - fSMBase.transform.position).magnitude > fSMBase.enemyStatus.EnemyAttakeDistance;
            }
            else return false;
        }
    }
}
