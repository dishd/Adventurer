using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ns
{
    /// <summary>
    /// 
    /// </summary>
    public class SkillList : UIScene
    {
        private UISceneWidget[] widgets;
        protected override void Start()
        {

            base.Start();
            widgets = transform.GetComponentsInChildren<UISceneWidget>();
            foreach (UISceneWidget widget in widgets)
            {
                widget.MouseClick = func;
            }

        }

        private void func(UISceneWidget eventObj)
        {
            SkillButtonList s = UIManager.Instance.GetUI<SkillButtonList>("SkillButtonList");
               s.setDes(eventObj.GetComponent<SkillBagItem>().skillDes);
        }
    }

}
