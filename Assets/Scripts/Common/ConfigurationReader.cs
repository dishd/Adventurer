using System;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

namespace Common
{
    /// <summary>
    /// �����ļ���ȡ��
    /// </summary>
    public class ConfigurationReader
    {
        /// <summary>
        /// ��ȡ�����ļ�
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetConfigFile(string fileName)
        {
            #region �����汾
            // ConfigMap.txt
            //string url = "file://" + Application.streamingAssetsPath + "/ConfigMap.txt";

            //WWW www = new WWW(url);

            //while (true)
            //{
            //    if (www.isDone)
            //    {
            //        return www.text;
            //    }

            //}

            //string url;
            //#if UNITY_EDITOR || UNITY_STANDALONE
            //            url = "file://" + Application.dataPath + "/StramingAssets/" + fileNmae;
            //#elif UNITY_IPHONE
            //            url = url = "file://" + Application.dataPath + "/Raw/" + fileNmae;
            //#elif UNITY_ANDROID
            //            url = "file://" + Application.dataPath + "!/Assets/" + fileNmae;
            //#endif
            //string url = "file://" + Application.streamingAssetsPath + fileNmae;
            #endregion
            string myurl = Application.streamingAssetsPath + "/" + fileName;
            #region ��ƽ̨�ж� StramingAssets
            // ����ڱ������� ���� pc(������)
            // if (Application.platform == RuntimePlatform.Adnorid)
            //Unity ���ǩ
#if UNITY_EDITOR || UNOTY_STANDALONE
            //myurl = "file://" + Application.dataPath + "/StreamingAssets/" + fileName;
            //myurl = Application.streamingAssetsPath +  "/ConfigMap.txt";
#elif UNITY_IPHONE
            //url = "file://" + Application.dataPath + "/Raw/" + fileName;
           myurl  = Application.persistentDataPath + fileName;
#elif UNITY_ANDROID
            //rul = "jar:file://" + Application.dataPath + "!/assets/" + fileName;
            myurl  = Application.persistentDataPath + fileName;
#endif
            #endregion
            //UnityWebRequest www = UnityWebRequest.Get(myurl);
            //www.SendWebRequest();

            //while (!www.downloadHandler.isDone) ;

            //return www.downloadHandler.text;

            WWW www = new WWW(myurl);
            while (true)
            {
                if (www.isDone)
                {
                    return www.text;
                }

            }
            return null;
        }



        /// <summary>
        /// ��ȡ�����ļ�
        /// </summary>
        /// <param name="fileContent">�ļ�����</param>
        /// <param name="handler">�����߼�</param>
        public static void Reader(string fileContent, Action<string> handler)
        {

            using (StringReader reader = new StringReader(fileContent))
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                                    
                     handler(line);
                }
            }
        }
    }

}
