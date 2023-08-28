using Common;
using myBagFrame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ns
{
    /// <summary>
    /// 
    /// </summary>
    public class SkillBagItem : BagItem
    {

        private UISprite skillIconSprite;
        private UILabel SkillName;
        public string skillDes;
        private string iconName;

        private void Awake()
        {
            skillIconSprite = transform.FindChildCompentByName<UISprite>("SkillIcon");
            SkillName = transform.FindChildCompentByName<UILabel>("SkillName");
        }

        public void SetItem(string iName, string skillName)
        {
            skillIconSprite.spriteName = iName;
            SkillName.text = skillName;
            iconName = iName;

        }

        public string GetInconName()
        {
            return iconName;
        }

        public int GetID()
        {
            return ID;
        }

        public void GetSkill(ref Skill skill)
        {
            skill.iconName = iconName;
            skill.ID = ID;
        }

        
    }

}
