using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace ns
{
    /// <summary>
    /// 
    /// </summary>
    public class SkillButtonList : UIScene
    {
        private UILabel uiLabel;
        private List<UISprite> skillIconList;

        protected override void Start()
        {
            base.Start();
            skillIconList = new List<UISprite>();
            uiLabel = transform.FindChildCompentByName<UILabel>("SkillDes");
            Transform tf = transform.FindChildByName("Skills");
            UISceneWidget[] us = tf.GetComponentsInChildren<UISceneWidget>();
            if (us != null)
            {
                int i = 1;
                foreach (UISceneWidget ui in us)
                {
                    skillIconList.Add(ui.transform.FindChildCompentByName<UISprite>("SkillIcon" + i));
                    ui.MouseClick = Fun;
                    i++;
                }
            }
            
        }

        public void setDes(string s)
        {
            uiLabel.text = s;
        }


        private void Fun(UISceneWidget eventObj)
        {

            UIToggle toggle = UIToggle.GetActiveToggle(1);
            if (toggle == null) return;

            Skill leftSkill = new Skill();
            toggle.GetComponent<SkillBagItem>().GetSkill(ref leftSkill);

            string n = eventObj.name;
            int index = int.Parse(n.Substring(n.Length - 1));
            index -= 1;
            savaSkillData(index, leftSkill);



        }

        private void savaSkillData(int index, Skill leftSkill)
        {
           

            Skill clickSkill = SkillBindButtondData.Instance.skills[index];
            if (clickSkill.ID == 0)
            {
                SkillBindButtondData.Instance.skills[index] = leftSkill;
                skillIconList[index].spriteName = leftSkill.iconName;
                for (int i = 0; i < 4; i++)
                {
                    if (index == i) continue;
                    else
                    {
                        if (SkillBindButtondData.Instance.skills[i].ID == leftSkill.ID)
                        {
                            SkillBindButtondData.Instance.SetNull(i);
                            skillIconList[i].spriteName = "JiNengDianJi";

                        }
                    }
                }
            }
            else if (clickSkill.ID == leftSkill.ID) return;
            else
            {
                string n1 = clickSkill.iconName;
                string n2 = leftSkill.iconName;
                int indexLeft = 0;

                for (int i = 0; i < 4; i++)
                {
                    if (SkillBindButtondData.Instance.skills[i].ID == leftSkill.ID)
                    {
                        indexLeft = i;
                        break;
                    }
                }

                skillIconList[indexLeft].spriteName = n1;
                skillIconList[index].spriteName = n2;

                Skill t = SkillBindButtondData.Instance.skills[indexLeft];
                SkillBindButtondData.Instance.skills[indexLeft] = clickSkill;
                SkillBindButtondData.Instance.skills[index] = t;

            }
        }

    }

}
