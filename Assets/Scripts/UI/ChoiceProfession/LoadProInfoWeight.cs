using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace ns
{
    /// <summary>
    /// 
    /// </summary>
    public class LoadProInfoWeight :UIScene
    {
        public string ID;
        private string job;
        private string description;
        private string weapon;
        [Tooltip("点击对象的名称")]
        public string widgetName;


        protected override void Start()
        {
            base.Start();

            job = "Job" + ID;
            description = "Description" + ID;
            weapon = "Weapon" + ID;
            if (ID == "1") SetRoleDescripion.Instance.SetDescription(ConfigurationNameManager.configMap["Job"][weapon],
                ConfigurationNameManager.configMap["Job"][job],
                ConfigurationNameManager.configMap["Job"][description]);

            GetWidget(widgetName).MouseClick = MouseClickDown;

        }

        private void MouseClickDown(UISceneWidget eventObj)
        {
            SetRoleDescripion.Instance.SetDescription(ConfigurationNameManager.configMap["Job"][weapon],
                ConfigurationNameManager.configMap["Job"][job],
                ConfigurationNameManager.configMap["Job"][description]);
        }

        //private void MouseClickDown(UISceneWidget eventObj, bool isDown)
        //{
        //    SetRoleDescripion.Instance.SetDescription(" ", ConfigurationNameManager.configMap["Job"][weapon],
        //        ConfigurationNameManager.configMap["Job"][job],
        //        ConfigurationNameManager.configMap["Job"][description]);

        //    //Dictionary<string,Dictionary<string, string>> s = ConfigurationNameManager.configMap;
        //}
    }

}
