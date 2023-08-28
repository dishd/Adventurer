using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ns
{
    /// <summary>
    /// 
    /// </summary>
    public class InitUIBattle : MonoBehaviour
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

            SetTure();


        }

       private void SetTure()
        {
            mUIManager.SetVisible("Panel_RoleStatus", true);
            mUIManager.SetVisible("Panel_Exit", true);
            mUIManager.SetVisible("Panel_Skill", true);
           
        }

    }

}
