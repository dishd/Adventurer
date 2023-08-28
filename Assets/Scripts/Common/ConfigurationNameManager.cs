using Common;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace ns
{
    /// <summary>
    /// 配置文件里面的数据
    /// </summary>
    public  class ConfigurationNameManager
    {
        public static Dictionary<string, Dictionary<string, string>> configMap;
        private static string mainKey;
        // 作用 : 初始化类的静态数据成员
        // 时机 : 类别加载时执行一次
        static ConfigurationNameManager()
        {
            configMap = new Dictionary<string, Dictionary<string, string>>();
            // 加载文件
            string fileContent = ConfigurationReader.GetConfigFile("config.txt");

            // 解析文件 (string ---> Dictionary<string,string>)
            // BuildMap(fileContent);
            ConfigurationReader.Reader(fileContent, BuildMap);
        }

        private static void BuildMap(string line)
        {
            //line = Regex.Replace(line, "\n", "");
            //  int i = line.Length;
            line = line.Trim();
            if (string.IsNullOrEmpty(line)) return;
            //if (line.StartsWith("")) { return; }
            if (line.StartsWith("["))
            {
                mainKey = line.Substring(1, line.Length - 2);
                configMap[mainKey] = new Dictionary<string, string>();
            }
            else
            {
                string[] arr = line.Replace(" ", "").Split("=");
                //if (arr[0] != "" && arr[1] != "")
                    configMap[mainKey].Add(arr[0], arr[1]);
            }
          
        }

        public static string GetValue(string mainKey,string key)
        {
            return configMap[mainKey][key];
        }

    }

}
