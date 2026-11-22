using UnityEngine;

namespace ns
{
    ///<summary>
    /// 敌人状态：血量、视野、攻击距离、移动/转向速度。
    /// </summary>
    public class EnemyStatus : CharacterStatus
    {
        public float EnemySawDistance;
        public float EnemyAttakeDistance;
        public float enemyMoveSpeed;
        public float enemyRotateSpeed;

        /// <summary>最大生命值，血条用；0 时自动取初始 hp。</summary>
        public float maxHp;

        public CharacterAnimatorParameters parameters;

        private Animator anim;

        private void Awake()
        {
            if (maxHp <= 0f) maxHp = hp;
        }

        private void Start()
        {
            anim = GetComponent<Animator>();
        }

        public void Damage(float damage)
        {
            if (hp <= 0f) return;
            hp -= damage;
            if (hp <= 0f) Dead();
        }

        private void Dead()
        {
            if (anim != null && parameters != null)
                anim.SetTrigger(parameters.die);
            Destroy(gameObject, 5f);
        }
    }
}
