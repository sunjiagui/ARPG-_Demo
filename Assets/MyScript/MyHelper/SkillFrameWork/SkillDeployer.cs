using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ns
{
	///<summary>
	///技能释放器
	///</summary>
	public abstract class SkillDeployer : MonoBehaviour
	{
		protected IAttakeCalculate selector;
        protected IImpactEffect[] effects;
        private SkillData skillData;
		public SkillData SkillData
		{
			get { return skillData; }
			set
            {
                skillData = value;
                InitDeployer();
            }
		}
        /// <summary>
        /// 初始化技能释放器
        /// </summary>
        private void InitDeployer()
        {
            selector = DeployerConfigFactory.Instance.CreatCalculateObject(skillData);
            effects = DeployerConfigFactory.Instance.CreatImpactObject(skillData);
        }
        public void CalculateTargets()
        {
            selector.Calculate(this, transform);
        }
        public void ImpactTargets()
        {
            if (effects == null) return;
            for(int i = 0; i < effects.Length; i++)
            {

                effects[i].ImpactEffect(this);
            }
        }

        public abstract void DeployerSkill();//将复杂的技能判定效果逻辑交给子类完善符合开闭原则，，一个技能一个释放器
        
        
    }
}

