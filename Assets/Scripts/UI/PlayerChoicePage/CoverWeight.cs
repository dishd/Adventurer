using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace ns
{
    /// <summary>
    /// 弹出的覆盖对话框
    /// </summary>
    public class CoverWeight : UIScene
    {

        private UILabel uiLabel;
        //private Transform transformOK;
        //private Transform transformCancel;
        protected override void Start()
        {
            base.Start();
            uiLabel = transform.FindChildCompentByName<UILabel>("CoverLable");
            GetWidget("DeletDataButton").MouseClick = DeteDataButton;
            GetWidget("CloaseCoverButton").MouseClick = CloseButton;

            uiLabel.text = ConfigurationNameManager.GetValue("StandAlone", "Cover");
            //transformOK = transform.FindChildByName("DeletDataButton");
            //transformCancel = transform.FindChildByName("CloaseCoverButton");

        }

        private void CloseButton(UISceneWidget eventObj)
        {
            SetVisible(false);
        }

        private void DeteDataButton(UISceneWidget eventObj)
        {
            DB.Instance.db.Execute("UPDATE  T_Account SET AccountName = null");
            DB.Instance.db.DeleteContents("T_Character");
            DB.Instance.db.Execute("UPDATE T_Money SET Gold = 0, Diamond = 0");
            SetVisible(false);
        }

    }

}
