using ARPGDemo.Skill;
using Common;
using myBagFrame;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ns
{
    /// <summary>
    /// 
    /// </summary>
    public class SkillPanel : BagPanel
    {
        private SkillData[] skillDatas;


        protected override void Start()
        {
            base.Start();

            Init();
        }


        private void Init()
        {
            //SkillData[] sks = DB.Instance.GetSkillDatasByDB( "T_Skill"+ CharacterTemplate.Instance.characterID);

            SkillData[] sks = DB.Instance.GetSkillDatasByDB("T_Skill1" );


            skillDatas = sks.FindAll((item) => { return item.costSP > 0; });
            
            foreach (SkillData skillData in skillDatas)
            {
                AddSkill(skillData);
            }

        }

        private void AddSkill(SkillData skillData)
        {
            GameObject go = Instantiate(bagUnit, uiGrid.transform);
            SkillBagItem bi = go.GetComponent<SkillBagItem>();
            bi.ID = skillData.skillID;
            bi.SetItem(skillData.skillCon, skillData.name);
            bi.skillDes = skillData.description;
        }

        public SkillData[] GetSkillDatas()
        {
            return skillDatas;
        }
    
    }

}
