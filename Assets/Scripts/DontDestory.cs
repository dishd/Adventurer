using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ns
{
    /// <summary>
    /// 不删除的场景
    /// </summary>
    public class DontDestory : MonoBehaviour
    {
        /// <summary>
        /// 解决DontDestroyOnLoad场景多次创建的问题
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
