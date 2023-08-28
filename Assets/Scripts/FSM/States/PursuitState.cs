using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.FSM
{
    /// <summary>
    /// ׷��״̬
    /// </summary>
    public class PursuitState : FSMState
    {
        public override void Init()
        {
            StateID = FSMStateID.Pursuit;
        }

        public override void EnterState(FSMBase fsm)
        {
            base.EnterState(fsm);
            fsm.anim.SetBool(fsm.chStatus.chParams.run, true);
        }
        public override void ActionState(FSMBase fsm)
        {
            base.ActionState(fsm);
            //fsm.targetTF.position

            if (fsm.targetTF != null)
                fsm.MoveToTarget(fsm.targetTF.position, fsm.chStatus.attackDistance, fsm.runSpeed);
        }

        public override void ExitState(FSMBase fsm)
        {
            base.ExitState(fsm);
            // ֹͣ�ƶ�
            fsm.StopMove();
            fsm.anim.SetBool(fsm.chStatus.chParams.run, false);
        }


    }

}
