using Mono.Data.Sqlite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ns
{
    /// <summary>
    /// 在ChoiceProfession中创建角色的按钮
    /// </summary>
    public class ButtonCreateRoleCP : UIScene
    {
        protected override void Start()
        {
            base.Start();
            GetWidget("Button_Create").MouseClick = ClickFunc;
        }

        private void ClickFunc(UISceneWidget eventObj)
        {
            UIToggle uiToggle = UIToggle.GetActiveToggle(1);
            string Id = uiToggle.gameObject.name;
            Id = Id.Substring(Id.Length - 1);
            CharacterTemplate.Instance.jobID = int.Parse(Id);
            ReadData(Id);
            InsertData();

            Global.LoadSceneName = "MainCity";
            Global.LoadUIName = "InteractivePage";
            Global.Contain3DScene = true;
            SceneManager.LoadScene("LoadPage");

        }

        private void ReadData(string ID)
        {
            SqliteDataReader sqReader = DB.Instance.db.Execute(string.Format("SELECT * FROM T_Job where JobID = {0}", ID));
            sqReader.Read();
            CharacterTemplate.Instance.lv = sqReader.GetInt32(sqReader.GetOrdinal("Lv"));
            CharacterTemplate.Instance.expCur = sqReader.GetInt32(sqReader.GetOrdinal("ExpCur"));
            CharacterTemplate.Instance.force = sqReader.GetInt32(sqReader.GetOrdinal("Force"));
            CharacterTemplate.Instance.intellect = sqReader.GetInt32(sqReader.GetOrdinal("Intellect"));
            CharacterTemplate.Instance.attackSpeed = sqReader.GetInt32(sqReader.GetOrdinal("Speed"));
            CharacterTemplate.Instance.maxHP = sqReader.GetInt32(sqReader.GetOrdinal("MaxHP"));
            CharacterTemplate.Instance.maxMP = sqReader.GetInt32(sqReader.GetOrdinal("MaxMP"));
            CharacterTemplate.Instance.damageMax = sqReader.GetInt32(sqReader.GetOrdinal("DamageMax"));
            CharacterTemplate.Instance.jobModel = sqReader.GetString(sqReader.GetOrdinal("jobModel"));
        }

        private void InsertData()
        {
            DB.Instance.db.InsertInto("T_Character", new string[] {
                "1",
                CharacterTemplate.Instance.jobID.ToString(),
                CharacterTemplate.Instance.lv.ToString(),
                CharacterTemplate.Instance.expCur.ToString(),
                CharacterTemplate.Instance.force.ToString(),
                CharacterTemplate.Instance.intellect.ToString(),
                CharacterTemplate.Instance.attackSpeed.ToString(),
                CharacterTemplate.Instance.maxHP.ToString(),
                CharacterTemplate.Instance.maxMP.ToString(),
                CharacterTemplate.Instance.damageMax.ToString(),
                "'"+CharacterTemplate.Instance.jobModel + "'",
            });
        }
    }

}
