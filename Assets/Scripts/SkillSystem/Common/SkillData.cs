using ns;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ARPGDemo.Skill
{

    /// <summary>
    /// ��������
    /// </summary>
    [Serializable]
    public class SkillData
    {
        /// <summary>
        /// ����ID
        /// </summary>
        public int skillID { get; set; }

        /// <summary>
        /// ����ͼ��
        /// </summary>
        public string skillCon { get; set; }


        /// <summary>
        /// ��������
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// ��������
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// ����ʱ��
        /// </summary>
        public float durationTime { get; set; }

        /// <summary>
        /// �˺����
        /// </summary>
        public float atkInterval { get; set; }

        /// <summary>
        /// �˺�����
        /// </summary>
        public float atkRatio { get; set; }

        /// <summary>
        /// ��ȴʱ��
        /// </summary>
        public int coolTime { get; set; }

        /// <summary>
        /// ħ������
        /// </summary>
        public int costSP { get; set; }

        /// <summary>
        /// ��������
        /// </summary>
        public float attackDistance { get; set; }

        /// <summary>
        /// ����Ŀ��tags
        /// </summary>
        public string[] attackTargetTags  { get; set; }


    /// <summary>
    /// ���ܵȼ�
    /// </summary>
    public int level { get; set; }



        /// <summary>
        /// ����Ԥ�Ƽ�����
        /// </summary>
        public string prefabName { get; set; }


        /// <summary>
        /// ���ô���
        /// </summary>
        public int DamageMode { get; set; }

        /// <summary>
        /// �������� ������Ⱥ��
        /// </summary>
        public SkillAttackType attackType { get; set; }

        /// <summary>
        /// ��������
        /// </summary>
        public string animationName { get; set; }

        /// <summary>
        /// �����Ƕ�
        /// </summary>
        public float attackAngle { get; set; }


        /// <summary>
        /// ��Ч����
        /// </summary>
        public string hitFxName { get; set; }

        /// <summary>
        /// ��������һ�����ܱ��
        /// </summary>
        public int nextBatterId { get; set; }





        /// <summary>
        /// ��������
        /// </summary>
        [HideInInspector]
        public GameObject owner;

        [HideInInspector]
        public GameObject skillPrefab;
        /// <summary>
        /// �ܻ���ЧԤ�Ƽ�
        /// </summary>
        [HideInInspector]
        public GameObject hitFxPrefab;


        /// <summary>
        /// ѡ������  ����(Բ��)������
        /// </summary>
        public SelectorType selectorType;


        /// <summary>
        /// ��ȴʣ��
        /// </summary>
        public int coolRemain;


        /// <summary>
        /// ����Ŀ�������
        /// </summary>
        [HideInInspector]
        public Transform[] attackTargets;

        /// <summary>
        /// ����Ӱ������
        /// </summary>
        public string[] impactType = { "CostSP", "Damage" };
    }

}
