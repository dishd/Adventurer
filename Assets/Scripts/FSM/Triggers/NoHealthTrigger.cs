using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.FSM
{
    /// <summary>
    /// û����������
    /// </summary>
    public class NoHealthTrigger : FSMTrigger
    {
        public override bool HandleTrigger(FSMBase fsm)
        {
            return fsm.chStatus.HP <= 0;
        }


        public override void Init()
        {
            TriggerID = FSMTriggerID.NoHealth;
        }
    }

}
