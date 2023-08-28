using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ns
{
    /// <summary>
    /// 通过ngui设置物体激活与否
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
