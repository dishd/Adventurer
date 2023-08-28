using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ns
{
    /// <summary>
    /// 
    /// </summary>
    public class InitRole : MonoBehaviour
    {
        private Vector3 Vector3 = new Vector3(35.85f, 0, 17.21f);
        
         private void Start()
        {
            GameObject go = GameObject.FindGameObjectWithTag("Player");
            go.transform.position = Vector3;
        }
    }

}
