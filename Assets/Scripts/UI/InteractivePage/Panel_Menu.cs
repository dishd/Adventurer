using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ns
{
    /// <summary>
    /// 
    /// </summary>
    public class Panel_Menu : UIScene
    {
        protected override void Start()
        {
            base.Start();

            GetWidget("Role").MouseClick = RoleClick;
            GetWidget("skill").MouseClick = SkillClick;
        }

        private void SkillClick(UISceneWidget eventObj)
        {
            UIManager.Instance.SetVisible("Panel_Skill", true);
        }

        private void RoleClick(UISceneWidget eventObj)
        {
            UIManager.Instance.SetVisible("Panel_CharacterInfo", true);
        }
    }

}
