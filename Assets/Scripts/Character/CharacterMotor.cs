using UnityEngine;

namespace ARPGDemo01.Character
{
    /// <summary>
    /// ��ɫ���:������ƽ�ɫ�ƶ�
    /// </summary>
    public class CharacterMotor : MonoBehaviour
    {
        [Tooltip("��ת�ٶ�")]
        public float rotateSpeed = 25f;
        [Tooltip("�ƶ��ٶ�")]
        public float moveSpeed = 6f;
        private CharacterController controller;
        


        private void Start ()
        {
            controller = GetComponentInChildren<CharacterController>();
          
        }

        /// <summary>
        /// ע��Ŀ�귽��ѡ��
        /// </summary>
        /// <param name="direction">ע�ͷ���</param>
        public void LookAtTarget(Vector3 direction)
        {

            //print(direction);
            if (direction == Vector3.zero) return; // ����� 0 ��Ļ�������ᱨ��ʹ�ü���
            Quaternion lookDir = Quaternion.LookRotation(direction);

            
            transform.rotation = Quaternion.Lerp(transform.rotation, lookDir, rotateSpeed * Time.deltaTime); // Time.deltaTime �� ʹ��ת��ʱ��̶�����д�����ڹ̶���ʱ���ڣ���תָ���ĽǶ�
            if (Quaternion.Angle(transform.rotation, lookDir) < 0.1f) transform.rotation = lookDir;
        }

     
        /// <summary>
        /// �ƶ�
        /// </summary>
        /// <param name="direction">����</param>
        public void Movement(Vector3 direction)
        {
                 
            Vector3 h = transform.right.normalized * direction.x;
            Vector3 v = transform.forward.normalized * direction.z;
            Vector3 vvv = v + h;
            //print( direction+" "+h + " "+v + " " +vvv);
            
            LookAtTarget(vvv);
            
            controller.Move(transform.forward * Time.deltaTime * moveSpeed);
            controller.Move(new Vector3(0, -1, 0));

        }      
    }

}
