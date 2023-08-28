using ARPGDemo01.Character;
using Common;
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
    public class Panel_Win : UIScene
    {
        private UILabel expLabel;
        private UISprite[] sprites;
        private float t;

       private PlayerStatus status;
        private CharacterInfos characterInfos;
        protected override void Start()
        {
            base.Start();

            status = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();
            characterInfos = status.GetComponent<CharacterInfos>();

            sprites = new UISprite[3];
            expLabel = transform.FindChildCompentByName<UILabel>("ExpLabel");

            for (int i = 0;  i < 3; i++)
            {
                int t = i + 1;
                sprites[i] = transform.FindChildCompentByName<UISprite>("ShowStart" + t);
            }

            GetWidget("Close").MouseClick = MouseClick;


        }

        private void OnEnable()
        {
            Panel_Exit p = UIManager.Instance.GetUI<Panel_Exit>("Panel_Exit");
            if (p == null) return;
            t = p.time;

            if (t > 120)
            {
                Set(1);
            }
            else if (t > 60)
            {
                Set(2);
            }
            else
            {
                Set(3);
            }
        }

        private void MouseClick(UISceneWidget eventObj)
        {
            GameObject.FindGameObjectWithTag("EasyTouch").transform.GetChild(0).gameObject.SetActive(false);
            Global.hideEtc = true;
            Global.Contain3DScene = true;
            Global.LoadSceneName = "MainCity";
            Global.LoadUIName = "InteractivePage";

            SceneManager.LoadScene("LoadPage");
        }

        public void Set(int start)
        {
            SqliteDataReader reader = DB.Instance.db.Execute("SELECT * FROM T_Scene WHERE SceneName = 11");
            int expIndex = start + 3;
            int goldIndex = start + 6;
            int exp = (int)reader[expIndex];
            int glod = (int)reader[goldIndex];
            int i = 0;
            for (; i < start; i++)
            {
                sprites[i].gameObject.SetActive(true);
            }

            for (; i < 3; i++)
            {
                sprites[i].gameObject.SetActive(false);
            }

            expLabel.text = exp.ToString();
            status.exp += exp;
            characterInfos.glod += glod;
            DB.Instance.db.UpdateInto("T_Money",new string[] { "Gold" },new string[] { characterInfos.glod.ToString() }, "AccountID", "1");

            SqliteDataReader reader1 = DB.Instance.db.Execute(string.Format("SELECT * FROM T_Exp WHERE Lv = {0}",status.lv));
            int ExpUp = (int)reader1[2];
            if (status.exp > ExpUp)
            {
                status.exp = status.exp - ExpUp;
                status.lv++;
                status.baseATK += 5;
                status.intellect += 5;
                status.HP += 1000;
                status.maxHP += 1000;
                status.SP += 500;
                status.maxSP += 500;

                DB.Instance.db.UpdateInto("T_Character", new string[] { "Lv", "ExpCur", "Force", "Intellect", "MaxHP", "MaxMP" },
                    new string[] {status.lv.ToString(), status.exp.ToString(), status.baseATK.ToString(), status.intellect.ToString(), status.maxHP.ToString(),
                    status.maxSP.ToString()}, "CharacterID", "1");

            }

            DB.Instance.db.UpdateInto("T_Character",  new string[] { "ExpCur" }, new string[] { status.exp.ToString() }, "CharacterID", "1");


        }

    }

}
