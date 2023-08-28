using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.FSM
{
    /// <summary>
    /// AI�����ļ���ȡ������
    /// </summary>
    public class AIConfigurationReaderFactory 
    {
        private static Dictionary<string, AIConfigurationReader> cache;

        static AIConfigurationReaderFactory()
        {
            cache = new Dictionary<string, AIConfigurationReader>();
        }
        public static Dictionary<string, Dictionary<string, string>> GetMap(string fileName)
        {
            if (!cache.ContainsKey(fileName))
            {
               cache.Add(fileName, new AIConfigurationReader(fileName));
            }
            return cache[fileName].Map;
        }
    }

}
