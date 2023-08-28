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
    public class Panel_Map : UIScene
    {
        protected override void Start()
        {
            base.Start();

            GetWidget("Close").MouseClick = MouseClick;
            GetWidget("Center").MouseClick = EnterBattle;
        }

        private void EnterBattle(UISceneWidget eventObj)
        {
            GameObject.FindGameObjectWithTag("EasyTouch").transform.GetChild(0).gameObject.SetActive(false);
            Global.hideEtc = true;
            Global.LoadSceneName = "11";
            Global.LoadUIName = "UIBattle";
            Global.Contain3DScene = true;

            SceneManager.LoadScene("LoadPage");
        }

        private void MouseClick(UISceneWidget eventObj)
        {
            SetVisible(false);
        }
    }

}
