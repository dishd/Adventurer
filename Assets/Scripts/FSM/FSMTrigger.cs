using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.FSM
{
    /// <summary>
    /// ������
    /// </summary>
    public abstract class FSMTrigger
    {
        // ���
        public FSMTriggerID TriggerID { get; set; }

        public FSMTrigger ()
        {
            Init();
        }
        //Ҫ����������ʼ��������Ϊ��Ÿ�ֵ��
        public abstract void Init ();


        // �߼�����
        public abstract bool HandleTrigger(FSMBase fsm);

    }

}
