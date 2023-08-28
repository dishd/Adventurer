using ARPGDemo.Skill;
using ARPGDemo01.Character;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AI.FSM
{
    /// <summary>
    /// 状态机
    /// </summary>
    [RequireComponent(typeof(EnemyStatus),typeof(NavMeshAgent), typeof(CharacterSkillSystem))]
    public class FSMBase : MonoBehaviour
    {
        #region 状态机自生成员
        // 状态类别
        private List<FSMState> states;

        [Tooltip("默认状态编号")]
        public FSMStateID defaultStateID;

        [Tooltip("当前状态机使用的配置文件")]
        public string fileName = "AI_01.txt";

        //当前状态
        //[SerializeField]
        public FSMState currentState;

        // 默认状态
        private FSMState defaultState;


        private void InitDefaultState()
        {
            // 查找默认状态
            defaultState = states.Find(s => s.StateID == defaultStateID);
            // 为当前状态赋值
            currentState = defaultState;
            // 进入状态
            currentState.EnterState(this);
        }

        // 通过配置文件
        private void ConfigFSM()
        {
            states = new List<FSMState>();
            // 每个状态机创建一个读取器对象
            //var map = new AIConfigurationReader(fileName).Map;
            // 每个文件创建一个读取器对象
            var map = AIConfigurationReaderFactory.GetMap(fileName);

            // 大字典 --> 状态
            // 小字典 --> 映射

            foreach (var state in map)
            {
                //item.Key 状态名称
                //item.Value 映射
                Type type = Type.GetType("AI.FSM." + state.Key + "State");
                FSMState stateObj = Activator.CreateInstance(type) as FSMState;
                states.Add(stateObj);

                foreach (var dic in state.Value)
                {
                    // dic.Key  条件编号
                    // idc.Value 状态编号
                    // string --> Enum
                    FSMTriggerID triggerID = (FSMTriggerID)Enum.Parse(typeof(FSMTriggerID), dic.Key);
                    FSMStateID stateID = (FSMStateID)Enum.Parse(typeof(FSMStateID), dic.Value);
                    // 添加映射
                    stateObj.AddMap(triggerID, stateID);
                }
            }

        }

        // 配置状态机
        // 硬编码
        //private void ConfigFSM()
        //{

        //    states = new List<FSMState>();
        //    // 配置状态机
        //    // --创建状态对象
        //    IdleState idle = new IdleState();
        //    // --设置状态(AddMap)
        //    idle.AddMap(FSMTriggerID.NoHealth, FSMStateID.Dead);
        //    idle.AddMap(FSMTriggerID.SawTarget, FSMStateID.Pursuit);
        //    // ---加入状态机
        //    states.Add(idle);

        //    DeadState dead = new DeadState();
        //    states.Add(dead);

        //    PursuitState pursuit = new PursuitState();
        //    pursuit.AddMap(FSMTriggerID.NoHealth, FSMStateID.Dead);
        //    pursuit.AddMap(FSMTriggerID.ReachTarget, FSMStateID.Attacking);
        //    pursuit.AddMap(FSMTriggerID.LoseTarget, FSMStateID.Default);

        //    states.Add(pursuit);

        //    AttackingState attacking = new AttackingState();
        //    attacking.AddMap(FSMTriggerID.NoHealth, FSMStateID.Dead);
        //    attacking.AddMap(FSMTriggerID.WithoutAttackRange, FSMStateID.Pursuit);
        //    attacking.AddMap(FSMTriggerID.KilledTarget, FSMStateID.Default);

        //    states.Add(attacking);

        //    PatrollingState patrolling = new PatrollingState();
        //    patrolling.AddMap(FSMTriggerID.NoHealth, FSMStateID.Dead);
        //    patrolling.AddMap(FSMTriggerID.SawTarget, FSMStateID.Pursuit);
        //    patrolling.AddMap(FSMTriggerID.CompletePatrol, FSMStateID.Idle);

        //    states.Add(patrolling);

        //}



        // 切换状态 
        public void ChangeActiveState(FSMStateID stateID)
        {
            // FSMStateID.Default
            //设置当前状态
            // 如果需要切换的状态编号是 : Default,则直接返回默认状态
            // 否则从状态列表中查找 
            //if (stateID == FSMStateID.Default)
            //        currentState = defaultState;
            //else
            //        currentState = states.Find(s => s.StateID == stateID);

            // 离开上一个状态
            currentState.ExitState(this);
            // 切换状态
            currentState = stateID == FSMStateID.Default ? defaultState : states.Find(s => s.StateID == stateID);
            // 进入下一个状态
            currentState.EnterState(this);
        }


        #endregion

        #region 脚本生命周期
        private void Start()
        {
            ConfigFSM();
            InitComponent();
            InitDefaultState();
        }

        public FSMStateID test_CurrentStateID;
        // 每帧处理的逻辑
        public void Update()
        {
            test_CurrentStateID = currentState.StateID;
            // 判断当前状态条件
            currentState.Reason(this);
            //执行当前状态逻辑
            currentState.ActionState(this);
            SearchTarget();
        }
        #endregion 

        #region 为状态与及条件提供的成员
        [HideInInspector]
        public Animator anim;
        [HideInInspector]
        public CharacterStatus chStatus;
        [HideInInspector]
        public Transform targetTF;
        [Tooltip("攻击目标标签")]
        public string[] targetTags = { "Player" };
        [Tooltip("视野距离")]
        public float sightDistance = 10;
        [HideInInspector]
        public CharacterSkillSystem skillSystem;

        [Tooltip("路点")]
        public Transform[] wayPoints;
        [Tooltip("巡逻模式")]
        public PatrolMode patrolMode;
        /// <summary>
        /// 是否完成巡逻
        /// </summary>
        [HideInInspector]
        public bool isPatorlComplete;
        public void InitComponent()
        {
            anim = GetComponentInChildren<Animator>();
            chStatus = GetComponent<CharacterStatus>();
            navAgent = GetComponent<NavMeshAgent>();
            skillSystem = GetComponent<CharacterSkillSystem>();
        }

        // 查找目标
        private void SearchTarget()
        {
            SkillData data = new SkillData()
            {
                attackTargetTags = targetTags,
                attackDistance = sightDistance,
                attackAngle = 360,
                attackType = SkillAttackType.SingleAttack
            };

           Transform[] targetArr = new SectorAttackSelector().SelectTarget(data, transform);
            targetTF = targetArr.Length == 0 ? null : targetArr[0];
        }

        // 寻路组件
        private NavMeshAgent navAgent;
        [Tooltip("跑步速度")]
        public float runSpeed = 2;
        [Tooltip("走路速度")]
        public float walkSpeed = 1;

    

        /// <summary>
        /// 移动到目标位置
        /// </summary>
        /// <param name="position">位置</param>
        /// <param name="stopDistance">停止距离</param>
        /// <param name="moveSpeed">移动速度</param>
        public void MoveToTarget(Vector3 position, float stopDistance, float moveSpeed)
        {
            // 通过寻路组件实现
            navAgent.SetDestination(position);
            navAgent.stoppingDistance = stopDistance;
            navAgent.speed = moveSpeed;
        }

        public void StopMove()
        {
            //navAgent.SetDestination(transform.position);
            navAgent.enabled = false;
            navAgent.enabled = true;
        }
        #endregion
     
        /*
         * 状态机主要执行流程：
         * 状态机每帧检测当前状态条件 ---> 状态类遍历所有条件对象 --> 
         * 如果每个条件达成 --> 状态机切换当前状态
         * 
         * 详细执行流程:
         * Start ()
         * {
         *      配置状态机 
         *      {
         *              创建条件列表
         *              创建状态对象
         *              添加映射   ---> 创建条件对象
         *     
         *      }
         *      根据默认状态编号设置当前状态 【当前状态】
         *      

         * }
         * 
         * void Update()
         * {
         *      检测当前状态条件
         *      {
         *              遍历条件列表，检测每个条件是否满足
         *              如果发现条件达成，则通过状态机切换状态(设置当前状态)。
         *      }
         *      执行当前状态行为
         *      
         * 
         * }

         */

    }

}
