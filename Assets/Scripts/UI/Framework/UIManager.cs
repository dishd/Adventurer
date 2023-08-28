using Common;
using System.Collections.Generic;

namespace NGUI.Framework
{
    /// <summary>
    /// UI 管理器 : 管理 (记录/隐藏) 所有窗口, 提供查找窗口的方法。
    /// </summary>
    
    public class UIManager : MonoSingleton<UIManager>
    {
        // Key 窗口类名称       value 窗口对象引用
        private Dictionary<string, UIWindow> uiWindowDic;

        public override void Init()
        {
            base.Init();
            uiWindowDic = new Dictionary<string, UIWindow>();
            UIWindow[] uiWindowArr = FindObjectsOfType<UIWindow>(true);
            
            
            // UIMainWindow[] uw = FindObjectsOfType<UIMainWindow>();

            for (int i =0; i < uiWindowArr.Length; i++)
            {
                // 脚本的名字 是 游戏物体的名字
                //隐藏窗口
                uiWindowArr[i].SetVisible(false);
                // 记录窗口
                AddWindow(uiWindowArr[i]);
            }
        }

        /// <summary>
        /// 添加新窗口，用于动态创建的窗口
        /// </summary>
        /// <param name="window">窗口对象</param>
        public void AddWindow(UIWindow window)
        {
            uiWindowDic.Add(window.GetType().Name, window);
        }
        /// <summary>
        /// 根据类型查找窗口的功能
        /// </summary>
        /// <typeparam name="T">需要查找的窗口类型</typeparam>
        /// <returns></returns>
        public T GetWindow<T>() where T : class
        {
            string key = typeof(T).Name; // 类名
            if (!uiWindowDic.ContainsKey(key)) return null;
            return uiWindowDic[key] as T;
        }
    }

}
