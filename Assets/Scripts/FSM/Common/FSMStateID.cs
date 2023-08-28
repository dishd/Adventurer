using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.FSM
{
    /// <summary>
    /// ×´Ì¬±àºÅ
    /// </summary>
    public enum FSMStateID
    {
        /// <summary>
        /// ÎÞ(²»´æÔÚ¸Ã×´Ì¬)
        /// </summary>
       None,

       /// <summary>
       /// ´ý»ú
       /// </summary>
       Idle,

       /// <summary>
       /// ËÀÍö
       /// </summary>
       Dead,

       /// <summary>
       /// ×·Öð
       /// </summary>
       Pursuit,

       /// <summary>
       /// ¹¥»÷
       /// </summary>
       Attacking,

       /// <summary>
       /// Ä¬ÈÏ
       /// </summary>
       Default,

       /// <summary>
       /// Ñ²Âß
       /// </summary>
       Patrolling
    }

}
