using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ns
{
    /// <summary>
    /// 
    /// </summary>
    public class Panel_MinMap : UIScene
    {
        protected override void Start()
        {
            base.Start();
            GetWidget("Top_Left").MouseClick = MouseClick;
        }

        private void MouseClick(UISceneWidget eventObj)
        {
            UIManager.Instance.SetVisible("Panel_Map", true);
        }
    }

}
