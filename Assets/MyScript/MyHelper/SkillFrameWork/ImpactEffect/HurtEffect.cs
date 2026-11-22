using System.Collections;
using UnityEngine;

namespace ns
{
    ///<summary>
    /// 持续伤害效果：按 DamageInterval 造成多次伤害；0 间隔会自动抬到 0.05，避免每帧无限伤害。
    /// </summary>
    public class HurtEffect : IImpactEffect
    {
        public void ImpactEffect(SkillDeployer skillDeployer)
        {
            skillDeployer.StartCoroutine(ContinueHurt(skillDeployer));
        }

        private IEnumerator ContinueHurt(SkillDeployer skillDeployer)
        {
            SkillData data = skillDeployer.SkillData;
            float duration = Mathf.Max(0.1f, data.durationTime);
            float interval = Mathf.Max(0.05f, data.attakeDelta);
            float time = 0f;

            do
            {
                if (data.attakeTargets != null)
                    Hurt(data);

                yield return new WaitForSeconds(interval);
                time += interval;
                skillDeployer.CalculateTargets();
            } while (time <= duration);
        }

        private void Hurt(SkillData data)
        {
            CharacterStatus ownerStatus = data.owner != null ? data.owner.GetComponent<CharacterStatus>() : null;
            if (ownerStatus == null) return;

            float attake = ownerStatus.baseAttake * data.attakeRatio;
            for (int i = 0; i < data.attakeTargets.Length; i++)
            {
                GameObject target = data.attakeTargets[i];
                if (target == null) continue;

                if (target.TryGetComponent<PlayerStatus>(out var playerStatus))
                    playerStatus.Damage(attake);

                if (target.TryGetComponent<EnemyStatus>(out var enemyStatus))
                    enemyStatus.Damage(attake);
            }
        }
    }
}
