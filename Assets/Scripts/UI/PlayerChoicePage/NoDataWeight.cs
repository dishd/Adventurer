using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ns
{
    /// <summary>
    /// û�����ݴ浵�Ŀ��
    /// </summary>
    public class NoDataWeight : UIScene
    {
        private UILabel uiLbael;

        protected override void  Start()
        {
            uiLbael = transform.FindChildCompentByName<UILabel>("ArchiveLable");
            uiLbael.text = ConfigurationNameManager.GetValue("StandAlone", "Archive");
            GetWidget("ArchiveButton").MouseClick = OnMouseClickFunc ;
        }

        public void SetLabel(string label)
        {
            uiLbael.text = label;
        }

        private void OnMouseClickFunc(UISceneWidget eventObj)
        {
            UIManager.Instance.SetVisible(gameObject.name, false);
        }
    }

}
