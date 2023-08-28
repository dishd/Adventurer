using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPGDemo.Skill
{
    /// <summary>
    /// �����ͷ���
    /// </summary>
    
    public abstract class SkillDeployer : MonoBehaviour
    {
        private SkillData skillData;
        private IAttackSelector selector;
        // Ӱ���㷨����
        private IImpactEffect[] impactArray;
        // ���ܹ������ṩ
        public SkillData SkillData
        {
            get
            {
                return skillData;
            }
            set
            {
                skillData = value;
                // �����㷨����
                InitDeplopyer();
            }
        }

        // �����㷨����
        private void InitDeplopyer()
        {
            // �����㷨����
            // ѡ��
            selector = DeployerConfigFactory.CreateIAttackSelecto(skillData);


            // Ӱ��
            impactArray = DeployerConfigFactory.CreateImpactEffect(skillData);
         
        }

       
        // ִ���㷨����
        // ѡ��
        public void CalculateTargets()
        {
            skillData.attackTargets = selector.SelectTarget(skillData, transform);
            
            //*************����****************
            //foreach(var item in skillData.attackTargets)
            //{
            //    print(item);
            //}
        }

        // Ӱ��
        // �ͷŷ�ʽ
        // �����ܹ���������, ������ʵ�֣���������ͷŲ��ԡ�
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
