using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.FSM
{
    /// <summary>
    /// Ŀ����빥����Χ
    /// </summary>
    public class ReachTargetTrigger : FSMTrigger
    {
        public override bool HandleTrigger(FSMBase fsm)
        {
            // ״̬��λ�� �� Ŀ��λ�� ���
            // �� ��ɫ��������Ƚ�
            if (fsm.targetTF == null) return false;
            return Vector3.Distance(fsm.transform.position, fsm.targetTF.position) < fsm.chStatus.attackDistance;
        }

        public override void Init()
        {
            TriggerID = FSMTriggerID.ReachTarget;
        }
    }

}
