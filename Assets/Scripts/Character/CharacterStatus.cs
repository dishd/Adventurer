using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPGDemo01.Character
{
    /// <summary>
    /// ��ɫ״̬��
    /// </summary>
    
    public class CharacterStatus : MonoBehaviour
    {
        [Tooltip("��������")]
        public CharacterAnimationParameter chParams;
        [Tooltip("����")]
        public float HP;
        [Tooltip("�������")]
        public float maxHP;
        [Tooltip("����")]
        public float SP;
        [Tooltip("�����")]
        public float maxSP;
        [Tooltip("����������")]
        public float baseATK;
        [Tooltip("������")]
        public float defence;
        [Tooltip("�������")]
        public float attackInterval;
        [Tooltip("��������")]
        public float attackDistance;


        protected void Start()
        {
            //print("����Star����");
            chParams = new CharacterAnimationParameter();
        }
        public void Damage(float val)
        {
            val -= defence;

            if (val <= 0) { return; }
            HP -= val;

            if (HP <= 0)
            {
                Death();
            }
        }

        // ���ø��� ����������ִ��������������
        protected virtual void Death()
        {
            GetComponentInChildren<Animator>().SetBool(chParams.death, true);
        }

    }

}
