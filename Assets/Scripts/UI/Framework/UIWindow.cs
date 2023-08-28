using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NGUI.Framework
{
    /// <summary>
    /// UI ���ڻ��� : �������д��ڹ��г�Ա (����)
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
        /// ���ô��ڿɼ���
        /// </summary>
        /// <param name="state">����״̬</param>
        /// /// <param name="delay">�ӳ�ʱ��(Ĭ�ϲ���)</param>
        public void SetVisible(bool state, float delay = 0)
        {
           StartCoroutine(SetVisibleDelay(state, delay));

            //SetVisible(true) ; // "����"��ʾ �ȴ�1֡
            //SetVisible(true, 2) ; // ��ʱ2����ʾ
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
        /// �������������ƻ�ȡUI������
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
