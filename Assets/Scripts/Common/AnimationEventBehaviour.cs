using ARPGDemo01.Character;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ns
{
    /// <summary>
    /// �����¼���Ϊ��
    /// </summary>
    
    public class AnimationEventBehaviour : MonoBehaviour
    {
        // �߻���
        // Ϊ����Ƭ������¼���ָ��ONCancleAnim��OnAttack��

        // ����
        // �ڽű��в��Ŷ�������������Ҫִ�е��߼���ע��attack�¼�

        public Animator anim;
        public event Action attackHandler;
        private object locker;

        private CharacterInputController characterInputController;

        private void Start()
        {
            locker = new object();
            anim = GetComponent<Animator>();
            characterInputController = GetComponent<CharacterInputController>();
        }

        // �� Unity �������
        private void OnCancelAnim(string animParam)
        {
            anim.SetBool(animParam, false);
        }


        private void OnCancelAnimNoPara()
        {
           
            characterInputController.isAttacking = false;
            characterInputController.isDisplay = false;

        }

        private void NoFunc()
        {
            characterInputController.isDisplay = false;
        }

        // ��Unity �������
        private void OnAttack()
        {
            if (attackHandler != null)
            {
                attackHandler(); // �����¼�
            }

        }

    }

}
