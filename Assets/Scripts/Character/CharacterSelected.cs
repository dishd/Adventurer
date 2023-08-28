using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPGDemo01.Character
{
    /// <summary>
    /// 角色选择器
    /// </summary>
    public class CharacterSelected : MonoBehaviour
    {
        private GameObject selectedGO;
        private float hideTime;
        [Tooltip("选择器物体名称")]
        public string selectedName = "seleted";

        public float displayTime = 3;
        private void Start()
        {
            selectedGO = transform.Find(selectedName).gameObject;
        }
        public void SetSelecteActive(bool state)
        {
            // 设置选择器物体激活状态
            selectedGO.SetActive(state);
            //设置当前脚本激活状态 (开启 / 停止 Update 调用)
            this.enabled = state;
            // 等待3秒 禁用物体
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
