using ARPGDemo01.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ns
{
    /// <summary>
    /// 摄像机跟随代码
    /// 
    /// 代码参考:https://blog.csdn.net/qq_52324195/article/details/121582220
    /// 
    /// 角度概念:https://blog.csdn.net/weixin_43673589/article/details/124195858
    /// </summary>
    public class CameraControl : MonoBehaviour
    {
        private Vector3 myDafaultDir;//默认方向
        [SerializeField]
        public Vector3 myCurrentDir;//当前方向
        private Transform myPlayerTransform;//玩家Transform
        private Vector3 myRotateValue;//控制器存储旋转值
        private Vector3 myPitchRotateAxis;//俯仰方向旋转轴
        private Vector3 myYawRotateAxis;//左右横向方向旋转轴
        private float myCurrentDistance; //当前距离
        private float myDistanceRecoveryDelayCounter; //延迟时间
        [Header("相机观测距离")]
        public float distance = 4f;//相机观测距离
        [Header("相机观测距离限制")]
        public Vector2 distanceLimit = new Vector2(1f, 4f);
        [Header("相机旋转速度")]
        public float moveSpeed = 120f;//相机旋转速度
        [Header("观测目标偏移量")]
        public Vector3 offset = new Vector3(0f, 1.5f, 0f);//观测目标偏移量
        [Header("视距调整速度")]
        [Range(0, 1)]
        public float stadiaAdjustmentSpeed;

        [Header("pitch反转")]
        public bool invertPitch = true;//反转pitch方向相机滑动
        [Header("pitch角度限制")]
        public Vector2 pitchLimit = new Vector2(-40f, 70f);//pitch方向角度约束
        private float obstacleSphereRadius = 2;
        [Header("物体遮盖后，镜头拉远延长时间")]
        private float distanceRecoveryDelay = 2f;
        [Header("物体遮盖后，镜头拉进速度")]
        public float distanceRecoverySpeed = 1f;

        public int obstacleLayerMask = 6;

        private void OnEnable()
        {
            myCurrentDir = myDafaultDir;
            myCurrentDistance = distance;
            //世界坐标系：y轴方向
            Vector3 upAxis = -Physics.gravity.normalized;
            //找到玩家Transform
            myPlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            //通过摄像机与玩家的方向向量投影在地面上的单位向量-获得初始方向
            //地面使用地面的法向量
            myDafaultDir = Vector3.ProjectOnPlane((transform.position - myPlayerTransform.position), upAxis).normalized;
            //横向旋转轴
            myYawRotateAxis = upAxis;
            //俯仰旋转轴，使用摄像机朝向与世界y轴方向的叉积得到
            myPitchRotateAxis = Vector3.Cross(upAxis, Vector3.ProjectOnPlane(transform.forward, upAxis));
        }

        private void Update()
        {
            Vector2 inputDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

            //更新横向旋转值
            myRotateValue.x += inputDelta.x * moveSpeed * Time.smoothDeltaTime;
            myRotateValue.x = AngleCorrection(myRotateValue.x);
            //更新纵向旋转值
            #region 俯仰角度约束
            myRotateValue.y += inputDelta.y * moveSpeed * (invertPitch ? -1 : 1) * Time.smoothDeltaTime;
            myRotateValue.y = AngleCorrection(myRotateValue.y);
            myRotateValue.y = Mathf.Clamp(myRotateValue.y, pitchLimit.x, pitchLimit.y);
            #endregion

            //构建角轴四元数,通过四元数方法：AngleAxis(角度,旋转轴)获得角轴旋转
            Quaternion horizontalQuat = Quaternion.AngleAxis(myRotateValue.x, myYawRotateAxis);//横向
            Quaternion verticalQuat = Quaternion.AngleAxis(myRotateValue.y, myPitchRotateAxis);//俯仰
            //注：第三人称控制只需要两个自由度，不需要Roll
            Vector3 finalDir = horizontalQuat * verticalQuat * myDafaultDir;//叠加四元数旋转获得最终方向
            myCurrentDir = finalDir;


            //Vector3 from = myPlayerTransform.TransformPoint(offset);//获得偏移后的伪相机位置
            Vector3 from = myPlayerTransform.localToWorldMatrix.MultiplyPoint3x4(offset);//获得偏移后的伪相机位置
            Vector3 to = from + finalDir * distance;//添加观测距离后的相机最终位置


            //省略前部分代码
            //Vector3 finalDir = horizontalQuat * verticalQuat * myDafaultDir;//叠加四元数旋转获得最终方向
            //myCurrentDir = finalDir;
            ////Vector3 from = myPlayerTransform.TransformPoint(offset);//获得偏移后的伪相机位置
            //Vector3 from = myPlayerTransform.localToWorldMatrix.MultiplyPoint3x4(offset);//获得偏移后的伪相机位置
            //Vector3 to = from + finalDir * distance;//添加观测距离后的相机最终位置
            //                                        //在此处添加插值恢复检测即可
            //CameraMoveDelay(from, to);
            //transform.position = from + finalDir * myCurrentDistance;
            transform.position = from + finalDir * myCurrentDistance;
            transform.LookAt(from);
            //characterMotor.LookAtTarget(new Vector3(transform.forward.normalized.x, 0, transform.forward.z))

        }

        private Vector3 ObstacleProcess(Vector3 from, Vector3 to)
        {
            //获得从伪相机位置到相机最终位置的方向向量
            Vector3 dir = (to - from).normalized;
            //检查最初位置是否会检测到障碍物，若检测到则报错
            //if (Physics.CheckSphere(from, obstacleSphereRadius, obstacleLayerMask))
            //    Debug.Log("错误！障碍物检测球体半径应小于角色胶囊");
            RaycastHit hit = default;
            //使用圆形检测是否有障碍物，如果有则将射线检测位置的负方向圆形半径位置赋值
            if (Physics.SphereCast(new Ray(from, dir), obstacleSphereRadius, out hit, distance, obstacleLayerMask))
            {
                return hit.point + (-dir * obstacleSphereRadius);
            }

           
            return to;
        }

        private void CameraMoveDelay(Vector3 from, Vector3 to)
        {
            //临时位置存储
            Vector3 exceptTo = ObstacleProcess(from, to);
            //获取移动距离
            float expectDistance = Vector3.Distance(exceptTo, from);
            if (expectDistance < myCurrentDistance)//如果拉近，则重置延迟
            {
                //拉近瞬时拉近
                myCurrentDistance = expectDistance;
                myDistanceRecoveryDelayCounter = distanceRecoveryDelay;
            }
            else//如果为拉远，延迟一段时间逐渐拉远
            {
                if (myDistanceRecoveryDelayCounter > 0)
                    myDistanceRecoveryDelayCounter -= Time.deltaTime;
                else
                    myCurrentDistance = Mathf.Lerp(myCurrentDistance, expectDistance, Time.smoothDeltaTime * distanceRecoverySpeed);
            }
        }

        private float AngleCorrection(float value)
        {
            float angle = value - 180;
            if (angle > 0)
            {
                return angle - 180;
            }
            if (value == 0) return 0;
            return value;
        }

    }

}
