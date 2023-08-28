using ARPGDemo01.Character;
using ns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPGDemo.Skill
{
    /// <summary>
    /// ���ķ���
    /// </summary>
    public class CostSPImpact : IImpactEffect
    {
        // ����ע�� ���Ʒ�ת
        public void Execute(SkillDeployer deployer)
        {
            var status = deployer.SkillData.owner.GetComponent<CharacterStatus>();
            status.SP -= deployer.SkillData.costSP;

            if (deployer.SkillData.owner.tag == "Player")
            {
                string sm = status.SP.ToString() + "/" + status.maxSP.ToString();
                UIManager.Instance.GetUI<Panel_RoleStatus>("Panel_RoleStatus").SetMP(sm);
            }
        }
    }

}
