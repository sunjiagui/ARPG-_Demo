using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ns
{
    ///<summary>
    ///释放器子类通过父类的判定影响效果来制定释放方式（利于协同并进式的开发）
    ///</summary>
    public class MeleeSkillDeployer : SkillDeployer
    {
        Vector3 targetPoint;
        


        public override void DeployerSkill()
        {
            //StartCoroutine(Move());
            CalculateTargets();
            ImpactTargets();
        }
        private IEnumerator Move()
        {
            targetPoint = transform.position + SkillData.attakeDistance * transform.forward;
            while((transform .position - targetPoint ).sqrMagnitude >= 0.3f)
            {
                transform .position = Vector3.MoveTowards(transform.position, targetPoint, Time.deltaTime * SkillData.moveSpeed);
                yield return new WaitForEndOfFrame();
            }
        }
    }
}

