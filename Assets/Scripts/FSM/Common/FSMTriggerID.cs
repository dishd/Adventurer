using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.FSM
{
    /// <summary>
    /// 条件编号
    /// </summary>
    public enum FSMTriggerID
    {
        /// <summary>
        /// 生命为0
        /// </summary>
        NoHealth,

        /// <summary>
        /// 发现目标
        /// </summary>
        SawTarget,

        /// <summary>
        /// 目标进入攻击范围
        /// </summary>
        ReachTarget,

        /// <summary>
        /// 丢失玩家
        /// </summary>
        LoseTarget,

        /// <summary>
        /// 完成巡逻
        /// </summary>
        CompletePatrol,

        /// <summary>
        /// 打死目标
        /// </summary>
        KilledTarget,

        /// <summary>
        /// 目标不存在攻击范围 / 玩家离开攻击范围
        /// </summary>
        WithoutAttackRange
    }

}
