using Common;
using System.Collections.Generic;

namespace NGUI.Framework
{
    /// <summary>
    /// UI ������ : ���� (��¼/����) ���д���, �ṩ���Ҵ��ڵķ�����
    /// </summary>
    
    public class UIManager : MonoSingleton<UIManager>
    {
        // Key ����������       value ���ڶ�������
        private Dictionary<string, UIWindow> uiWindowDic;

        public override void Init()
        {
            base.Init();
            uiWindowDic = new Dictionary<string, UIWindow>();
            UIWindow[] uiWindowArr = FindObjectsOfType<UIWindow>(true);
            
            
            // UIMainWindow[] uw = FindObjectsOfType<UIMainWindow>();

            for (int i =0; i < uiWindowArr.Length; i++)
            {
                // �ű������� �� ��Ϸ���������
                //���ش���
                uiWindowArr[i].SetVisible(false);
                // ��¼����
                AddWindow(uiWindowArr[i]);
            }
        }

        /// <summary>
        /// ����´��ڣ����ڶ�̬�����Ĵ���
        /// </summary>
        /// <param name="window">���ڶ���</param>
        public void AddWindow(UIWindow window)
        {
            uiWindowDic.Add(window.GetType().Name, window);
        }
        /// <summary>
        /// �������Ͳ��Ҵ��ڵĹ���
        /// </summary>
        /// <typeparam name="T">��Ҫ���ҵĴ�������</typeparam>
        /// <returns></returns>
        public T GetWindow<T>() where T : class
        {
            string key = typeof(T).Name; // ����
            if (!uiWindowDic.ContainsKey(key)) return null;
            return uiWindowDic[key] as T;
        }
    }

}
