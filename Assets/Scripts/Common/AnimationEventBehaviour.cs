using ARPGDemo01.Character;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ns
{
    /// <summary>
    /// 动画事件行为类
    /// </summary>
    
    public class AnimationEventBehaviour : MonoBehaviour
    {
        // 策划：
        // 为动画片段添加事件，指向ONCancleAnim、OnAttack。

        // 程序：
        // 在脚本中播放动画，动画中需要执行的逻辑，注册attack事件

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

        // 由 Unity 引擎调用
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

        // 由Unity 引擎调用
        private void OnAttack()
        {
            if (attackHandler != null)
            {
                attackHandler(); // 引发事件
            }

        }

    }

}
