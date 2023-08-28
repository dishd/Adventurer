using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ns
{
    /// <summary>
    /// 
    /// </summary>
    public class Dialog_Eixt : UIScene
    {
        private UILabel message1;
        private UILabel message2;

        protected override void Start()
        {
            base.Start();

            message1 = transform.FindChildCompentByName<UILabel>("Message1");
            message2 = transform.FindChildCompentByName<UILabel>("Message2");

            message1.text = ConfigurationNameManager.GetValue("Battle", "Message1");
            message2.text = ConfigurationNameManager.GetValue("Battle", "Message2");

            GetWidget("CancelButton").MouseClick = CancelFunc;
            GetWidget("OkButton").MouseClick = OkFunc;

        }

        private void OkFunc(UISceneWidget eventObj)
        {
            Global.Contain3DScene = true;
            Global.LoadSceneName = "MainCity";
            Global.LoadUIName = "InteractivePage";

            GameObject.FindGameObjectWithTag("EasyTouch").transform.GetChild(0).gameObject.SetActive(false);
            Global.hideEtc = true;

            SceneManager.LoadScene("LoadPage");
        }

        private void CancelFunc(UISceneWidget eventObj)
        {
            SetVisible(false);
        }
    }

}
