using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NGUI.Framework
{
    /// <summary>
    /// UI 窗口基类 : 定义所有窗口共有成员 (显隐)
    /// </summary>
    
    public class UIWindow : MonoBehaviour
    {
        private UIPanel uiPanel;
        // private VRTK_UICanvas uiCanvas;
        private Dictionary<string, UIEventListener> uiEventDic;

        private void Awake()
        {
            uiPanel = GetComponent<UIPanel>();
            // uiCanvas = getComponent<VRTK_UICanvas>();
            uiEventDic = new Dictionary<string, UIEventListener>();
        }

        /// <summary>
        /// 设置窗口可见性
        /// </summary>
        /// <param name="state">显隐状态</param>
        /// /// <param name="delay">延迟时间(默认参数)</param>
        public void SetVisible(bool state, float delay = 0)
        {
           StartCoroutine(SetVisibleDelay(state, delay));

            //SetVisible(true) ; // "立即"显示 等待1帧
            //SetVisible(true, 2) ; // 延时2秒显示
        }

        public IEnumerator SetVisibleDelay(bool state, float delay)
        {
            yield return new WaitForSeconds(delay);

            // CanvasGroup
            uiPanel.alpha = state ? 1 : 0;
            // VRTK Canvas
            // uiCanvas.enabled = state;

        }

        /// <summary>
        /// 根据子物体名称获取UI监听器
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public UIEventListener GetUIEventListener(string name)
        {
            if (!uiEventDic.ContainsKey(name))
            {
                Transform tf = transform.FindChildByName(name);
                UIEventListener uiEvent = UIEventListener.GetListener(tf);
                uiEventDic.Add(name, uiEvent);
            }
            return uiEventDic[name];
            

        }
    }

}
