using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace myBagFrame
{
    /// <summary>
    /// 
    /// </summary>
    public class BagManager : MonoSingleton<BagManager>
    {

        private Dictionary<string, BagPanel> dic;

        public override void Init()
        {
            base.Init();
            dic = new Dictionary<string, BagPanel>();

            BagPanel[] bagPanels = FindObjectsOfType<BagPanel>();
            if (bagPanels != null)
            {

                for (int i = 0; i < bagPanels.Length; i++)
                {
                    dic.Add(bagPanels[i].gameObject.name, bagPanels[i]);
                }
            }
        }

        public BagPanel GetBagPenelByName(string name)
        {
            if (!dic.ContainsKey(name)) return null;
            return dic[name];
        }


    }

}
