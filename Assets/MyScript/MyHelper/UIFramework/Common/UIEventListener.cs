using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ns
{
    ///<summary>
    /// ½»»¥ÔªËØ£¬£¬
    ///</summary>
    public class UIEventListener : MonoBehaviour , IPointerUpHandler ,IPointerDownHandler ,IPointerClickHandler
    {
        public event Action<PointerEventData> PointClike;
        public event Action<PointerEventData> PointUp;
        public event Action<PointerEventData> PointDown;

        public static UIEventListener GetUIEventListener(Transform tf)
        {
            if (tf.gameObject.TryGetComponent<UIEventListener>(out var target)) return target;
            return tf.gameObject.AddComponent<UIEventListener>();

            
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            PointClike? .Invoke (eventData);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            PointDown? .Invoke (eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            PointUp? .Invoke (eventData);
        }
    }
}

