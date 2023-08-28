using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.CanvasScaler;

namespace ns
{
    /// <summary>
    /// 
    /// </summary>
    public class ToggleControleCP : UIScene
    {
        private UIToggle[] toggles;
        
        protected override void Start()
        {
            base.Start();
            Init();

            //toggles = FindObjectsOfType<UIToggle>();

            for (int i = 0; i < toggles.Length; i++)
            {
               // toggles[i].GetComponent<UISceneWidget>().MouseClick = MousClickFunc;
            }
            

        }

        private void MousClickFunc(UISceneWidget eventObj)
        {
            string name = eventObj.name;
            name = name.Substring(name.Length- 1);

            //print(name);

            SetRoleDescripion.Instance.SetDescription(ConfigurationNameManager.configMap["Job"]["Weapon" + name],
                ConfigurationNameManager.configMap["Job"]["Job" + name],
                ConfigurationNameManager.configMap["Job"]["Description" + name]);
        }

        private void Init()
        {
            SetRoleDescripion.Instance.SetDescription(ConfigurationNameManager.configMap["Job"]["Weapon1"],
               ConfigurationNameManager.configMap["Job"]["Job1"],
               ConfigurationNameManager.configMap["Job"]["Description1"]);
        }
    }

}
