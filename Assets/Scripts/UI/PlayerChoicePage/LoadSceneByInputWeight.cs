using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ns
{
    /// <summary>
    /// 
    /// </summary>
    public class LoadSceneByInputWeight : LoadSceneWeight
    {
        public UIInput uiInput;
        protected override void Start()
        {
            base.Start();
            GetWidget(buttonName).MouseClick = ClickFunc;
        }

        private void ClickFunc(UISceneWidget eventObj)
        {
            Global.LoadSceneName = nextLoadSceneName;
            string s = uiInput.value;
            CharacterTemplate.Instance.characterName = uiInput.value;
            //DB.Instance.db.UpdateInto("T_Account", new string[] { "AccountName" }, new string[] {uiInput.value}, "AccountID", "0" );
            //s ="UPDATE T_Account SET AccountName = " + s + " WHERE AccountID = 0";
            s = string.Format("UPDATE T_Account SET AccountName = '{0}' ", uiInput.value);
            //s = "UPDATE T_Account SET AccountName = ";
            //print(s);
            //DB.Instance.db.Execute("UPDATE  T_Account SET AccountName = ''");
            DB.Instance.db.Execute(s);

            SceneManager.LoadScene("LoadPage");
        }


    }

}
