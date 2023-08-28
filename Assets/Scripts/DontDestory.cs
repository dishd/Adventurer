using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ns
{
    /// <summary>
    /// ��ɾ���ĳ���
    /// </summary>
    public class DontDestory : MonoBehaviour
    {
        /// <summary>
        /// ���DontDestroyOnLoad������δ���������
        /// https://blog.csdn.net/Czhenya/article/details/85079527
        /// </summary>
        /// 
        public string tagName;
        void Awake()
        {
            GameObject[] objs = GameObject.FindGameObjectsWithTag(tagName);

            if (objs.Length > 1)
            {
                Destroy(gameObject);
            }

            DontDestroyOnLoad(gameObject);
        }

    }

}
