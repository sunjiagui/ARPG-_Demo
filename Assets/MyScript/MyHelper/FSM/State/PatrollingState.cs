using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFSM
{
    public class PatrollingState : FSMState
    {
        public override void Init()
        {
            StateID = StateID.Patrolling;
        }
        public override void EnterState(FSMBase fSMBase)
        {
            
        }
        public override void ExitState(FSMBase fSMBase)
        {
            
            
        }

    }
}
