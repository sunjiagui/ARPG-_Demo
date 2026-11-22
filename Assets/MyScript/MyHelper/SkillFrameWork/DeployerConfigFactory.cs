using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ns
{
	///<summary>
	///
	///</summary>
	public class DeployerConfigFactory :MonoSingleton<DeployerConfigFactory>
	{
        /// <summary>
        /// 根据技能判断范围等生成一个能提供判定方法的对象
        /// </summary>
        /// <param name="skillData"></param>
        /// <returns></returns>
        private static Dictionary<string, object> cache;
        static DeployerConfigFactory()
        {
            cache = new();
        }
        public IAttakeCalculate CreatCalculateObject(SkillData skillData)
        {
            string AttakeSelectorName = string.Format("ns.{0}AttakeCalculate", skillData.AttakeType);
            //命名规范：命名空间+攻击种类枚举名+AttakeCalculate
            IAttakeCalculate attake = CreatObject<IAttakeCalculate>(AttakeSelectorName);
            return attake;
        }

       

        /// <summary>
        /// //返回技能效果对象数组
        /// </summary>
        /// <param name="skillData"></param>
        /// <returns></returns>
        public IImpactEffect[] CreatImpactObject(SkillData skillData)
        {
            if (skillData.skillImpacts == null || skillData.skillImpacts.Length == 0 ) return null;
            IImpactEffect[] impactEffects = new IImpactEffect[skillData.skillImpacts.Length];
            if (skillData.skillImpacts.Length <= 0)
            {
                print("未添加技能效果");
                return null;
            } 
            for (int i = 0; i < skillData.skillImpacts.Length; i++)
            {
                //类名规范：命名空间+数组中技能名字+Effect
                string skillObjectName = string.Format("ns.{0}Effect", skillData.skillImpacts[i]);
                IImpactEffect effect = CreatObject<IImpactEffect>(skillObjectName);
                impactEffects[i] = effect;
            }
            return impactEffects;
        }



        private T CreatObject<T>(string name) where T : class
        {
            if (cache.ContainsKey(name)) return cache[name] as T;
            Type type = Type.GetType(name);
            object attake = Activator.CreateInstance(type);
            cache.Add(name, attake);
            return attake as T;
        }



    }
}

