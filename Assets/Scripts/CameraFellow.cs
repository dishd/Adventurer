using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPGDemo01.Camera
{
    /// <summary>
    /// 
    /// </summary>
    
    public class CameraFellow : MonoBehaviour
    {
        //摄像机
        //[Tooltip("摄像机的Transition组件")]
        private Transform m_Transform;
        //人物主角
        [Tooltip("主角的Transition组件")]
        public Transform player_Transform;
        [Tooltip("摄像机的旋转速度")]
        public float rotateSpeed;
       

        private Vector3 offset;

        void Start()
        {
            m_Transform = gameObject.GetComponent<Transform>();

            //offset是摄像机相对于人物主角的相对位置
            offset = new Vector3(0, 0, 0);
            
        }

        void Update()
        {
            //直接改变摄像机的位置（这种方式比较生硬，建议使用下一种插值的方式）
            m_Transform.position = player_Transform.position + offset;

            //插值的方式控制摄像机的跟随
            //m_Transform.position = Vector3.Lerp(m_Transform.position, player_Transform.position + offset, rotateSpeed * Time.deltaTime);
            //if (Vector3.Distance(m_Transform.position, player_Transform.position + offset) < 0.1f)
            //{
            //    m_Transform.position = player_Transform.position + offset;
            //}
        }
    }

}
