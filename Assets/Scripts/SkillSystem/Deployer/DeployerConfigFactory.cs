using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPGDemo.Skill
{
    /// <summary>
    /// �ͷ������ù��� : �ṩ�����ͷ��������㷨����Ĺ��ܡ�
    /// ���� : ������Ĵ��� �� ʹ�÷��롣
    /// </summary>
    
    public class DeployerConfigFactory 
    {
        private static Dictionary<string, object> cache; 
        static DeployerConfigFactory()
        {
            cache = new Dictionary<string, object>();
        }
        public static IAttackSelector CreateIAttackSelecto(SkillData data)
        {
            // skillData.selectorType 
            // ѡ�������������� :
            // ARPGDemo.Skill. +ö���� + AttackSelector
            // ���� : 
            // ����ѡ�� :  ARPGDemo.Skill.SectorAttackSelector
            string className = string.Format("ARPGDemo.Skill.{0}AttackSelector", data.selectorType);
            return CreateObject<IAttackSelector>(className);
        }
        public static IImpactEffect[] CreateImpactEffect(SkillData data)
        {
            IImpactEffect[] impacts = new IImpactEffect[data.impactType.Length];
            // Ӱ��Ч�������淶 :
            // ARPGDemo.Skill+impactType[?]+Impact
            // ���� ��
            // ���ٷ��� :  ARPGDemo.Skill.CostSPImpact
            for (int i = 0; i < data.impactType.Length; i++)
            {
                string classNameImpact = string.Format("ARPGDemo.Skill.{0}Impact", data.impactType[i]);
                impacts[i] = CreateObject<IImpactEffect>(classNameImpact);
            }

            return impacts;
        }
        private static T CreateObject<T>(string className) where T : class
        {
            if (!cache.ContainsKey(className))
            {
                //Debug.Log("����");
                Type type = Type.GetType(className);
                object instance = Activator.CreateInstance(type);
                cache.Add(className, instance);
            }
            return cache[className] as T;
            
        }
    }

}
