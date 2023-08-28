using Mono.Data.Sqlite;
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
    public class Button_Continue : UIScene
    {
        protected override void Start()
        {
            base.Start();
            GetWidget("ContinueGameFont").MouseClick += ClickFunc;
        }

        private void ClickFunc(UISceneWidget eventObj)
        {
            if (DB.Instance.GetTAccountTable() == null)
            {
                UIManager.Instance.SetVisible("Dialog_NoData", true);
            }
            else
            {
                CharacterTemplate.Instance.characterName = DB.Instance.GetTAccountTable();
                SqliteDataReader reader = DB.Instance.db.ReadFullTable("T_Character");
                CharacterTemplate.Instance.jobID = (int)reader[1];
                CharacterTemplate.Instance.lv = (int)reader[2];
                CharacterTemplate.Instance.expCur = (int)reader[3];
                CharacterTemplate.Instance.force = (int)reader[4];
                CharacterTemplate.Instance.intellect = (int)reader[5];
                CharacterTemplate.Instance.attackSpeed = (int)reader[6];
                CharacterTemplate.Instance.maxHP = (int)reader[7];
                CharacterTemplate.Instance.maxMP = (int)reader[8];
                CharacterTemplate.Instance.damageMax = (int)reader[9];
                CharacterTemplate.Instance.jobModel = reader[10].ToString();
                SqliteDataReader reader2 = DB.Instance.db.ReadFullTable("T_Character");
                CharacterTemplate.Instance.gold = (int)reader2[1];
                CharacterTemplate.Instance.diamond = (int)reader2[2];


                Global.LoadUIName = "InteractivePage";
                Global.LoadSceneName = "MainCity";
                Global.Contain3DScene = true;

                SceneManager.LoadScene("LoadPage");

            }
        }
    }

}
