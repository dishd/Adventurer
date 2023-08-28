using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPGDemo.Skill
{
    /// <summary>
    /// 影响效果算法
    /// </summary>
    
    public interface IImpactEffect
    {
        // 伤害生命 5
        void Execute(SkillDeployer deployer);
    }

}
