using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPGDemo.Skill
{
    /// <summary>
    /// 
    /// </summary>
    public class MeleeSkillDeployer : SkillDeployer
    {
        public override void DeploySkill()
        {
            // ִ��ѡ���㷨
            CalculateTargets();


            // ִ��Ӱ���㷨
            ImpactTargets();
        }
    }

}
