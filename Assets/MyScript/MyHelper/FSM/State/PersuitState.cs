using ns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFSM
{
    ///<summary>
    ///×·Öð×´Ì¬
    ///</summary>
    public class PersuitState : FSMState
    {
        public string persuitNeedTags = "Player";
        public override void Init()
        {
            StateID = StateID.Persuit;
        }

        public override void EnterState(FSMBase fSMBase)
        {
            base.EnterState(fSMBase);
            
        }
        public override void ActionState(FSMBase fSMBase)
        {
            base.ActionState(fSMBase);
            if (fSMBase.enemyNeedTarget != null) 
            {
                Quaternion quaternion = Quaternion.LookRotation(fSMBase.enemyNeedTarget.position - fSMBase.enemyStatus.transform.position);
                fSMBase.transform.rotation = Quaternion.RotateTowards(fSMBase.transform.rotation, quaternion, Time.deltaTime * fSMBase.enemyStatus.enemyRotateSpeed);
                fSMBase.enemyController.Move(fSMBase.enemyStatus.enemyMoveSpeed * Time.deltaTime * (fSMBase.enemyNeedTarget.position - fSMBase.enemyStatus.transform.position));                
            }
        }
        
        

    }
}

