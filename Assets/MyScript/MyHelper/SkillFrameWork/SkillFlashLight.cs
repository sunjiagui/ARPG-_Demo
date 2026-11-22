using System.Collections;
using UnityEngine;

namespace ns
{
    /// <summary>
    /// 技能闪光：激活时让点光源从 peak 衰减到 0，增强打击感。
    /// 挂在带 Light 的子物体上，由 SkillEffectSequencer 在对应时间激活该组时触发。
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Light))]
    public class SkillFlashLight : MonoBehaviour
    {
        public float delay = 0f;
        public float peakIntensity = 8f;
        public float duration = 0.22f;
        public float range = 10f;

        private Light _light;
        private Coroutine _routine;

        private void Awake()
        {
            _light = GetComponent<Light>();
            if (_light != null)
            {
                _light.range = range;
                _light.intensity = 0f;
            }
        }

        private void OnEnable()
        {
            if (_light == null) _light = GetComponent<Light>();
            if (_light == null) return;
            if (_routine != null) StopCoroutine(_routine);
            _routine = StartCoroutine(Run());
        }

        private void OnDisable()
        {
            if (_routine != null)
            {
                StopCoroutine(_routine);
                _routine = null;
            }
            if (_light != null) _light.intensity = 0f;
        }

        private IEnumerator Run()
        {
            _light.intensity = 0f;
            if (delay > 0f) yield return new WaitForSeconds(delay);

            float t = 0f;
            while (t < duration)
            {
                t += Time.deltaTime;
                float k = 1f - Mathf.Clamp01(t / duration);
                _light.intensity = peakIntensity * k * k;
                yield return null;
            }
            _light.intensity = 0f;
            _routine = null;
        }
    }
}
