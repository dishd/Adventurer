using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ns
{
    /// <summary>
    /// 
    /// </summary>
    public class Test1 : MonoBehaviour
    {
        private UIToggle uiToggle;
        private List<EventDelegate> aaa;
        private EventDelegate aaaa;

        public void PrintOne(int a)
        {
            print(1);
        }
        public void Print(int a )
        {
            print(2);
           
        }

       
    }

}
