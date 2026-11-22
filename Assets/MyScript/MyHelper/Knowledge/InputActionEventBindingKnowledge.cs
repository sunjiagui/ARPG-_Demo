using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ns
{
	///<summary>
	///
	///</summary>
	///
	
	public class InputActionEventBindingKnowledge : MonoBehaviour
	{
		//MyInputControls control;
		//CharacterMotor motor;

  //      private void Awake()
  //      {
  //          control = new();
  //          control.PlayMap.Enable();
  //          //PlayerInput input = GetComponent<PlayerInput>();
  //          //input.onActionTriggered += Input_onActionTriggered; //绑定事件 等号后面是自动生成的方法名


  //          //control.Player.Jump.performed += Jump_performed; //map中的什么操作的生命什么过程添加事件

  //          control.PlayMap.Movement.performed += Movement_performed;
		//	motor = GetComponent<CharacterMotor>();
  //      }

  //      private void Movement_performed(InputAction.CallbackContext context)
  //      {
		//	Vector2 vect = control.PlayMap.Movement.ReadValue<Vector2>();
		//	Vector3 targetVect = new Vector3(vect.x, 0, vect.y);
		//	motor.Move(targetVect );
  //      }

  //      private void FixedUpdate()
		//{
		//	Vector2 vect = control.Player.Movement.ReadValue<Vector2>();
		//	print(vect);
		//	Vector3 targetVect = new(vect.x, 0, vect.y);
		//	body.velocity = targetVect * num;
  //      }

		//private void Movement_performed(InputAction.CallbackContext context)
  //      {
		//	Vector2 vect = context.ReadValue<Vector2>();
		//	Vector3 targetVect = new Vector3 (vect.x, 0, vect.y);
		//	print(targetVect.y);
		//	body.AddForce(targetVect * num , ForceMode.Force);
			
  //      }

  //      private void Jump_performed(InputAction.CallbackContext context)
  //      {
		//	print(222);
			
  //      }

  //      private void Input_onActionTriggered(InputAction.CallbackContext context)
  //      {
		//	//相当于UI中通过代码绑定事件 ，首先获得物体上的playerInput组件然后绑定事件
		//	if (context.action.name == "Jump" && context .performed )
		//	{
  //              body.AddForce(Vector3.up * num, ForceMode.Acceleration);
  //              print(context.phase);
  //          }
  //      }

  //      public void OnJump(InputAction.CallbackContext context) //context Unity会自动传入
		//{
		//	//unity事件 相当于UI添加事件中的拽物体给方法
		//	if(context .performed )
		//	{
  //              body.AddForce(Vector3.up * num, ForceMode.Acceleration);
  //              print(context.phase);
  //          }
			
		//}
	}
}

