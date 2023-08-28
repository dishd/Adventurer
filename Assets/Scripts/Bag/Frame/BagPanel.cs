using System.Collections.Generic;
using UnityEngine;

namespace myBagFrame
{
    /// <summary>
    /// 
    /// </summary>
    public class BagPanel : MonoBehaviour
    {
      
        [Tooltip("������Ԫ:������װ����Ʒ")]
        public GameObject bagUnit;

        protected UIGrid uiGrid;

        protected Dictionary<string, BagItem> dic;

        protected virtual void Start()
        {
            dic = new Dictionary<string, BagItem>();
            uiGrid = transform.GetComponentInChildren<UIGrid>();
        }

        public void AddItem(BagItem item)
        {
            dic.Add(item.ID.ToString(), item);
        }


        public void DeleteItemById(string id)
        {
            DestroyAllChild(dic[id].transform);
            dic.Remove(id);
        }

        private void DestroyAllChild(Transform go)
        {
            if (go == null) return;

            for (int i = 0; i < go.transform.childCount; i++)
            {
                Transform t1 = go.transform.GetChild(i);
                if (t1 != null)
                {
                    DestroyAllChild(t1);
                    Destroy(go.gameObject);
                }
            }
            if (go != null)
            {
                Destroy(go.gameObject);
            }

        }

    }
}


