using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    /// <summary>
    /// �仯���������
    /// </summary>
    
    public static class TransformHelper
    {
        /// <summary>
        /// δ֪�㼶,���Һ��ָ�����Ƶı任�����
        /// </summary>
        /// <param name="currentTF">��ǰ�任���</param>
        /// <param name="childName">�����������</param>
        /// <returns></returns>
        public static Transform FindChildByName(this Transform currentTF, string childName)
        {
            // �ݹ� : �����ڲ��е�������Ĺ��̡�
            // 1.���������в���
            if (currentTF.name == childName) { return currentTF; }
            Transform childTF = currentTF.Find(childName);
            if (childTF != null) { return childTF; }

            for (int i = 0; i < currentTF.childCount; i++)
            {
                // 2. �������������
                childTF = FindChildByName(currentTF.GetChild(i), childName);
                if (childTF != null) return childTF;
            }
            return null;
        }      

        public static T FindChildCompentByName<T>(this Transform currentTF, string compentName) where T : Component
        {
            Transform tf = FindChildByName(currentTF, compentName);
            if (tf == null) return null;
            return tf.GetComponent<T>();
        }

        public static void ChildrenDelegate(this Transform go, Action<Transform> action)
        {
            if (go == null) return;

            for (int i = 0; i < go.transform.childCount; i++)
            {
                Transform t1 = go.transform.GetChild(i);
                if (t1 != null)
                {
                    ChildrenDelegate(t1,action);
                    action(go);
                }
            }
            if (go != null)
            {
                action(go);
            }
        }
    }

}
