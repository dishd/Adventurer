using ARPGDemo01.Character;
using ns;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace ARPGDemo.Skill
{
    /// <summary>
    /// 伤害生命
    /// </summary>
    public class DamageImpact : IImpactEffect
    {
        //private SkillData data;
        public void Execute(SkillDeployer deployer)
        {
           
            // data = deployer.SkillData;
            deployer.StartCoroutine(RepeatDamage(deployer));
        }

        private IEnumerator RepeatDamage(SkillDeployer deployer)
        {
            float atkTime = 0;
            do
            {
                // 伤害目标生命
                OnceDamage(deployer.SkillData);
                yield return new WaitForSeconds(deployer.SkillData.atkInterval);
                atkTime += deployer.SkillData.atkInterval;
                deployer.CalculateTargets(); // 重新计算目标
            } while (atkTime < deployer.SkillData.durationTime); // 攻击时间没到
        }

        private void OnceDamage(SkillData data)
        {
            //deployer.SkillData.attackTargets ---> CharacterStatus HP
            // 技能攻击力 : 攻击比率 * 基础攻击力
            float atk = data.atkRatio * data.owner.GetComponent<CharacterStatus>().baseATK;
            
            for (int i = 0; i < data.attackTargets.Length; i++) 
            {
                var status =  data.attackTargets[i].GetComponent<CharacterStatus>();

                status.Damage(atk);
                if (data.owner.tag == "Player")
                    DamageEffectEnemy(data.attackTargets[i], atk, status);
                else
                {
                    DamgeEffectPlayer(status);
                }
            }

            // 创建攻击特效
        }

        private void DamageEffectEnemy(Transform tf, float atk, CharacterStatus cs)
        {
            TestNPC npc = tf.GetComponent<TestNPC>();
            npc._bloodSlider.value = (float)( cs.HP /cs.maxHP);
            HUDText hud = npc._heroPanel.GetComponent<HUDText>();
            hud.Add(atk, Color.red, 0);
        }

        private void DamgeEffectPlayer(CharacterStatus cs)
        {
            int c = (int)cs.HP;
            string hpString = c.ToString() + "/"  + cs.maxHP.ToString();
            Panel_RoleStatus rs = UIManager.Instance.GetUI<Panel_RoleStatus>("Panel_RoleStatus");
            rs.setHP(hpString);
        }
    }

}
