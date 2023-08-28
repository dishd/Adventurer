using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

namespace Common
{
    /// <summary>
    /// ��Դ������
    /// </summary>
    public class ResourceManager
    {
        
        private static Dictionary<string, string> configMap;
        // ���� : ��ʼ����ľ�̬���ݳ�Ա
        // ʱ�� : ������ʱִ��һ��
        static ResourceManager()
        {
            configMap = new Dictionary<string, string>();
            // �����ļ�
            string fileContent = ConfigurationReader.GetConfigFile("ConfigMap.txt");

            // �����ļ� (string ---> Dictionary<string,string>)
            // BuildMap(fileContent);
            ConfigurationReader.Reader(fileContent, BuildMap);
        }

        private static void BuildMap(string line)
        {
            // ����������
            string[] keyValue = line.Split('=');
            //�ļ��� keyValue[0]   ·�� keyValue[1]
            configMap.Add(keyValue[0], keyValue[1]);
        }


        //private static void BuildMap(string fileContent)
        //{
        //    configMap = new Dictionary<string, string>();
        //    // �ļ���=·��\r\n�ļ���=·��
        //    //fileContent.Split()
        //    // StringReader �ַ�����ȡ�����ṩ�����ж�ȡ�ַ�������
        //    using (StringReader reader = new StringReader(fileContent))
        //    {
        //        string line;
        //        // 1. ��һ�У��������������
        //        while ((line = reader.ReadLine()) != null)
        //        {
        //            // ����������
        //            string[] keyValue = line.Split('=');
        //            //�ļ��� keyValue[0]   ·�� keyValue[1]
        //            configMap.Add(keyValue[0], keyValue[1]);

        //        }
        //        // 1.�ȶ�һ��
        //        //string line = reader.ReadLine();
        //        // 2.��Ϊ������� / 4.���ж�����
        //        //while (line != null)
        //        //{
        //        //    string[] keyValue = line.Split('=');
        //        //    //�ļ��� keyValue[0]   ·�� keyValue[1]
        //        //    configMap.Add(keyValue[0], keyValue[1]);
        //        // 3. �ٶ�һ��
        //        //    line = reader.ReadLine();
        //        //}
        //    } // �������˳� using ���룬���Զ����� reader.Dispose() ����


        //}

        public static T Load<T>(string prefabName) where T : UnityEngine.Object
        {


            //string fileContent = GetConfigFile("ConfigMap.txt");
            if (prefabName == "") return null;
            // prefabName ---> prefabPath
            string prefabPath = configMap[prefabName];
            return Resources.Load<T>(prefabPath);
        }
    }

}
