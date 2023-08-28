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
    public class Panel_Loser : UIScene
    {
        protected override void Start()
        {
            base.Start();

            GetWidget("Close").MouseClick = MouseClickFunc;
        }

        private void MouseClickFunc(UISceneWidget eventObj)
        {
            GameObject.FindGameObjectWithTag("EasyTouch").transform.GetChild(0).gameObject.SetActive(false);
            Global.hideEtc = true;
            Global.Contain3DScene = true;
            Global.LoadSceneName = "MainCity";
            Global.LoadUIName = "InteractivePage";

            SceneManager.LoadScene("LoadPage");
        }
    }

}
