using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.FSM
{
    /// <summary>
    /// ×·Öð×´Ì¬
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
            // Í£Ö¹ÒÆ¶¯
            fsm.StopMove();
            fsm.anim.SetBool(fsm.chStatus.chParams.run, false);
        }


    }

}
