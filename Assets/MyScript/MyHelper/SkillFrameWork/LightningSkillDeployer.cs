using UnityEngine;

namespace ns
{
    /// <summary>
    /// 雷电类技能：不移动，原地按范围选目标并施加效果。
    /// </summary>
    public class LightningSkillDeployer : SkillDeployer
    {
        public override void DeployerSkill()
        {
            CalculateTargets();
            ImpactTargets();
        }
    }
}
