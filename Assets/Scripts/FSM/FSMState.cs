using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace AI.FSM
{
    /// <summary>
    /// 状态类
    /// </summary>
    public abstract class FSMState
    {
        public FSMStateID StateID { get; set; }
        // 映射表
        private Dictionary<FSMTriggerID, FSMStateID> map;

        // 条件列表
        private List<FSMTrigger> triggers;

        public FSMState()
        {
            Init();
            triggers = new List<FSMTrigger>();
            map = new Dictionary<FSMTriggerID, FSMStateID>();
        }

        // 要求实现类必须初始化状态类，为编号赋值
        public abstract void Init();

        // 有状态机调用 (为映射表和条件列表赋值)
        public void AddMap(FSMTriggerID triggerID, FSMStateID stateID)
        {
            // 添加映射
            map.Add(triggerID, stateID);

            // 创建条件对象
            CreateTrigger(triggerID);
        }

        private void CreateTrigger(FSMTriggerID triggerID)
        {

            //创建条件对象
            // 命名规则: AI.FSM + 条件枚举 + Trigger
            Type type = Type.GetType("AI.FSM." + triggerID + "Trigger");
            FSMTrigger trigger = Activator.CreateInstance(type) as FSMTrigger;
            triggers.Add(trigger);
        }
        
        // 为具体状态类提供可选实现
        public virtual void EnterState(FSMBase fsm) { }
        public virtual void ActionState(FSMBase fsm) { }
        public virtual void ExitState(FSMBase fsm) { }

        // 检测当前状态的条件是否满足
        public void Reason(FSMBase fsm)
        {
            for (int i =0; i < triggers.Count; i++)
            {
                // 发现条件满足
                if (triggers[i].HandleTrigger(fsm))
                {
                    // 从状态表里获取输出状态
                    FSMStateID stateID = map[triggers[i].TriggerID];
                    // 切换状态
                    fsm.ChangeActiveState(stateID);
                    return;

                }
            }
        }
    }

}
