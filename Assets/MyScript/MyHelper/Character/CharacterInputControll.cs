using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

namespace ns
{
    ///<summary>
    ///角色输入控制，只关心按钮绑定不关心逻辑
    ///</summary>
    public class CharacterInputControll : MonoBehaviour
    {
        public MyInputController inputControl;
        
        private void Awake()
        {
            inputControl = new();
            
        }            

        private void OnEnable()
        {
            //开启玩家输入
            inputControl.PlayMap.Enable();
            //玩家移动中阶段
            inputControl.PlayMap.Move.performed += Move_performed;
            inputControl.PlayMap.Move.canceled += Move_canceled;
            
        }

        private void OnDisable()
        {
            //玩家移动中阶段
            inputControl.PlayMap.Move.performed -= Move_performed;
            inputControl.PlayMap.Move.canceled -= Move_canceled;
            inputControl.PlayMap.Disable();
        }



        private void Move_performed(InputAction.CallbackContext obj)
        {
            SkillEventCenter.Move_performedInvoke();
        }

        private void Move_canceled(InputAction.CallbackContext obj)
        {
            SkillEventCenter.Move_canceledInvoke();
        }

        

    }
}
