using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.FSM
{
    /// <summary>
    /// AI �����ļ�������
    /// </summary>
    public class AIConfigurationReader
    {
        // ���ݽṹ
        // ���ֵ� : key ״̬         value ӳ��
        // С�ֵ� : key �������   value ״̬���
        public Dictionary<string, Dictionary<string, string>> Map { get; private set; }

        public AIConfigurationReader(string fileName) 
        {
            Map = new Dictionary<string, Dictionary<string, string>>();
            // ��ȡ�����ļ�
            string configFile = ConfigurationReader.GetConfigFile(fileName);
            //���������ļ�
            ConfigurationReader.Reader(configFile, BuildMap);
        }

        private string mainKey;
        private void BuildMap(string line)
        {
            // line --> Map
            // 1.ȥ���հ�(������� ��Ϊ���ַ���)
            line = line.Trim();
            //if (line == "" || line == null) return;
            if (string.IsNullOrEmpty(line)) return;
            
            // ����� [ ��ͷ
            if (line.StartsWith("[")) // 2. ״̬
            {
                // [Idle]
                mainKey = line.Substring(1, line.Length - 2);
                //״̬
                Map.Add(mainKey, new Dictionary<string, string>());
            } 
            else
            {
                // 3.ӳ�� NoHealth>Dead
                string[] keyValue = line.Split('>');
                Map[mainKey].Add(keyValue[0], keyValue[1]);

            }

    
        }
    }

}
