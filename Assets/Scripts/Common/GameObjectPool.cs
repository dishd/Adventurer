using ns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    /// <summary>
    /// �����
    /// </summary>
    
    /// ������
    public interface IResetable
    {
        void OnReset();
    }
    public class GameObjectPool : MonoSingleton<GameObjectPool>
    {
        /*
         * ʹ�÷���:
         * 1. ʹ��ƽ������ / ���ٵ����壬��ͨ������ش���/ ���ա�
         * GameObjectPool.Instance.CreateObject("���", Ԥ�Ƽ�, λ��, ��ת);
         * GameObjectPool.Instance.CollectObject(��Ϸ����);
         * 2. ��Ҫͨ������ش��������壬����ÿ�δ���ʱִ�У����ýű�ʵ��IResetable�ӿ�
         */
        private Dictionary<string, List<GameObject>> cache;

        public override void Init()
        {
            base.Init();
            cache = new Dictionary<string, List<GameObject>>();
        }
        /// <summary>
        /// ͨ������� ��������
        /// </summary>
        /// <param name="key">���</param>
        /// <param name="prefab"��Ҫ����ʵ����Ԥ�Ƽ�></param>
        /// <param name="pos">λ��</param>
        /// <param name="rotate">��ת</param>
        /// <returns></returns>
        public GameObject CreateObject(string key, GameObject prefab, Vector3 pos, Quaternion rotate)
        {
            GameObject go = FindUsableObject(key);

            if (go == null)
            {
                go = AddObject(key, prefab);  // ��������       // ����Ŀ��� -->

            }

            // ʹ��
            UseObject(pos, rotate, go); // ����λ�� / ��ת
            return go;

        }

        //ʹ�ö���
        private  void UseObject(Vector3 pos, Quaternion rotate, GameObject go)
        {
            go.transform.position = pos;
            go.transform.rotation = rotate;
            go.SetActive(true);

            // ����Ŀ���
            //go.GetComponent<Bullet>().����();
            // ����
            //go.GetComponent<IResetable>().OnReset();
            // ����ִ��������������Ҫ���õ��߼�
            foreach(var item in go.GetComponents<IResetable>())
            {
                item.OnReset();
            }
        }

        // ��Ӷ���
        private GameObject AddObject(string key, GameObject prefab)
        {
            // ��������
            GameObject go = Instantiate(prefab);
            // �������
            // �������û�� key ����Ӽ�¼
            if (!cache.ContainsKey(key)) cache.Add(key, new List<GameObject>());
            cache[key].Add(go);
            return go;
        }

        // ����ָ������� ����ʹ�õĶ���
        private GameObject FindUsableObject(string key)
        {
            GameObject go = null;
            if (cache.ContainsKey(key))
                go = cache[key].Find(g => !g.activeInHierarchy);
            return go;
        }

        /// <summary>
        /// ���ն���
        /// </summary>
        /// <param name="go">��Ҫ�����յ���Ϸ����</param>
        /// <param name="delay">�ӳ�ʱ�� Ĭ��Ϊ0</param>
        public void CollectObject(GameObject go,float delay = 0)
        {
            StartCoroutine(CollectObjectDelay(go,delay));
        }
        public IEnumerator CollectObjectDelay(GameObject go, float delay)
        {
            yield return new WaitForSeconds(delay);
            go.SetActive(false);
        }

        // ���Ī�����
        public void Clear(string key)
        {
            // Destory(��Ϸ����)
            // cache[key] --> List<GameObject>

            //for (int i = 0; i < cache[key].Count; i++)
            //{
            //    Destroy(cache[key][i]);
            //}

            //for (int i  = cache[key].Count - 1; i >= 0; i--)
            //{
            //    Destroy(cache[key][i]);
            //}

            foreach(var item in cache[key])
            {
                Destroy(item);
            }

            // ���
            cache.Remove(key);
        }

        // ���ȫ��
        public void ClearAll()
        {
            // foreach ֻ��Ԫ��
            // ���� �ֵ� ����
            //foreach(var key in cache.Keys) // �쳣 : ��Ч�Ĳ���
            //{
            //    Clear(key); // �Ƴ��ֵ��¼  cache.Remove(key);
            //}

            // ���ֵ������н�����List������
            List<string> keyList = new List<string>(cache.Keys);

            foreach (var key in keyList)
            {
                Clear(key);
            }

        }


    }



}
