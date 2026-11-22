using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace ns
{
	///<summary>
	///负责将技能逻辑订阅到事件中行(需要角色的技能系统以及事件中心)
	///</summary>
	public class PlayerInputEvent : MonoBehaviour
	{
		CharacterSkillSystem skillSystem;
        CharacterMotor motor;
        Animator anim;
        CharacterInputControll control;
        PlayerStatus playerStatus;
        bool attakeCommend = true;//攻击指令
        bool isMove;
        Vector2 direction;


        private void Awake()
        {
            anim = GetComponent<Animator>();
            motor = GetComponent<CharacterMotor>();
            skillSystem = GetComponent<CharacterSkillSystem>();
            control = GetComponent<CharacterInputControll>();
            playerStatus = GetComponent<PlayerStatus>();
            
        }

        private void OnEnable()
        {

            SkillEventCenter.Attake_performed += SkillAttake;

            SkillEventCenter.Move_performed += EventCenter_Move_performed;
            SkillEventCenter.Move_canceled += EventCenter_Move_canceled;
        }

        

        private void OnDisable()
        {
            SkillEventCenter.Attake_performed -= SkillAttake;
            
            SkillEventCenter.Move_performed -= EventCenter_Move_performed;
            SkillEventCenter.Move_canceled -= EventCenter_Move_canceled;
            
        }

        

        private void SkillAttake(PointerEventData obj)
        {
            SkillSelect select = obj.pointerClick.GetComponent<SkillSelect>();
            //判断攻击指令
            if (!attakeCommend) return;
            SkillData data = skillSystem.SkillAttake(select .SkillID);
            if (data != null) StartCoroutine(SetCommendFalseForWhatTime(data.whenTimeCanInvokeOther));//防止多次释放
            
        }

       

        private void EventCenter_Move_canceled()
        {
            CancelMove();
        }

        private void EventCenter_Move_performed()
        {
            StartMove();
        }

        
        

        private void CancelMove()
        {
            if (anim != null) anim.SetBool(playerStatus.parameters.run, false);
            isMove = false;
        }
        private void StartMove()
        {
            isMove = true;
            if (anim != null) anim.SetBool(playerStatus.parameters.run, true);
        }

        IEnumerator SetCommendFalseForWhatTime(float deltaTime)
        {
            attakeCommend = false;
            yield return new WaitForSeconds(deltaTime);
            attakeCommend = true;
        }



        private void Update()
        {
            //每帧移动
            MoveMain();
        }

        private void MoveMain()
        {
            if (!isMove) return;
            direction = control.inputControl.PlayMap.Move.ReadValue<Vector2>(); 
            if (motor != null) motor.SetMoveDir(direction);
        }

    }
}

