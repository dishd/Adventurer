using ARPGDemo.Skill;
using ARPGDemo01.Character;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace ns
{
    /// <summary>
    /// 
    /// </summary>
    public class InitMainCity : MonoBehaviour
    {
        
        private CharacterInfos infos;

        private Object playerPrefabs;

        private GameObject playerGameObject;
        private PlayerStatus playerStatus;

        private Transform playTansf;
        
        private void Awake()
        {
            playTansf = GameObject.FindGameObjectWithTag("PlayerPos").transform;
            playerGameObject = GameObject.FindGameObjectWithTag("Player");
            if (playerGameObject == null)
            {
                playerPrefabs = Resources.Load(CharacterTemplate.Instance.jobModel);

                playerGameObject = Instantiate(playerPrefabs, playTansf.transform, true) as GameObject;

                playerGameObject.AddComponent<CharacterSkillSystem>();

                infos = playerGameObject.AddComponent<CharacterInfos>();
                //playerGameObject.AddComponent<CharacterInputController>();

                //playerGameObject.AddComponent<PlayerStatus>();
                //playerGameObject.AddComponent<CharacterMotor>();
                //playerGameObject.AddComponent<CharacterSkillSystem>();
                //playerGameObject.AddComponent<CharacterSkillManager>();

                SkillData[] skills = DB.Instance.GetSkillDatasByDB("T_Skill" + CharacterTemplate.Instance.jobID);

                AttackButtonData.attackDatas = new int[3];
                for (int i = 0; i < 3; i ++)
                {
                    AttackButtonData.attackDatas[i] = skills[i].skillID;
                }
                //PutData();


                infos.JobModelName = CharacterTemplate.Instance.jobModel;
                infos.jobPrefabs = playerPrefabs;
                infos.SetMoney();

                playerStatus = playerGameObject.GetComponent<PlayerStatus>();
                playerStatus.setCharacterStatus();

               

                 playerGameObject.GetComponent<CharacterSkillManager>().skills = skills;

            }
            
            playerGameObject.transform.position = Global.postion;



        }

        private void PutData()
        {
            playerStatus.HP = CharacterTemplate.Instance.maxHP;
            playerStatus.SP = CharacterTemplate.Instance.maxMP;
            playerStatus.maxHP = CharacterTemplate.Instance.maxHP;
            playerStatus.maxSP = CharacterTemplate.Instance.maxMP;
            playerStatus.lv = CharacterTemplate.Instance.lv;
            playerStatus.atkSpeed = CharacterTemplate.Instance.attackSpeed;
            playerStatus.attackDistance = 1;
            playerStatus.exp = CharacterTemplate.Instance.expCur;
            playerStatus.attackInterval = 2;
            playerStatus.defence = 0;
            playerStatus.intellect = CharacterTemplate.Instance.intellect;
            playerStatus.baseATK = CharacterTemplate.Instance.force;

            CharacterInfos x = playerStatus.GetComponent<CharacterInfos>();
            x.diamond = CharacterTemplate.Instance.diamond;
            x.glod = CharacterTemplate.Instance.gold;
        }
    }

}
