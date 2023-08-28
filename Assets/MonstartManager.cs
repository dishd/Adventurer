using ARPGDemo.Skill;
using ARPGDemo01.Character;
using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ns
{
    /// <summary>
    /// 
    /// </summary>
    public class MonstartManager : MonoSingleton<MonstartManager>
    {
        private CharacterStatus[] monstartStatus;

        public static int monstartCount = 3;

        public override void Init()
        {
            base.Init();
            GameObject[] gos = GameObject.FindGameObjectsWithTag("Enemy");
            int len = gos.Length;
            monstartStatus = new CharacterStatus[len];

            SkillData[] ss = DB.Instance.GetSkillDatasByDB("T_MonsterSkill");
            for (int i = 0; i < len; i++)
            {
                monstartStatus[i] = gos[i].GetComponent<CharacterStatus>();
                gos[i].GetComponent<CharacterSkillManager>().skills = ss;
            }
        }

        public void MonstartDeath()
        {
            monstartCount--;
            if (monstartCount == 0)
            {
                UIManager.Instance.SetVisible("Panel_Win", true);
            }
        }
    }

}
