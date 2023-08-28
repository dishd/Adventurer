using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace ns
{
    /// <summary>
    /// 
    /// </summary>
    public class Panel_Exit : UIScene
    {

        private UILabel tiemLabel;
        public int time = 0;
        protected override void Start()
        {
            base.Start();

            GetWidget("ExitButton").MouseClick = ExitButton;
            tiemLabel = transform.FindChildCompentByName<UILabel>("TiemLabel");


            InvokeRepeating("SetTime",0,1);
        }

      

        private void SetTime()
        {
            tiemLabel.text = Global.GetMinuteTime(time);
            if (time >= 300)
                 UIManager.Instance.SetVisible("Panel_Loser", true);
            time++;
           
        }

        private void ExitButton(UISceneWidget eventObj)
        {
            UIManager.Instance.SetVisible("Dialog_Eixt", true);
        }


    }

}
