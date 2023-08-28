using ns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    /// <summary>
    /// 对象池
    /// </summary>
    
    /// 可重置
    public interface IResetable
    {
        void OnReset();
    }
    public class GameObjectPool : MonoSingleton<GameObjectPool>
    {
        /*
         * 使用方法:
         * 1. 使用平方创建 / 销毁的物体，都通过对象池创建/ 回收。
         * GameObjectPool.Instance.CreateObject("类别", 预制件, 位置, 旋转);
         * GameObjectPool.Instance.CollectObject(游戏对象);
         * 2. 需要通过对象池创建的物体，如需每次创建时执行，则让脚本实现IResetable接口
         */
        private Dictionary<string, List<GameObject>> cache;

        public override void Init()
        {
            base.Init();
            cache = new Dictionary<string, List<GameObject>>();
        }
        /// <summary>
        /// 通过对象池 创建对象
        /// </summary>
        /// <param name="key">类别</param>
        /// <param name="prefab"需要创建实例的预制件></param>
        /// <param name="pos">位置</param>
        /// <param name="rotate">旋转</param>
        /// <returns></returns>
        public GameObject CreateObject(string key, GameObject prefab, Vector3 pos, Quaternion rotate)
        {
            GameObject go = FindUsableObject(key);

            if (go == null)
            {
                go = AddObject(key, prefab);  // 创建物体       // 设置目标点 -->

            }

            // 使用
            UseObject(pos, rotate, go); // 设置位置 / 旋转
            return go;

        }

        //使用对象
        private  void UseObject(Vector3 pos, Quaternion rotate, GameObject go)
        {
            go.transform.position = pos;
            go.transform.rotation = rotate;
            go.SetActive(true);

            // 设置目标点
            //go.GetComponent<Bullet>().方法();
            // 抽象
            //go.GetComponent<IResetable>().OnReset();
            // 遍历执行物体中所有需要重置的逻辑
            foreach(var item in go.GetComponents<IResetable>())
            {
                item.OnReset();
            }
        }

        // 添加对象
        private GameObject AddObject(string key, GameObject prefab)
        {
            // 创建对象
            GameObject go = Instantiate(prefab);
            // 加入池中
            // 如果池中没有 key 则添加记录
            if (!cache.ContainsKey(key)) cache.Add(key, new List<GameObject>());
            cache[key].Add(go);
            return go;
        }

        // 查找指定类别中 可以使用的对象
        private GameObject FindUsableObject(string key)
        {
            GameObject go = null;
            if (cache.ContainsKey(key))
                go = cache[key].Find(g => !g.activeInHierarchy);
            return go;
        }

        /// <summary>
        /// 回收对象
        /// </summary>
        /// <param name="go">需要被回收的游戏对象</param>
        /// <param name="delay">延迟时间 默认为0</param>
        public void CollectObject(GameObject go,float delay = 0)
        {
            StartCoroutine(CollectObjectDelay(go,delay));
        }
        public IEnumerator CollectObjectDelay(GameObject go, float delay)
        {
            yield return new WaitForSeconds(delay);
            go.SetActive(false);
        }

        // 清空莫个类别
        public void Clear(string key)
        {
            // Destory(游戏对象)
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

            // 类别
            cache.Remove(key);
        }

        // 清空全部
        public void ClearAll()
        {
            // foreach 只读元素
            // 遍历 字典 集合
            //foreach(var key in cache.Keys) // 异常 : 无效的操作
            //{
            //    Clear(key); // 移除字典记录  cache.Remove(key);
            //}

            // 将字典中所有建存入List集合中
            List<string> keyList = new List<string>(cache.Keys);

            foreach (var key in keyList)
            {
                Clear(key);
            }

        }


    }



}
