using ARPGDemo01.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ns
{
    /// <summary>
    /// 
    /// </summary>
    [RequireComponent(typeof(CharacterInputController))]   
    
    public class CharacterInfos : MonoBehaviour
    {
        [HideInInspector]
        public string JobModelName;
        [HideInInspector]
        public UnityEngine.Object jobPrefabs;
        [HideInInspector]
        public int glod;
        [HideInInspector]
        public int diamond;

        public void SetMoney()
        {
            glod = CharacterTemplate.Instance.gold;
            diamond = CharacterTemplate.Instance.diamond;
        }

        public void SetJobModelName()
        {
            JobModelName = CharacterTemplate.Instance.jobModel;
        }
    }

}
