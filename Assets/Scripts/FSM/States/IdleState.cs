using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.FSM
{
    /// <summary>
    /// Ĭ��״̬
    /// </summary>
    public class IdleState : FSMState
    {
        public override void Init()
        {
            StateID = FSMStateID.Idle;
        }

        public override void EnterState(FSMBase fsm)
        {
            base.EnterState(fsm);

            // ���Ŵ�������
            fsm.anim.SetBool(fsm.chStatus.chParams.idle, true);
        }

        public override void ExitState(FSMBase fsm)
        {
            base.ExitState(fsm);

            // ���Ŵ�������
            fsm.anim.SetBool(fsm.chStatus.chParams.idle, false);
        }
    }

}
