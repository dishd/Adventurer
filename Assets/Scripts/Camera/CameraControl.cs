using ARPGDemo01.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ns
{
    /// <summary>
    /// ������������
    /// 
    /// ����ο�:https://blog.csdn.net/qq_52324195/article/details/121582220
    /// 
    /// �Ƕȸ���:https://blog.csdn.net/weixin_43673589/article/details/124195858
    /// </summary>
    public class CameraControl : MonoBehaviour
    {
        private Vector3 myDafaultDir;//Ĭ�Ϸ���
        [SerializeField]
        public Vector3 myCurrentDir;//��ǰ����
        private Transform myPlayerTransform;//���Transform
        private Vector3 myRotateValue;//�������洢��תֵ
        private Vector3 myPitchRotateAxis;//����������ת��
        private Vector3 myYawRotateAxis;//���Һ�������ת��
        private float myCurrentDistance; //��ǰ����
        private float myDistanceRecoveryDelayCounter; //�ӳ�ʱ��
        [Header("����۲����")]
        public float distance = 4f;//����۲����
        [Header("����۲��������")]
        public Vector2 distanceLimit = new Vector2(1f, 4f);
        [Header("�����ת�ٶ�")]
        public float moveSpeed = 120f;//�����ת�ٶ�
        [Header("�۲�Ŀ��ƫ����")]
        public Vector3 offset = new Vector3(0f, 1.5f, 0f);//�۲�Ŀ��ƫ����
        [Header("�Ӿ�����ٶ�")]
        [Range(0, 1)]
        public float stadiaAdjustmentSpeed;

        [Header("pitch��ת")]
        public bool invertPitch = true;//��תpitch�����������
        [Header("pitch�Ƕ�����")]
        public Vector2 pitchLimit = new Vector2(-40f, 70f);//pitch����Ƕ�Լ��
        private float obstacleSphereRadius = 2;
        [Header("�����ڸǺ󣬾�ͷ��Զ�ӳ�ʱ��")]
        private float distanceRecoveryDelay = 2f;
        [Header("�����ڸǺ󣬾�ͷ�����ٶ�")]
        public float distanceRecoverySpeed = 1f;

        public int obstacleLayerMask = 6;

        private void OnEnable()
        {
            myCurrentDir = myDafaultDir;
            myCurrentDistance = distance;
            //��������ϵ��y�᷽��
            Vector3 upAxis = -Physics.gravity.normalized;
            //�ҵ����Transform
            myPlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            //ͨ�����������ҵķ�������ͶӰ�ڵ����ϵĵ�λ����-��ó�ʼ����
            //����ʹ�õ���ķ�����
            myDafaultDir = Vector3.ProjectOnPlane((transform.position - myPlayerTransform.position), upAxis).normalized;
            //������ת��
            myYawRotateAxis = upAxis;
            //������ת�ᣬʹ�����������������y�᷽��Ĳ���õ�
            myPitchRotateAxis = Vector3.Cross(upAxis, Vector3.ProjectOnPlane(transform.forward, upAxis));
        }

        private void Update()
        {
            Vector2 inputDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

            //���º�����תֵ
            myRotateValue.x += inputDelta.x * moveSpeed * Time.smoothDeltaTime;
            myRotateValue.x = AngleCorrection(myRotateValue.x);
            //����������תֵ
            #region �����Ƕ�Լ��
            myRotateValue.y += inputDelta.y * moveSpeed * (invertPitch ? -1 : 1) * Time.smoothDeltaTime;
            myRotateValue.y = AngleCorrection(myRotateValue.y);
            myRotateValue.y = Mathf.Clamp(myRotateValue.y, pitchLimit.x, pitchLimit.y);
            #endregion

            //����������Ԫ��,ͨ����Ԫ��������AngleAxis(�Ƕ�,��ת��)��ý�����ת
            Quaternion horizontalQuat = Quaternion.AngleAxis(myRotateValue.x, myYawRotateAxis);//����
            Quaternion verticalQuat = Quaternion.AngleAxis(myRotateValue.y, myPitchRotateAxis);//����
            //ע�������˳ƿ���ֻ��Ҫ�������ɶȣ�����ҪRoll
            Vector3 finalDir = horizontalQuat * verticalQuat * myDafaultDir;//������Ԫ����ת������շ���
            myCurrentDir = finalDir;


            //Vector3 from = myPlayerTransform.TransformPoint(offset);//���ƫ�ƺ��α���λ��
            Vector3 from = myPlayerTransform.localToWorldMatrix.MultiplyPoint3x4(offset);//���ƫ�ƺ��α���λ��
            Vector3 to = from + finalDir * distance;//��ӹ۲�������������λ��


            //ʡ��ǰ���ִ���
            //Vector3 finalDir = horizontalQuat * verticalQuat * myDafaultDir;//������Ԫ����ת������շ���
            //myCurrentDir = finalDir;
            ////Vector3 from = myPlayerTransform.TransformPoint(offset);//���ƫ�ƺ��α���λ��
            //Vector3 from = myPlayerTransform.localToWorldMatrix.MultiplyPoint3x4(offset);//���ƫ�ƺ��α���λ��
            //Vector3 to = from + finalDir * distance;//��ӹ۲�������������λ��
            //                                        //�ڴ˴���Ӳ�ֵ�ָ���⼴��
            //CameraMoveDelay(from, to);
            //transform.position = from + finalDir * myCurrentDistance;
            transform.position = from + finalDir * myCurrentDistance;
            transform.LookAt(from);
            //characterMotor.LookAtTarget(new Vector3(transform.forward.normalized.x, 0, transform.forward.z))

        }

        private Vector3 ObstacleProcess(Vector3 from, Vector3 to)
        {
            //��ô�α���λ�õ��������λ�õķ�������
            Vector3 dir = (to - from).normalized;
            //������λ���Ƿ���⵽�ϰ������⵽�򱨴�
            //if (Physics.CheckSphere(from, obstacleSphereRadius, obstacleLayerMask))
            //    Debug.Log("�����ϰ���������뾶ӦС�ڽ�ɫ����");
            RaycastHit hit = default;
            //ʹ��Բ�μ���Ƿ����ϰ������������߼��λ�õĸ�����Բ�ΰ뾶λ�ø�ֵ
            if (Physics.SphereCast(new Ray(from, dir), obstacleSphereRadius, out hit, distance, obstacleLayerMask))
            {
                return hit.point + (-dir * obstacleSphereRadius);
            }

           
            return to;
        }

        private void CameraMoveDelay(Vector3 from, Vector3 to)
        {
            //��ʱλ�ô洢
            Vector3 exceptTo = ObstacleProcess(from, to);
            //��ȡ�ƶ�����
            float expectDistance = Vector3.Distance(exceptTo, from);
            if (expectDistance < myCurrentDistance)//����������������ӳ�
            {
                //����˲ʱ����
                myCurrentDistance = expectDistance;
                myDistanceRecoveryDelayCounter = distanceRecoveryDelay;
            }
            else//���Ϊ��Զ���ӳ�һ��ʱ������Զ
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
