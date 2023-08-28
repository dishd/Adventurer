using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;



/*
 * 1. ���������� : �̳���Editor�ֻ࣬��Ҫ��unity��������ִ�еĴ��롣
 * 2. �˵��� ���� [MenuItem("......")] : ����������Ҫ��Unity�������в����˵���ť�ķ���
 * 3. AssetDatabase : ֻ�����ڱ������в�����Դ����ع��ܡ�
 * 4. StreamingAssets : unity����Ŀ¼֮һ����Ŀ¼�е��ļ����ᱻѹ�����ʺ����ƶ��˶�ȡ��Դ(��PC�ζ˿���д��)
 *    �־û�·�� Application.presistentDataPath ·������������ʱ���ж�д����,Unity�ⲿĿ¼(��װ����ʱ�Ų���) 
 */
/// <summary>
/// ���������ļ���
/// </summary>
public class GenerateResConfig : Editor
{
    [MenuItem("Tools/Resources/Generate ResConfig File")]
    public static void Generate()
    {
        // ������Դ�����ļ�
        // 1. ���� Resources Ŀ¼������Ԥ�Ƽ�����·��
         string[] resFiles = AssetDatabase.FindAssets("t:prefab", new string[] {"Assets/Resources"});
        // GUID
        for (int i = 0; i < resFiles.Length; i++) 
        {
            resFiles[i] = AssetDatabase.GUIDToAssetPath(resFiles[i]);
            // 2. ���ɶ�Ӧ��ϵ
            // ���� = ·��
            string fileName = Path.GetFileNameWithoutExtension(resFiles[i]);
            string filePath = resFiles[i].Replace("Assets/Resources/", string.Empty).Replace(".prefab", string.Empty);
            resFiles[i] = fileName + "=" + filePath;
        }

        // 3. д���ļ�
        File.WriteAllLines("Assets/StreamingAssets/ConfigMap.txt" ,resFiles);
        AssetDatabase.Refresh();

    }
}


