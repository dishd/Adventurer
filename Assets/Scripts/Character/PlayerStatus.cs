using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPGDemo01.Character
{
    /// <summary>
    /// 角色状态
    /// </summary>

    public class PlayerStatus : CharacterStatus
    {
        [HideInInspector]
        public string playerName;
        [HideInInspector]
        public int lv;
        [HideInInspector]
        public int exp;
        [HideInInspector]
        public int intellect;
        [HideInInspector]
        public int atkSpeed;
        public int lvExp;


        public void setCharacterStatus()
        {
            playerName = CharacterTemplate.Instance.characterName;
            lv = CharacterTemplate.Instance.lv;
            exp = CharacterTemplate.Instance.expCur;
            baseATK = CharacterTemplate.Instance.force;
            intellect = CharacterTemplate.Instance.intellect;
            maxHP = CharacterTemplate.Instance.maxHP;
            maxSP = CharacterTemplate.Instance.maxMP;
            HP = CharacterTemplate.Instance.maxHP;
            SP = CharacterTemplate.Instance.maxMP;
            
        }

        // 隐藏方法 通过子类型引用调用，覆盖父类型同名方法，好像它不存在
        // 解决脚本生命周期冲突: 方法隐藏，在方法体内通过base关键字调用父
        // Unity中，将子类附加到物体中，创建子类对象，相当于通过子类型引用调用脚本生命周期.
        private new void Start()
        {
            base.Start();
            //print("子类Start方法");
        }

        // 在运行时，修改父类方法方法地址表
        // 因为需要调用父类，执行子类
        protected override void Death()
        {
            base.Death();

            UIManager.Instance.SetVisible("Panel_Loser", true);
            
        }
    }

}
