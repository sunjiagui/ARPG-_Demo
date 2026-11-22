using UnityEngine;
using UnityEngine.UI;

namespace ns
{
    /// <summary>
    /// 怪物头顶血条：World Space Canvas，跟随 EnemyStatus.hp 平滑变化，并朝向主相机。
    /// </summary>
    public class EnemyHealthBar : MonoBehaviour
    {
        [Header("引用")]
        public Image fillImage;
        public Image frameImage;

        [Header("表现")]
        public bool faceCamera = true;
        public float smoothSpeed = 4f;
        public Color highColor = new Color(0.30f, 0.90f, 0.55f);
        public Color midColor = new Color(0.95f, 0.75f, 0.25f);
        public Color lowColor = new Color(0.95f, 0.25f, 0.22f);

        private EnemyStatus status;
        private float maxHp;
        private float displayed = 1f;

        private void Awake()
        {
            status = GetComponentInParent<EnemyStatus>();
            if (status == null)
            {
                Debug.LogWarning("[EnemyHealthBar] missing EnemyStatus in parents");
                return;
            }
            maxHp = status.maxHp > 0f ? status.maxHp : status.hp;
            if (maxHp <= 0f) maxHp = 1f;
            displayed = Mathf.Clamp01(status.hp / maxHp);
            Apply(displayed);
        }

        private void LateUpdate()
        {
            if (status == null) return;

            float target = Mathf.Clamp01(status.hp / maxHp);
            displayed = Mathf.MoveTowards(displayed, target, Time.deltaTime * smoothSpeed);
            Apply(displayed);

            if (faceCamera && Camera.main != null)
                transform.rotation = Camera.main.transform.rotation;
        }

        private void Apply(float value)
        {
            if (fillImage != null)
            {
                fillImage.fillAmount = value;
                fillImage.color = value >= 0.5f
                    ? Color.Lerp(midColor, highColor, (value - 0.5f) * 2f)
                    : Color.Lerp(lowColor, midColor, value * 2f);
            }
            if (frameImage != null)
            {
                Color c = frameImage.color;
                c.a = Mathf.Lerp(0.55f, 1f, value);
                frameImage.color = c;
            }
        }
    }
}
