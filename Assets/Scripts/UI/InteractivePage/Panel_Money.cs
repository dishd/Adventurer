using Common;
using Mono.Data.Sqlite;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace ns
{
    /// <summary>
    /// 
    /// </summary>
    public class Panel_Money : UIScene
    {
        private UILabel goldLabel;
        private UILabel diamondLabel;
        private CharacterInfos characterInfos;

        private new void Start()
        {
            goldLabel = transform.FindChildCompentByName<UILabel>("GoldLabel");
            diamondLabel = transform.FindChildCompentByName<UILabel>("DiamondLabel");
            characterInfos = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterInfos>();
            SetGoldLabel(characterInfos.glod);
            SetDiamondLabel(characterInfos.diamond);
           
        }

        public void SettMoneyByDB()
        {
            SqliteDataReader sqReader = DB.Instance.db.Execute("Select * form T_Money");
            SetGoldLabel((int)sqReader[1]);
            SetDiamondLabel((int)sqReader[2]);
        }

        public void SetGoldLabel(int gold)
        {
            goldLabel.text = gold.ToString();
        }

        public void SetDiamondLabel(int diamond)
        {
            diamondLabel.text = diamond.ToString();
        }
        
    }

}
