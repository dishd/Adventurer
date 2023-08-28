using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace AI.FSM
{
    /// <summary>
    /// ״̬��
    /// </summary>
    public abstract class FSMState
    {
        public FSMStateID StateID { get; set; }
        // ӳ���
        private Dictionary<FSMTriggerID, FSMStateID> map;

        // �����б�
        private List<FSMTrigger> triggers;

        public FSMState()
        {
            Init();
            triggers = new List<FSMTrigger>();
            map = new Dictionary<FSMTriggerID, FSMStateID>();
        }

        // Ҫ��ʵ��������ʼ��״̬�࣬Ϊ��Ÿ�ֵ
        public abstract void Init();

        // ��״̬������ (Ϊӳ���������б�ֵ)
        public void AddMap(FSMTriggerID triggerID, FSMStateID stateID)
        {
            // ���ӳ��
            map.Add(triggerID, stateID);

            // ������������
            CreateTrigger(triggerID);
        }

        private void CreateTrigger(FSMTriggerID triggerID)
        {

            //������������
            // ��������: AI.FSM + ����ö�� + Trigger
            Type type = Type.GetType("AI.FSM." + triggerID + "Trigger");
            FSMTrigger trigger = Activator.CreateInstance(type) as FSMTrigger;
            triggers.Add(trigger);
        }
        
        // Ϊ����״̬���ṩ��ѡʵ��
        public virtual void EnterState(FSMBase fsm) { }
        public virtual void ActionState(FSMBase fsm) { }
        public virtual void ExitState(FSMBase fsm) { }

        // ��⵱ǰ״̬�������Ƿ�����
        public void Reason(FSMBase fsm)
        {
            for (int i =0; i < triggers.Count; i++)
            {
                // ������������
                if (triggers[i].HandleTrigger(fsm))
                {
                    // ��״̬�����ȡ���״̬
                    FSMStateID stateID = map[triggers[i].TriggerID];
                    // �л�״̬
                    fsm.ChangeActiveState(stateID);
                    return;

                }
            }
        }
    }

}
