using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ns
{
    /// <summary>
    /// 
    /// </summary>
    public class TestHUD : MonoBehaviour
    {
        public HUDText hudText;
        private void Start ()
        {
            hudText = GetComponent<HUDText>();  
        }
        void Update()
        {
            
            // 0f��ʾ��������ǰ��ͣ��ʱ��stayDuration
            hudText.Add(Time.deltaTime * 10f, Color.red, 0f);
           
            
        }
    }

}
