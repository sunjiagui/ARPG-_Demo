using UnityEngine;
using UnityEngine.InputSystem;

namespace ns
{
    /// <summary>
    /// 第三人称相机（脚本直接挂在相机物体上）。
    ///
    /// 预制件结构：
    ///   HeroRoot（空物体，不旋转）
    ///   ├─ Hero_zhanshi / Hero_sorceress（角色根，会旋转）
    ///   └─ View_Camera_xxx（本脚本 + Camera + AudioListener）
    ///
    /// 相机与角色平级，不在旋转节点下：鼠标右键拖动只改相机朝向；
    /// 每帧只做位置跟随（跟随相机必需），不再需要“世界旋转补偿”。
    /// </summary>
    [DisallowMultipleComponent]
    public class ThirdPersonCameraController : MonoBehaviour
    {
        [Header("引用")]
        public Transform target;        // 角色根（Hero_zhanshi / Hero_sorceress）
        public Camera cam;              // 自己（Camera 组件）

        [Header("位置")]
        public float pivotHeight = 1.6f;   // 相机轴心高度（相对角色脚底）
        public float distance = 3.6f;      // 相机到轴心的距离
        public float minDistance = 2.0f;
        public float maxDistance = 6.0f;
        public float zoomStep = 0.4f;

        [Header("鼠标视角")]
        public bool rightMouseOnly = true;     // true：按住右键拖动才转视角
        public float yawSensitivity = 0.15f;   // 度/像素
        public float pitchSensitivity = 0.12f;
        public float minPitch = 5f;            // 低头最大角度
        public float maxPitch = 70f;
        public bool invertY = false;
        public bool lockCursorWhileLooking = true;
        public bool enableZoom = true;

        [Header("相机避障（可选）")]
        public bool cameraCollision = false;
        public LayerMask collisionMask = ~0;
        public float collisionPadding = 0.2f;

        private float _yaw;
        private float _pitch;
        private bool _wasLooking;
        private bool _rotDirty = true;
        private float _appliedDistance = 1f;
        private CursorLockMode _prevLockMode;
        private bool _prevCursorVisible;

        private void Awake()
        {
            if (target == null) target = FindHeroSibling();
            if (cam == null) cam = GetComponent<Camera>();

            _yaw = target != null ? target.eulerAngles.y : 0f;
            _pitch = Mathf.Clamp(18f, minPitch, maxPitch);
            _appliedDistance = distance;
            ApplyPose();
        }

        private Transform FindHeroSibling()
        {
            if (transform.parent == null) return null;
            foreach (Transform sibling in transform.parent)
            {
                if (sibling != transform && sibling.GetComponent<CharacterController>() != null)
                    return sibling;
            }
            return null;
        }

        private void Update()
        {
            var mouse = Mouse.current;
            if (mouse == null || target == null) return;

            bool looking = rightMouseOnly ? mouse.rightButton.isPressed : true;

            if (looking)
            {
                Vector2 delta = mouse.delta.ReadValue();
                if (delta.sqrMagnitude > 0.0001f)
                {
                    _yaw += delta.x * yawSensitivity;
                    _pitch += (invertY ? delta.y : -delta.y) * pitchSensitivity;
                    _pitch = Mathf.Clamp(_pitch, minPitch, maxPitch);
                    _rotDirty = true;
                }

                if (enableZoom)
                {
                    float scroll = mouse.scroll.ReadValue().y;
                    if (Mathf.Abs(scroll) > 0.01f)
                        distance = Mathf.Clamp(distance - Mathf.Sign(scroll) * zoomStep, minDistance, maxDistance);
                }
            }

            if (lockCursorWhileLooking && looking != _wasLooking)
            {
                if (looking)
                {
                    _prevLockMode = Cursor.lockState;
                    _prevCursorVisible = Cursor.visible;
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }
                else
                {
                    Cursor.lockState = _prevLockMode;
                    Cursor.visible = _prevCursorVisible;
                }
            }
            _wasLooking = looking;
        }

        private void LateUpdate()
        {
            if (target == null) return;

            // 朝向只在鼠标动过时写一次（相机不在旋转父级下，不需要每帧补偿）
            if (_rotDirty)
            {
                transform.rotation = Quaternion.Euler(_pitch, _yaw, 0f);
                _rotDirty = false;
            }

            Vector3 pivot = target.position + Vector3.up * pivotHeight;
            float finalDistance = distance;

            if (cameraCollision)
            {
                Vector3 dir = -transform.forward;
                if (Physics.SphereCast(pivot, 0.15f, dir, out RaycastHit hit, distance + 0.05f,
                        collisionMask, QueryTriggerInteraction.Ignore))
                {
                    if (!hit.transform.IsChildOf(target))
                        finalDistance = Mathf.Clamp(hit.distance - collisionPadding, minDistance * 0.5f, distance);
                }
            }

            _appliedDistance = finalDistance;
            // 每帧只跟随位置（跟随相机必需）：轴心 - 当前朝向 * 距离
            transform.position = pivot - transform.forward * finalDistance;
        }

        private void ApplyPose()
        {
            transform.rotation = Quaternion.Euler(_pitch, _yaw, 0f);
            if (target != null)
                transform.position = target.position + Vector3.up * pivotHeight - transform.forward * distance;
        }

        private void OnDisable()
        {
            if (_wasLooking && lockCursorWhileLooking)
            {
                Cursor.lockState = _prevLockMode;
                Cursor.visible = _prevCursorVisible;
                _wasLooking = false;
            }
        }

        /// <summary>当前视角的水平朝向（度）</summary>
        public float Yaw { get { return _yaw; } }
        public bool IsLooking { get { return _wasLooking; } }
    }
}
