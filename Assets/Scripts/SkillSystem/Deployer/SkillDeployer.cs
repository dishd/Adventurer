using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPGDemo.Skill
{
    /// <summary>
    /// 技能释放器
    /// </summary>
    
    public abstract class SkillDeployer : MonoBehaviour
    {
        private SkillData skillData;
        private IAttackSelector selector;
        // 影响算法对象
        private IImpactEffect[] impactArray;
        // 技能管理器提供
        public SkillData SkillData
        {
            get
            {
                return skillData;
            }
            set
            {
                skillData = value;
                // 创建算法对象
                InitDeplopyer();
            }
        }

        // 创建算法对象
        private void InitDeplopyer()
        {
            // 创建算法对象
            // 选区
            selector = DeployerConfigFactory.CreateIAttackSelecto(skillData);


            // 影响
            impactArray = DeployerConfigFactory.CreateImpactEffect(skillData);
         
        }

       
        // 执行算法对象
        // 选区
        public void CalculateTargets()
        {
            skillData.attackTargets = selector.SelectTarget(skillData, transform);
            
            //*************测试****************
            //foreach(var item in skillData.attackTargets)
            //{
            //    print(item);
            //}
        }

        // 影响
        // 释放方式
        // 共技能管理器调用, 由子类实现，定义具体释放策略。
        public void ImpactTargets()
        {
            for (int i = 0; i < impactArray.Length; i++)
            {
                impactArray[i].Execute(this);
            }
        }
        public abstract void DeploySkill(); 
    }

}
