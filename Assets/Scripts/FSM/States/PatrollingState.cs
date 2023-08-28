using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.FSM
{
    /// <summary>
    /// 巡逻状态
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
            fsm.isPatorlComplete = false; // 进入巡逻状态时，巡逻没有完成
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

            //根据巡逻模式
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
            // -- 往返 A B C B A B C...
            if (Vector3.Distance(fsm.transform.position, fsm.wayPoints[index].position) < 1)
            {
                if (index == fsm.wayPoints.Length - 1)
                {
                    // 数组反转
                    Array.Reverse(fsm.wayPoints);
                    index++;
                }
                // A B C C B A
                // 0 1  2 0 1  2
                index = (index + 1) % fsm.wayPoints.Length;
            }
            // 走你 移动
            fsm.MoveToTarget(fsm.wayPoints[index].position, 0, fsm.walkSpeed);
        }

        private void LoopPatrolling(FSMBase fsm)
        {
            // -- 循环 A B C A B C....
            //是否到达目标点
            if (Vector3.Distance(fsm.transform.position, fsm.wayPoints[index].position) < 1)
            {
                // 重点 : 取余可以使一个整数在一个周期内变化
                index = (index + 1) % fsm.wayPoints.Length;
            }
                // 走你 移动
             fsm.MoveToTarget(fsm.wayPoints[index].position, 0, fsm.walkSpeed);
        }

        private int index = 0;
        private void OncePatrolling(FSMBase fsm)
        {
            // -- 单词 A B C
            // fsm.wayPoints[2].position

            //是否到达目标点

            if (Vector3.Distance(fsm.transform.position, fsm.wayPoints[index].position) < 1)
            {

                // 如果已经是数据最大索引
                if (index == fsm.wayPoints.Length - 1)
                {
                    // 完成巡逻
                    fsm.isPatorlComplete = true;
                    return; // 退出
                }
                index++;
            }

            // Debug.Log("index:" + index + "Distance" + Vector3.Distance(fsm.transform.position, fsm.wayPoints[index].position));
            // 走你 移动
            fsm.MoveToTarget(fsm.wayPoints[index].position, 0, fsm.walkSpeed);
        }
    }

}
