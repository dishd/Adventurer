using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ns
{
    /// <summary>
    /// ͨ��ngui�������弤�����
    /// </summary>
    public class EventSetActive : MonoBehaviour
    {
        public void SetEnable()
        {
            gameObject.SetActive(true);
        }

        public void SetDisable()
        {
            gameObject.SetActive(false);
        }
    }

}
