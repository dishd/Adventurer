using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.FSM
{
    /// <summary>
    /// ���Ѳ��
    /// </summary>
    public class CompletePatrolTrigger : FSMTrigger
    {
        public override bool HandleTrigger(FSMBase fsm)
        {
            return fsm.isPatorlComplete;
        }

        public override void Init()
        {
            TriggerID = FSMTriggerID.CompletePatrol;
        }
    }

}
