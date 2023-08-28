using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.FSM
{
    /// <summary>
    /// ״̬���
    /// </summary>
    public enum FSMStateID
    {
        /// <summary>
        /// ��(�����ڸ�״̬)
        /// </summary>
       None,

       /// <summary>
       /// ����
       /// </summary>
       Idle,

       /// <summary>
       /// ����
       /// </summary>
       Dead,

       /// <summary>
       /// ׷��
       /// </summary>
       Pursuit,

       /// <summary>
       /// ����
       /// </summary>
       Attacking,

       /// <summary>
       /// Ĭ��
       /// </summary>
       Default,

       /// <summary>
       /// Ѳ��
       /// </summary>
       Patrolling
    }

}
