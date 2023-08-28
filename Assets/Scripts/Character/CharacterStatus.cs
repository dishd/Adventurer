using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPGDemo01.Character
{
    /// <summary>
    /// 角色状态类
    /// </summary>
    
    public class CharacterStatus : MonoBehaviour
    {
        [Tooltip("动画参数")]
        public CharacterAnimationParameter chParams;
        [Tooltip("生命")]
        public float HP;
        [Tooltip("最大生命")]
        public float maxHP;
        [Tooltip("法力")]
        public float SP;
        [Tooltip("最大法力")]
        public float maxSP;
        [Tooltip("基础攻击力")]
        public float baseATK;
        [Tooltip("防御力")]
        public float defence;
        [Tooltip("攻击间隔")]
        public float attackInterval;
        [Tooltip("攻击距离")]
        public float attackDistance;


        protected void Start()
        {
            //print("父类Star方法");
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

        // 调用父亲 死亡方法，执行子类死亡方法
        protected virtual void Death()
        {
            GetComponentInChildren<Animator>().SetBool(chParams.death, true);
        }

    }

}
