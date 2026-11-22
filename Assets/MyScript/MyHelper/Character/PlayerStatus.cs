using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ns
{
	///<summary>
	///
	///</summary>
	public class PlayerStatus : CharacterStatus 
	{
        public CharacterAnimatorParameters parameters;
        CharacterInputControll controll;
        private Animator anim;

        private void Start()
        {
            anim = GetComponent<Animator>();
            controll = GetComponent<CharacterInputControll>();
        }

        public void Damage(float damage)
        {
            if (hp <= 0) return;
            hp -= damage;
            if (hp <= 0) Dead();
        }

        private void Dead()
        {
            //播放动画，消除物体
            anim.SetTrigger(parameters.die);
            Destroy(gameObject, 3f);
        }

    }
}

