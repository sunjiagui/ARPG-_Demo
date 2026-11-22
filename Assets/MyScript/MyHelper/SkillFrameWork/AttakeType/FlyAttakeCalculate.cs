using System.Collections.Generic;
using UnityEngine;

namespace ns
{
    ///<summary>
    /// 以目标为中心的范围选择：先按 tag 找出全部候选，再按技能距离过滤。
    /// </summary>
    public class FlyAttakeCalculate : IAttakeCalculate
    {
        public void Calculate(SkillDeployer skillDeployer, Transform SkillSelfPosition)
        {
            GameObject[] theFind = FindCloser(skillDeployer.SkillData);
            List<GameObject> finallyGet = new List<GameObject>();
            float attakeDistance = skillDeployer.SkillData.attakeDistance;
            foreach (var item in theFind)
            {
                if (item == null) continue;
                if ((SkillSelfPosition.position - item.transform.position).magnitude <= attakeDistance)
                    finallyGet.Add(item);
            }
            skillDeployer.SkillData.attakeTargets = finallyGet.ToArray();
        }

        public GameObject[] FindCloser(SkillData data)
        {
            List<GameObject> contain = new List<GameObject>();
            if (data.attakeTags == null) return contain.ToArray();

            foreach (var tag in data.attakeTags)
            {
                if (string.IsNullOrEmpty(tag)) continue;
                GameObject[] targets = GameObject.FindGameObjectsWithTag(tag);
                for (int i = 0; i < targets.Length; i++)
                {
                    if (targets[i] != null && !contain.Contains(targets[i]))
                        contain.Add(targets[i]);
                }
            }
            return contain.ToArray();
        }
    }
}
