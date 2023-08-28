using ARPGDemo.Skill;
using Common;
using Mono.Data.Sqlite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using UnityEngine;
using UnityEngine.Networking;

namespace ns
{
    /// <summary>
    /// PlayerChoicePage场景下的数据库操作
    /// </summary>
    public class DB : MonoSingleton<DB>
    {
        public DbAccess db;

        /// <summary>	/// 数据库路径	/// </summary>
        private string appDBPath;

        public string dbName = "ARPG.db";

        public override void Init()
        {
            base.Init();
            db = new DbAccess();

            CreateDataBase();
        
        }

        /// <summary>	/// 创建/打开数据库	/// </summary>
        private void CreateDataBase()
        {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
            appDBPath = Application.streamingAssetsPath + "/" + dbName;
#elif UNITY_ANDROID || UNITY_IPHONE
		appDBPath = Application.persistentDataPath + "/" +dbName;
		if(!File.Exists(appDBPath)){
			StartCoroutine(CopyDataBase());
		}
#endif
            db = new DbAccess("URI=file:" + appDBPath);
        }

        /// <summary>	/// 拷贝数据库	/// </summary>
        /// <returns>数据库.</returns>
        private IEnumerator CopyDataBase()
        {
            WWW loadDB = new WWW(Application.streamingAssetsPath + "/ARPG.db");

            yield return loadDB;
            File.WriteAllBytes(appDBPath, loadDB.bytes);
        }

        private void OnDestroy()
        {
            db.CloseSqlConnection();
        }

        public string GetTAccountTable()
        {
            SqliteDataReader reader = db.ReadFullTable("T_Account");
            string returnName = "";
            int id;
            while (reader.Read())
            {
                id = reader.GetOrdinal("AccountName");
                if (reader.IsDBNull(id)) return null;
                returnName = reader.GetString(id);
            }
            return returnName;
        }

        /// <summary>
        /// 通过表的名字读取数据
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        /// 
        const BindingFlags InstanceBindFlags = BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;

        public SkillData[] GetSkillDatasByDB(string tableName)
        {
            List<SkillData> skillDatas = new List<SkillData>();
            SqliteDataReader sqReader = db.ReadFullTable(tableName);
            

            while (sqReader.Read())
            {
                //SkillData skillData = new SkillData();
                //skillData.skillID = (int)sqReader[1];
                //skillData.skillCon = sqReader[2].ToString();
                //skillData.description = sqReader[3].ToString();
                //skillData.name = sqReader[4].ToString();
                //skillData.durationTime = (float)sqReader[5];
                //skillData.atkInterval = (float)sqReader[6];
                //skillData.atkRatio = (float)sqReader[7];
                //skillData.coolTime = (int)sqReader[8];
                //skillData.costSP = (int)sqReader[9];
                //skillData.attackDistance = (float)sqReader[10];
                //skillData.attackTargetTags = new string[] { sqReader[11].ToString() };
                //skillData.level = (int)sqReader[12];
                //skillData.prefabName = sqReader[13].ToString();
                //skillData.DamageMode = (int)sqReader[14];
                //skillData.attackType = (SkillAttackType)sqReader[15];
                //skillData.animationName = sqReader[16].ToString();
                //skillData.attackAngle = (float)sqReader[17];
                //skillData.hitFxName = sqReader[18].ToString();
                //skillData.nextBatterId = (int)sqReader[19];

                //skillDatas.Add(skillData);

                SkillData skillData = new SkillData();
                Type t = typeof(ARPGDemo.Skill.SkillData);
                int i = 1;
              
                foreach (var item in t.GetProperties())
                {
                    
                    if (item.PropertyType.Equals(typeof(string)))
                    {
                        item.SetValue(skillData, sqReader[i].ToString());
                    }
                    else if (item.PropertyType.Equals(typeof(float)))
                    {
                        item.SetValue(skillData, float.Parse(sqReader[i].ToString()));
                    }
                    else if (item.PropertyType.Equals(typeof(string[])))
                    {
                        string[] str = sqReader[i].ToString().Split(',');
                        item.SetValue(skillData, str);
                    }
                    else
                    {
                        item.SetValue(skillData, int.Parse(sqReader[i].ToString()));
                    }
                    i++;
                }

                skillDatas.Add(skillData);

            }

            return skillDatas.ToArray();
        }
    }

}
