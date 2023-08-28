using UnityEngine;

namespace ARPGDemo01.Character
{
    /// <summary>
    /// 角色马达:负责控制角色移动
    /// </summary>
    public class CharacterMotor : MonoBehaviour
    {
        [Tooltip("旋转速度")]
        public float rotateSpeed = 25f;
        [Tooltip("移动速度")]
        public float moveSpeed = 6f;
        private CharacterController controller;
        


        private void Start ()
        {
            controller = GetComponentInChildren<CharacterController>();
          
        }

        /// <summary>
        /// 注释目标方向选择
        /// </summary>
        /// <param name="direction">注释方向</param>
        public void LookAtTarget(Vector3 direction)
        {

            //print(direction);
            if (direction == Vector3.zero) return; // 如果是 0 点的话，下面会报错，使用加上
            Quaternion lookDir = Quaternion.LookRotation(direction);

            
            transform.rotation = Quaternion.Lerp(transform.rotation, lookDir, rotateSpeed * Time.deltaTime); // Time.deltaTime 是 使旋转的时间固定，此写法是在固定的时间内，旋转指定的角度
            if (Quaternion.Angle(transform.rotation, lookDir) < 0.1f) transform.rotation = lookDir;
        }

     
        /// <summary>
        /// 移动
        /// </summary>
        /// <param name="direction">方向</param>
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
