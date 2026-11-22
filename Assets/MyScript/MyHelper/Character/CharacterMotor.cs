using UnityEngine;

namespace ns
{
    /// <summary>
    /// 角色位移与朝向：
    /// - 移动方向 = 输入方向按相机水平朝向转换到世界空间（A 方案：朝移动方向转身）
    /// - 只有存在移动输入时才旋转角色，鼠标转视角不会影响角色
    /// - 位移沿输入方向走（不沿 transform.forward），转身只是视觉平滑
    /// </summary>
    public class CharacterMotor : MonoBehaviour
    {
        public float moveSpeed = 7f;
        public float rotateSpeed = 360f;   // 度/秒，越大转得越快
        public Transform mainCam;          // 相机（现在位于 CameraRig 下，不影响逻辑）

        private CharacterController _cc;

        private void Awake()
        {
            _cc = GetComponent<CharacterController>();
            if (mainCam == null && Camera.main != null)
                mainCam = Camera.main.transform;
        }

        // 由 PlayerInputEvent 每帧调用，传入摇杆/键盘二维输入
        public void SetMoveDir(Vector2 joyDir)
        {
            if (joyDir.sqrMagnitude < 0.0001f) return;

            Transform camT = mainCam != null ? mainCam : (Camera.main != null ? Camera.main.transform : null);
            Vector3 camF = camT != null ? Vector3.ProjectOnPlane(camT.forward, Vector3.up) : transform.forward;
            Vector3 camR = camT != null ? Vector3.ProjectOnPlane(camT.right, Vector3.up) : transform.right;
            camF.y = 0f; camR.y = 0f;
            if (camF.sqrMagnitude < 0.0001f) camF = transform.forward;
            if (camR.sqrMagnitude < 0.0001f) camR = transform.right;
            camF.Normalize();
            camR.Normalize();

            // 相机水平朝向 + 输入方向 => 期望移动方向
            Vector3 wish = camF * joyDir.y + camR * joyDir.x;
            if (wish.sqrMagnitude < 0.0001f) return;
            wish.Normalize();

            // 只在移动时朝移动方向缓慢转身
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                Quaternion.LookRotation(wish, Vector3.up),
                rotateSpeed * Time.deltaTime);

            // 位移沿输入方向（跟手）
            _cc.Move(moveSpeed * Time.deltaTime * wish);
        }
    }
}
