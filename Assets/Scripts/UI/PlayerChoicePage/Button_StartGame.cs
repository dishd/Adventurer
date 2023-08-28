using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ns
{
    /// <summary>
    /// 
    /// </summary>
    public class Button_StartGame : UIScene
    {
        protected override void Start()
        {
            base.Start();
            GetWidget("StartGameBack").MouseClick = ClickFunc;
        }

        private void ClickFunc(UISceneWidget eventObj)
        {
            if (DB.Instance.GetTAccountTable() == null)
            {
                UIManager.Instance.SetVisible("LoadSceneWeight", true);
            }
            else
            {
                UIManager.Instance.SetVisible("Dialog_Cover", true);
            }

        }
    }

}
