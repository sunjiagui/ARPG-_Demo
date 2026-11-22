using System;
using System.Collections;
using UnityEngine;

namespace ns
{
    /// <summary>
    /// 技能表现的时间轴：按 delay 依次激活子物体组。
    /// 挂在技能 prefab 根上；子物体分组（Start / Impact / Linger）初始可以保持 active，
    /// 运行时由本脚本把有 delay 的组先隐藏，再按时间打开。
    /// </summary>
    [DisallowMultipleComponent]
    public class SkillEffectSequencer : MonoBehaviour
    {
        [Serializable]
        public class Step
        {
            public string name;
            public float delay;
            public GameObject target;
        }

        public Step[] steps;

        private Coroutine _routine;

        private void OnEnable()
        {
            if (steps == null || steps.Length == 0) return;
            StopAll();
            ResetStates();
            _routine = StartCoroutine(RunTimeline());
        }

        private void OnDisable()
        {
            StopAll();
        }

        private void StopAll()
        {
            if (_routine != null)
            {
                StopCoroutine(_routine);
                _routine = null;
            }
        }

        private void ResetStates()
        {
            for (int i = 0; i < steps.Length; i++)
            {
                if (steps[i] != null && steps[i].target != null)
                {
                    bool active = steps[i].delay <= 0.0001f;
                    steps[i].target.SetActive(active);
                }
            }
        }

        private IEnumerator RunTimeline()
        {
            float elapsed = 0f;
            // 拷一份，避免运行时被改
            var items = (Step[])steps.Clone();
            Array.Sort(items, (a, b) => a.delay.CompareTo(b.delay));

            for (int i = 0; i < items.Length; i++)
            {
                var step = items[i];
                if (step == null || step.target == null) continue;

                if (step.delay > elapsed)
                {
                    yield return new WaitForSeconds(step.delay - elapsed);
                    elapsed = step.delay;
                }

                step.target.SetActive(true);
                PlayAllParticles(step.target);
            }
        }

        private static void PlayAllParticles(GameObject root)
        {
            var systems = root.GetComponentsInChildren<ParticleSystem>(true);
            for (int i = 0; i < systems.Length; i++)
            {
                systems[i].Clear(true);
                systems[i].Play(true);
            }
            var animations = root.GetComponentsInChildren<Animation>(true);
            for (int i = 0; i < animations.Length; i++)
            {
                if (animations[i].clip != null) animations[i].Play();
            }
        }
    }
}
