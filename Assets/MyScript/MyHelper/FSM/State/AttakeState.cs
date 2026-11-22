using System.Collections;
using ns;
using UnityEngine;

namespace MyFSM
{
    public class AttakeState : FSMState
    {
        private Coroutine castRoutine;
        private CharacterSkillSystem skillSystem;

        public override void Init()
        {
            StateID = StateID.Attake;
        }

        public override void EnterState(FSMBase fSMBase)
        {
            base.EnterState(fSMBase);
            skillSystem = fSMBase.GetComponent<CharacterSkillSystem>();
            if (castRoutine != null) fSMBase.StopCoroutine(castRoutine);
            castRoutine = fSMBase.StartCoroutine(CastLoop(fSMBase));
        }

        public override void ActionState(FSMBase fSMBase)
        {
            base.ActionState(fSMBase);
        }

        public override void ExitState(FSMBase fSMBase)
        {
            if (castRoutine != null)
            {
                fSMBase.StopCoroutine(castRoutine);
                castRoutine = null;
            }
            skillSystem = null;
        }

        private IEnumerator CastLoop(FSMBase fSMBase)
        {
            // 起手稍微等一下，然后进入攻击状态后立即放一次，之后每 2 秒一次
            yield return new WaitForSeconds(0.3f);

            while (fSMBase != null && fSMBase.enemyNeedTarget != null)
            {
                if (skillSystem != null)
                {
                    skillSystem.SkillAttake(1);
                }
                yield return new WaitForSeconds(2f);
            }
        }
    }
}
