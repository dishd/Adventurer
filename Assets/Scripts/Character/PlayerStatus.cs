using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPGDemo01.Character
{
    /// <summary>
    /// ��ɫ״̬
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

        // ���ط��� ͨ�����������õ��ã����Ǹ�����ͬ��������������������
        // ����ű��������ڳ�ͻ: �������أ��ڷ�������ͨ��base�ؼ��ֵ��ø�
        // Unity�У������฽�ӵ������У�������������൱��ͨ�����������õ��ýű���������.
        private new void Start()
        {
            base.Start();
            //print("����Start����");
        }

        // ������ʱ���޸ĸ��෽��������ַ��
        // ��Ϊ��Ҫ���ø��ִ࣬������
        protected override void Death()
        {
            base.Death();

            UIManager.Instance.SetVisible("Panel_Loser", true);
            
        }
    }

}
