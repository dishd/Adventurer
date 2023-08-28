using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ns
{
    /// <summary>
    /// 
    /// </summary>
    public class UIIterInit : MonoBehaviour
    {
        private UIManager mUIManager;

        private void Start()
        {
            Object obj = FindObjectOfType(typeof(UIManager));
            if (obj)
                mUIManager = obj as UIManager;
            if (mUIManager == null)
            {
                GameObject uiManager = new GameObject("UIManager");
                mUIManager = uiManager.AddComponent<UIManager>();
            }
            mUIManager.InitializeUIs();
            InitSetTure();
        }

        private void InitSetTure()
        {
            //mUIManager.SetVisible("SkillList", true);
            //mUIManager.SetVisible("SkillButtonList", true);
            mUIManager.SetVisible("Panel_Menu", true);
            UIManager.Instance.SetVisible("SkillButtonList", true);
            UIManager.Instance.SetVisible("SkillList", true);
            UIManager.Instance.SetVisible("Panel_RoleStatus", true);
            UIManager.Instance.SetVisible("Panel_Money", true);
            UIManager.Instance.SetVisible("Panel_MinMap", true);


        }
    }

}
