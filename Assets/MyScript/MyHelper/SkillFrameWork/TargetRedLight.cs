using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace ns
{
    ///<summary>
    ///
    ///</summary>
    public class TargetRedLight : MonoSingleton<TargetRedLight>
    {
        private GameObject _currentHighlightObj;
        // 保存高亮协程，方便打断
        private Coroutine _highlightCoroutine;

        // 高亮颜色
        public Color highlightColor = Color.red;
        // 高亮时长
        public float highlightTime = 5f;

        /// <summary>
        /// 外部调用这个方法来高亮物体
        /// </summary>
        public void HighlightObject(GameObject target)
        {
            // 1. 如果已经有高亮物体，先取消它
            if (_currentHighlightObj != null)
            {
                ResetObjectColor(_currentHighlightObj);
            }

            // 2. 如果之前有协程在跑，停止它
            if (_highlightCoroutine != null)
            {
                StopCoroutine(_highlightCoroutine);
            }

            // 3. 设置新的高亮物体
            _currentHighlightObj = target;

            if (target != null)
            {
                SetObjectColor(target, highlightColor);
                // 开启新的5秒计时
                _highlightCoroutine = StartCoroutine(HighlightTimer());
            }
        }

        /// <summary>
        /// 5秒后自动取消高亮
        /// </summary>
        private IEnumerator HighlightTimer()
        {
            yield return new WaitForSeconds(highlightTime);

            // 时间到，恢复颜色
            if (_currentHighlightObj != null)
            {
                ResetObjectColor(_currentHighlightObj);
            }

            _currentHighlightObj = null;
            _highlightCoroutine = null;
        }

        // 设置物体颜色（假设用Standard材质）
        private void SetObjectColor(GameObject obj, Color color)
        {
            var renderer = obj.GetComponent<MeshRenderer>();
            if (renderer != null)
            {
                renderer.material.color = color;
            }
        }

        // 恢复原来的颜色（白色/默认）
        private void ResetObjectColor(GameObject obj)
        {
            var renderer = obj.GetComponent<MeshRenderer>();
            if (renderer != null)
            {
                renderer.material.color = Color.white;
            }
        }
    }
}

