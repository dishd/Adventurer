using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace ns
{
    /// <summary>
    /// С��ͼ������ 
    /// �ο�:https://blog.csdn.net/alayeshi/article/details/115961694 
    ///       https://www.cnblogs.com/zhangbaochong/p/4856646.html
    /// </summary>
    public class testmin : MonoBehaviour
    {
        public Transform minicamera;
        public Transform player;
        private Transform miniplayerIcon;


        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            minicamera = GameObject.FindGameObjectWithTag("MapCamera").transform;
            
            miniplayerIcon = GameObject.FindGameObjectWithTag("PlayerPosInMap").transform;
        }
        void Update()
        {
            minicamera.position = player.position + Global.mapAndRoleOffset;
            miniplayerIcon.eulerAngles = new Vector3(0, 0, -90 - player.eulerAngles.y);
        }

    }

}
