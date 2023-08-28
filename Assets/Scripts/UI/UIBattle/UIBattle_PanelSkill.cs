using ARPGDemo.Skill;
using ARPGDemo01.Character;
using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ns
{
    /// <summary>
    /// 
    /// </summary>
    public class UIBattle_PanelSkill : UIScene
    {

        public UISceneWidget[] widgets;

        protected override void Start()
        {
            base.Start();

           
            widgets = transform.GetComponentsInChildren<UISceneWidget>();
            
            for (int i = 0; i < widgets.Length - 1; i++)
            {
                if ( SkillBindButtondData.Instance.skills[i].ID != 0 )
                {
                    int t = i + 1;
                    widgets[i].transform.FindChildCompentByName<UISprite>("SkillIcon" + t).spriteName = SkillBindButtondData.Instance.skills[i].iconName;
                }
            }

            FindObjectOfType<CharacterInputController>().SetSkill(widgets);
        }

   
       
    }

}
