using ns;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ARPGDemo.Skill
{

    /// <summary>
    /// 技能数据
    /// </summary>
    [Serializable]
    public class SkillData
    {
        /// <summary>
        /// 技能ID
        /// </summary>
        public int skillID { get; set; }

        /// <summary>
        /// 技能图标
        /// </summary>
        public string skillCon { get; set; }


        /// <summary>
        /// 技能描述
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// 技能名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 持续时间
        /// </summary>
        public float durationTime { get; set; }

        /// <summary>
        /// 伤害间隔
        /// </summary>
        public float atkInterval { get; set; }

        /// <summary>
        /// 伤害比率
        /// </summary>
        public float atkRatio { get; set; }

        /// <summary>
        /// 冷却时间
        /// </summary>
        public int coolTime { get; set; }

        /// <summary>
        /// 魔法消耗
        /// </summary>
        public int costSP { get; set; }

        /// <summary>
        /// 攻击距离
        /// </summary>
        public float attackDistance { get; set; }

        /// <summary>
        /// 攻击目标tags
        /// </summary>
        public string[] attackTargetTags  { get; set; }


    /// <summary>
    /// 技能等级
    /// </summary>
    public int level { get; set; }



        /// <summary>
        /// 技能预制件名称
        /// </summary>
        public string prefabName { get; set; }


        /// <summary>
        /// 无用凑数
        /// </summary>
        public int DamageMode { get; set; }

        /// <summary>
        /// 攻击类型 单攻，群攻
        /// </summary>
        public SkillAttackType attackType { get; set; }

        /// <summary>
        /// 动画名称
        /// </summary>
        public string animationName { get; set; }

        /// <summary>
        /// 攻击角度
        /// </summary>
        public float attackAngle { get; set; }


        /// <summary>
        /// 特效名称
        /// </summary>
        public string hitFxName { get; set; }

        /// <summary>
        /// 连击的下一个技能编号
        /// </summary>
        public int nextBatterId { get; set; }





        /// <summary>
        /// 技能所属
        /// </summary>
        [HideInInspector]
        public GameObject owner;

        [HideInInspector]
        public GameObject skillPrefab;
        /// <summary>
        /// 受击特效预制件
        /// </summary>
        [HideInInspector]
        public GameObject hitFxPrefab;


        /// <summary>
        /// 选择类型  扇形(圆形)，矩阵
        /// </summary>
        public SelectorType selectorType;


        /// <summary>
        /// 冷却剩余
        /// </summary>
        public int coolRemain;


        /// <summary>
        /// 攻击目标对象组
        /// </summary>
        [HideInInspector]
        public Transform[] attackTargets;

        /// <summary>
        /// 技能影响类型
        /// </summary>
        public string[] impactType = { "CostSP", "Damage" };
    }

}
