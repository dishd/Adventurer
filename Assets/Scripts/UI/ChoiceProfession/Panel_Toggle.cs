using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ns
{
    /// <summary>
    /// 
    /// </summary>
    public class Panel_Toggle : UIScene
    {
        protected override void Start()
        {
            base.Start();
            Init();

            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetComponentInChildren<UISceneWidget>().MouseClick = MousClickFunc;
            }
        }

        private void MousClickFunc(UISceneWidget eventObj)
        {
            string name = eventObj.name;
            name = name.Substring(name.Length - 1);

            //print(name);

            SetRoleDescripion.Instance.SetDescription(ConfigurationNameManager.configMap["Job"]["Weapon" + name],
                ConfigurationNameManager.configMap["Job"]["Job" + name],
                ConfigurationNameManager.configMap["Job"]["Description" + name]);
        }

        private void Init()
        {
            //    setroledescripion.instance.setdescription(configurationnamemanager.configmap["job"]["weapon1"],
            //       configurationnamemanager.configmap["job"]["job1"],
            //       configurationnamemanager.configmap["job"]["description1"]);
            //}

            SetRoleDescripion.Instance.SetDescription(ConfigurationNameManager.configMap["Job"]["Weapon1"],
               ConfigurationNameManager.configMap["Job"]["Job1"],
               ConfigurationNameManager.configMap["Job"]["Description1"]);
        }
    }

}
