using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.FSM
{
    /// <summary>
    /// �������
    /// </summary>
    public enum FSMTriggerID
    {
        /// <summary>
        /// ����Ϊ0
        /// </summary>
        NoHealth,

        /// <summary>
        /// ����Ŀ��
        /// </summary>
        SawTarget,

        /// <summary>
        /// Ŀ����빥����Χ
        /// </summary>
        ReachTarget,

        /// <summary>
        /// ��ʧ���
        /// </summary>
        LoseTarget,

        /// <summary>
        /// ���Ѳ��
        /// </summary>
        CompletePatrol,

        /// <summary>
        /// ����Ŀ��
        /// </summary>
        KilledTarget,

        /// <summary>
        /// Ŀ�겻���ڹ�����Χ / ����뿪������Χ
        /// </summary>
        WithoutAttackRange
    }

}
