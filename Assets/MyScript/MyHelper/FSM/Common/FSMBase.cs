using ns;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace MyFSM
{
	///<summary>
	///
	///</summary>
	public class FSMBase : MonoBehaviour
	{
        #region 状态机本身
        [Header("选择默认状态但不要选Default")]
        public StateID defaultStateID;

        [Header ("不用更改方便编译器查看当前状态")]
        public StateID currentStateID;


        private FSMState defaultState;
        private List<FSMState> states;
        private FSMState currentState;

        #region 状态机配置
        [Header ("状态机需要的配置文件（要加后缀）")]
        public string fileName;
        FSMStateConfigFileHelperFactory help;
        /// <summary>
        /// 配置状态机
        /// </summary>
        private void ConfigBase()
        {
            Dictionary<string, Dictionary<string, string>> map = help.GetMap(fileName);
            foreach (var itemCoin in map)
            {
                //命名规则 ：命名空间 + . + 枚举名 + state
                Type type = Type.GetType("MyFSM." + itemCoin.Key + "State");
                FSMState state = Activator.CreateInstance(type) as FSMState;
                states.Add(state);
                
                foreach (var partMap in map[itemCoin.Key])
                {
                    TriggerID triggerID = (TriggerID)Enum.Parse(typeof(TriggerID), partMap.Key);
                    StateID stateID = (StateID)Enum.Parse(typeof(StateID), partMap.Value);
                    state.AddMap(triggerID, stateID);
                }
            }

        }
        private IEnumerator Start()
        {
            states = new();
            help = new FSMStateConfigFileHelperFactory();
            //获取配置图表
            yield return StartCoroutine(help.Initial(fileName));
            //解析图表
            ConfigBase();
            //设置初始状态
            InitState();
            //拿到状态机所需要的组件
            GetComponentMain();
        }
        private void Update()
        {
            //等待配置加载完成
            if(help.isInit)
            {
                //调试时方便查看状态
                currentStateID = currentState.StateID;
                //每帧判断状态
                currentState.Reason(this);
                //做该状态的任务
                currentState.ActionState(this);

                //追逐状态需要持续搜寻人物
                foreach (string item in enemyNeedTags)
                {
                    FindTarget(item);
                }
            }
        }
            
        #endregion
        public void InitState()
        {
            defaultState = states.Find(s => s.StateID == defaultStateID);
            currentState = defaultState;
            currentState.EnterState(this);
        }
        /// <summary>
        /// 每帧判断条件，执行状态方法，判断状态切换
        /// </summary>
        public void ChangeState(StateID stateID)
        {
            currentState.ExitState(this);
            currentState = stateID == StateID.Default ? currentState = defaultState : states.Find(s => s.StateID == stateID);
            currentState.EnterState(this);
        }
        #endregion


        #region 以后多个状态会调所以在这状态机类只做一次 减少性能消耗为子类提供帮助，减少代码重复，虽然违反开闭：频繁修改一个类
        [Header ("敌人需要找的目标")]
        public string[] enemyNeedTags; //敌人找的点的标签
        //[HideInInspector]
        //public CharacterSkillSystem enemySkillSystem;//敌人技能系统
        [HideInInspector]
        public EnemyStatus enemyStatus;//敌人状态信息
        [HideInInspector]
        public CharacterController enemyController;//敌人移动马达

        [HideInInspector]
        public Transform enemyNeedTarget;// 敌人需要找的点
        
        
       
        /// <summary>
        /// 这个地方会频繁修改违背开闭原则，但是以后子类可能会有很多需要组件的，每个子类中获得组件会消耗更多性能，所以违背开闭原则保证性能
        /// </summary>
        private void GetComponentMain()
		{
            enemyStatus = GetComponent<EnemyStatus>();
            enemyController = GetComponent<CharacterController>();
            //enemySkillSystem = GetComponent<CharacterSkillSystem>();
        }

        private void FindTarget(string targetTag)
        {
            GameObject target = GameObject.FindWithTag(targetTag);
            if (target != null)
            {
                if ((target.transform.position - transform.position).magnitude < enemyStatus.EnemySawDistance) enemyNeedTarget = target.transform;
                else enemyNeedTarget = null;
            }
            
        }

        #endregion
    }
}

