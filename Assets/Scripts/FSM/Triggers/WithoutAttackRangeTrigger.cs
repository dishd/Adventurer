using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.FSM
{
    /// <summary>
    /// Àë¿ª¹¥»÷·¶Î§
    /// </summary>
    public class WithoutAttackRangeTrigger : FSMTrigger
    {
        public override bool HandleTrigger(FSMBase fsm)
        {
            if (fsm.targetTF == null) return false;
            return Vector3.Distance(fsm.transform.position, fsm.targetTF.position) > fsm.chStatus.attackDistance; 
        }

        public override void Init()
        {
            TriggerID = FSMTriggerID.WithoutAttackRange;
        }
    }

}
