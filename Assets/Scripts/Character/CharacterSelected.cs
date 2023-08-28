using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPGDemo01.Character
{
    /// <summary>
    /// ��ɫѡ����
    /// </summary>
    public class CharacterSelected : MonoBehaviour
    {
        private GameObject selectedGO;
        private float hideTime;
        [Tooltip("ѡ������������")]
        public string selectedName = "seleted";

        public float displayTime = 3;
        private void Start()
        {
            selectedGO = transform.Find(selectedName).gameObject;
        }
        public void SetSelecteActive(bool state)
        {
            // ����ѡ�������弤��״̬
            selectedGO.SetActive(state);
            //���õ�ǰ�ű�����״̬ (���� / ֹͣ Update ����)
            this.enabled = state;
            // �ȴ�3�� ��������
            if (state)
                hideTime = Time.time + displayTime;
        }

        private void Update()
        {
            if (hideTime <= Time.time)
            {
                SetSelecteActive(false);
            }
        }
    }

}
