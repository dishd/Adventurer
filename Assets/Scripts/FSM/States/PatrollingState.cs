using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.FSM
{
    /// <summary>
    /// Ѳ��״̬
    /// </summary>
    public class PatrollingState : FSMState
    {
        public override void Init()
        {
            StateID = FSMStateID.Patrolling;
        }

        public override void EnterState(FSMBase fsm)
        {
            base.EnterState(fsm);
            fsm.isPatorlComplete = false; // ����Ѳ��״̬ʱ��Ѳ��û�����
            fsm.anim.SetBool(fsm.chStatus.chParams.run, true);
        }

        public override void ExitState(FSMBase fsm)
        {
            base.ExitState(fsm);
            fsm.anim.SetBool(fsm.chStatus.chParams.run, false);
        }

        public override void ActionState(FSMBase fsm)
        {
            base.ActionState(fsm);

            //����Ѳ��ģʽ
            switch(fsm.patrolMode)
            {
                case PatrolMode.Once:
                    OncePatrolling(fsm);
                    break;
                case PatrolMode.Loop:
                    LoopPatrolling(fsm);
                    break;
                case PatrolMode.PingPong:
                    PingPongPatrolling(fsm);
                    break;
            }
        }

        private void PingPongPatrolling(FSMBase fsm)
        {
            // -- ���� A B C B A B C...
            if (Vector3.Distance(fsm.transform.position, fsm.wayPoints[index].position) < 1)
            {
                if (index == fsm.wayPoints.Length - 1)
                {
                    // ���鷴ת
                    Array.Reverse(fsm.wayPoints);
                    index++;
                }
                // A B C C B A
                // 0 1  2 0 1  2
                index = (index + 1) % fsm.wayPoints.Length;
            }
            // ���� �ƶ�
            fsm.MoveToTarget(fsm.wayPoints[index].position, 0, fsm.walkSpeed);
        }

        private void LoopPatrolling(FSMBase fsm)
        {
            // -- ѭ�� A B C A B C....
            //�Ƿ񵽴�Ŀ���
            if (Vector3.Distance(fsm.transform.position, fsm.wayPoints[index].position) < 1)
            {
                // �ص� : ȡ�����ʹһ��������һ�������ڱ仯
                index = (index + 1) % fsm.wayPoints.Length;
            }
                // ���� �ƶ�
             fsm.MoveToTarget(fsm.wayPoints[index].position, 0, fsm.walkSpeed);
        }

        private int index = 0;
        private void OncePatrolling(FSMBase fsm)
        {
            // -- ���� A B C
            // fsm.wayPoints[2].position

            //�Ƿ񵽴�Ŀ���

            if (Vector3.Distance(fsm.transform.position, fsm.wayPoints[index].position) < 1)
            {

                // ����Ѿ��������������
                if (index == fsm.wayPoints.Length - 1)
                {
                    // ���Ѳ��
                    fsm.isPatorlComplete = true;
                    return; // �˳�
                }
                index++;
            }

            // Debug.Log("index:" + index + "Distance" + Vector3.Distance(fsm.transform.position, fsm.wayPoints[index].position));
            // ���� �ƶ�
            fsm.MoveToTarget(fsm.wayPoints[index].position, 0, fsm.walkSpeed);
        }
    }

}
