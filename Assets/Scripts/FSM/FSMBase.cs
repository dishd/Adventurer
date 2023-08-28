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
    /// ״̬��
    /// </summary>
    [RequireComponent(typeof(EnemyStatus),typeof(NavMeshAgent), typeof(CharacterSkillSystem))]
    public class FSMBase : MonoBehaviour
    {
        #region ״̬��������Ա
        // ״̬���
        private List<FSMState> states;

        [Tooltip("Ĭ��״̬���")]
        public FSMStateID defaultStateID;

        [Tooltip("��ǰ״̬��ʹ�õ������ļ�")]
        public string fileName = "AI_01.txt";

        //��ǰ״̬
        //[SerializeField]
        public FSMState currentState;

        // Ĭ��״̬
        private FSMState defaultState;


        private void InitDefaultState()
        {
            // ����Ĭ��״̬
            defaultState = states.Find(s => s.StateID == defaultStateID);
            // Ϊ��ǰ״̬��ֵ
            currentState = defaultState;
            // ����״̬
            currentState.EnterState(this);
        }

        // ͨ�������ļ�
        private void ConfigFSM()
        {
            states = new List<FSMState>();
            // ÿ��״̬������һ����ȡ������
            //var map = new AIConfigurationReader(fileName).Map;
            // ÿ���ļ�����һ����ȡ������
            var map = AIConfigurationReaderFactory.GetMap(fileName);

            // ���ֵ� --> ״̬
            // С�ֵ� --> ӳ��

            foreach (var state in map)
            {
                //item.Key ״̬����
                //item.Value ӳ��
                Type type = Type.GetType("AI.FSM." + state.Key + "State");
                FSMState stateObj = Activator.CreateInstance(type) as FSMState;
                states.Add(stateObj);

                foreach (var dic in state.Value)
                {
                    // dic.Key  �������
                    // idc.Value ״̬���
                    // string --> Enum
                    FSMTriggerID triggerID = (FSMTriggerID)Enum.Parse(typeof(FSMTriggerID), dic.Key);
                    FSMStateID stateID = (FSMStateID)Enum.Parse(typeof(FSMStateID), dic.Value);
                    // ���ӳ��
                    stateObj.AddMap(triggerID, stateID);
                }
            }

        }

        // ����״̬��
        // Ӳ����
        //private void ConfigFSM()
        //{

        //    states = new List<FSMState>();
        //    // ����״̬��
        //    // --����״̬����
        //    IdleState idle = new IdleState();
        //    // --����״̬(AddMap)
        //    idle.AddMap(FSMTriggerID.NoHealth, FSMStateID.Dead);
        //    idle.AddMap(FSMTriggerID.SawTarget, FSMStateID.Pursuit);
        //    // ---����״̬��
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



        // �л�״̬ 
        public void ChangeActiveState(FSMStateID stateID)
        {
            // FSMStateID.Default
            //���õ�ǰ״̬
            // �����Ҫ�л���״̬����� : Default,��ֱ�ӷ���Ĭ��״̬
            // �����״̬�б��в��� 
            //if (stateID == FSMStateID.Default)
            //        currentState = defaultState;
            //else
            //        currentState = states.Find(s => s.StateID == stateID);

            // �뿪��һ��״̬
            currentState.ExitState(this);
            // �л�״̬
            currentState = stateID == FSMStateID.Default ? defaultState : states.Find(s => s.StateID == stateID);
            // ������һ��״̬
            currentState.EnterState(this);
        }


        #endregion

        #region �ű���������
        private void Start()
        {
            ConfigFSM();
            InitComponent();
            InitDefaultState();
        }

        public FSMStateID test_CurrentStateID;
        // ÿ֡������߼�
        public void Update()
        {
            test_CurrentStateID = currentState.StateID;
            // �жϵ�ǰ״̬����
            currentState.Reason(this);
            //ִ�е�ǰ״̬�߼�
            currentState.ActionState(this);
            SearchTarget();
        }
        #endregion 

        #region Ϊ״̬�뼰�����ṩ�ĳ�Ա
        [HideInInspector]
        public Animator anim;
        [HideInInspector]
        public CharacterStatus chStatus;
        [HideInInspector]
        public Transform targetTF;
        [Tooltip("����Ŀ���ǩ")]
        public string[] targetTags = { "Player" };
        [Tooltip("��Ұ����")]
        public float sightDistance = 10;
        [HideInInspector]
        public CharacterSkillSystem skillSystem;

        [Tooltip("·��")]
        public Transform[] wayPoints;
        [Tooltip("Ѳ��ģʽ")]
        public PatrolMode patrolMode;
        /// <summary>
        /// �Ƿ����Ѳ��
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

        // ����Ŀ��
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

        // Ѱ·���
        private NavMeshAgent navAgent;
        [Tooltip("�ܲ��ٶ�")]
        public float runSpeed = 2;
        [Tooltip("��·�ٶ�")]
        public float walkSpeed = 1;

    

        /// <summary>
        /// �ƶ���Ŀ��λ��
        /// </summary>
        /// <param name="position">λ��</param>
        /// <param name="stopDistance">ֹͣ����</param>
        /// <param name="moveSpeed">�ƶ��ٶ�</param>
        public void MoveToTarget(Vector3 position, float stopDistance, float moveSpeed)
        {
            // ͨ��Ѱ·���ʵ��
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
         * ״̬����Ҫִ�����̣�
         * ״̬��ÿ֡��⵱ǰ״̬���� ---> ״̬����������������� --> 
         * ���ÿ��������� --> ״̬���л���ǰ״̬
         * 
         * ��ϸִ������:
         * Start ()
         * {
         *      ����״̬�� 
         *      {
         *              ���������б�
         *              ����״̬����
         *              ���ӳ��   ---> ������������
         *     
         *      }
         *      ����Ĭ��״̬������õ�ǰ״̬ ����ǰ״̬��
         *      

         * }
         * 
         * void Update()
         * {
         *      ��⵱ǰ״̬����
         *      {
         *              ���������б����ÿ�������Ƿ�����
         *              �������������ɣ���ͨ��״̬���л�״̬(���õ�ǰ״̬)��
         *      }
         *      ִ�е�ǰ״̬��Ϊ
         *      
         * 
         * }

         */

    }

}
