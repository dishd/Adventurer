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
    public class LoadSceneWeight : UIScene
    {

        public string nextLoadSceneName;
        public string buttonName;
        //public UIInput uiLabel;

        protected override void Start()
        {
            base.Start();
            mUIName = "LoadSceneWeight";

            GetWidget(buttonName).MouseClick = MousePressFunction;
        }

        private void MousePressFunction(UISceneWidget eventObj)
        {
            Global.LoadSceneName = "PlayerChoicePage";
      

            SceneManager. LoadScene("LoadPage");
        }

       
    }

}
