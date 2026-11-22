using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace ns
{
    ///<summary>
    ///以人物为中心 以技能数据攻击距离为判定
    ///</summary>
    public class SectorAttakeCalculate : IAttakeCalculate
    {
        GameObject[] theEnd;
        /// <summary>
        /// 将判定到的物体返回到技能数据中的attakeTargets
        /// </summary>
        /// <param name="skillDeployer"></param>
        /// <param name="SkillSelfPosition"></param>
        /// <returns></returns>

        public void Calculate(SkillDeployer skillDeployer, Transform SkillSelfPosition)
        {

            //所有敌人
            GameObject[] theFind = FindCloser(skillDeployer.SkillData);
            List<GameObject> finallyGet;
            finallyGet = new();
            //在技能范围内的敌人
            GameObject[] finallyGetArray;
            float attakeDistance = skillDeployer.SkillData.attakeDistance;
            foreach (var item in theFind)
            {
                if ((skillDeployer.SkillData.owner .transform .position - item.transform.position).magnitude <= attakeDistance) finallyGet.Add(item);
            }
            finallyGetArray = finallyGet.ToArray();
            skillDeployer.SkillData.attakeTargets = finallyGetArray;

        }


        //public GameObject FindTheCloser(SkillData data)
        //{
        //    for (int i = 0; i < data.attakeTags.Length; i++)
        //    {
        //        //获取场景所有敌人
        //        GameObject[] targets = GameObject.FindGameObjectsWithTag(data.attakeTags[i]);
        //        List<GameObject> contain = new();
        //        contain.AddRange<GameObject>(targets.ToList());
        //        theEnd = contain.ToArray();
        //    }
        //    //最近距离的敌人
        //    GameObject target = theEnd.GetMin<GameObject, float>(g => (g.transform.position - transform.position).sqrMagnitude);
        //    return target;
        //}
        public GameObject[] FindCloser(SkillData data)
        {
            for (int i = 0; i < data.attakeTags.Length; i++)
            {
                //获取场景所有敌人
                GameObject[] targets = GameObject.FindGameObjectsWithTag(data.attakeTags[i]);
                List<GameObject> contain = new();
                contain.AddRange<GameObject>(targets.ToList());
                theEnd = contain.ToArray();
            }
            return theEnd;
        }

    }
}

