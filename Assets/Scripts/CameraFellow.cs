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
        //�����
        //[Tooltip("�������Transition���")]
        private Transform m_Transform;
        //��������
        [Tooltip("���ǵ�Transition���")]
        public Transform player_Transform;
        [Tooltip("���������ת�ٶ�")]
        public float rotateSpeed;
       

        private Vector3 offset;

        void Start()
        {
            m_Transform = gameObject.GetComponent<Transform>();

            //offset�������������������ǵ����λ��
            offset = new Vector3(0, 0, 0);
            
        }

        void Update()
        {
            //ֱ�Ӹı��������λ�ã����ַ�ʽ�Ƚ���Ӳ������ʹ����һ�ֲ�ֵ�ķ�ʽ��
            m_Transform.position = player_Transform.position + offset;

            //��ֵ�ķ�ʽ����������ĸ���
            //m_Transform.position = Vector3.Lerp(m_Transform.position, player_Transform.position + offset, rotateSpeed * Time.deltaTime);
            //if (Vector3.Distance(m_Transform.position, player_Transform.position + offset) < 0.1f)
            //{
            //    m_Transform.position = player_Transform.position + offset;
            //}
        }
    }

}
