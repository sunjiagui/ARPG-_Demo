using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ns
{
    ///<summary>
    ///普工连击类,输入控制类调用NormalAttake()方法，并加入动画事件就OK
    ///</summary>
    public class NormalAttakeCombo : MonoBehaviour
    {
        public bool isAttake = false;//攻击状态
        public bool attakeCommend;//攻击开关
        float timeDelta = 0;//积累时间
        readonly float windowTime = 0.2f;//窗口时间  窗口预留时间要小 不然等待Attake2的时间太长（导致调用了Attake2的方法但是动画回到了对话动画此时就不会调用动画事件也就不会有重置方法）
        CharacterSkillSystem skillSystem;
        public int[] attakeID;//攻击代号顺序输入
        int currentSkillIDCount = 0;
        Animator anim;
        AnimationEvent animationEvent;
        SkillData data;
        public void NormalAttake()
        {
            attakeCommend = true;
        }
        public void NormalAttakeEnd()
        {
            attakeCommend = false;
        }
        private void Awake()
        {
            skillSystem = GetComponent<CharacterSkillSystem>();
            anim = GetComponent<Animator>();
            animationEvent = GetComponent<AnimationEvent>();
        }
        //private void OnEnable()
        //{
        //    animationEvent.OnAttakeEndEvent += AttakeEnd;
        //}
        //private void OnDisable()
        //{
        //    animationEvent.OnAttakeEndEvent -= AttakeEnd;
        //}
        //private void AttakeEnd()//动画结束事件
        //{
        //    anim.SetBool(data.skillAnimationName, false);
        //    StartCoroutine(WindowTimerLog());
        //}

        private void SkillAttake()
        {
            print("进攻" + currentSkillIDCount);
            isAttake = true;

            int skillID = attakeID[currentSkillIDCount];
            data = skillSystem.SkillAttake(skillID);
        }

        private IEnumerator WindowTimerLog()
        {
            while (timeDelta <= windowTime)
            {
                yield return null;
                timeDelta += Time.deltaTime;
                if (attakeCommend)
                {
                    currentSkillIDCount = (currentSkillIDCount + 1) % attakeID.Length;
                    print("count增加后值:" + currentSkillIDCount);
                    attakeCommend = true;
                    isAttake = false;
                    yield break;
                }
            }
            ResetMain();
        }
        private void Update()
        {
            if (!isAttake && attakeCommend)
            {
                SkillAttake();
            }

        }
        private void ResetMain()
        {
            currentSkillIDCount = 0;
            isAttake = false;
            timeDelta = 0;
            print("重置方法当前count:" + currentSkillIDCount);
        }



    }
}


