using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//从T     对象        取 一个属性【Age，Tall...,Books】返回这个属性的 值
//学生类 对象  张三                  Age                返回张三年龄的 值 20
//                 李四                  Age                                         25

//从T对象返回某个属性的 值 zs.Age=20   
/// <summary>
/// 选择委托：负责 从某个类型中 选取某个字段 返回这个字段的值
/// 例如：               学生类中          年龄                           值 20!
/// </summary>
/// <typeparam name="T">数据类型： Student</typeparam>
/// <typeparam name="TKey">数据类型的字段的类型 ：Age int</typeparam>
/// <param name="t">数据类型的对象： zsObj</param>
/// <returns>对象的某个字段的值：zsObj.Age  20</returns>
                                     
public delegate TKey SelectHandler<T, TKey>(T t);

/// <summary>
/// 查找条件委托：表示一个查找条件，例如：
/// id=1
/// name="zs"
/// id>1
/// id>1&&name!="zs"&&tall>180
/// </summary>
/// <typeparam name="T"></typeparam>
/// <param name="t"></param>
/// <returns></returns>
public delegate bool  FindHandler<T>(T t);
public static class ArrayHelper
{
    //推导
    //1实现 整数数组排序的方法 冒泡法
    //static public void OrderBy1(int[] array)
    //{
    //    for (int i = 0; i < array.Length; i++)
    //    {
    //        for (int j = i+1; j < array.Length; j++)
    //        {
    //            if (array[i].CompareTo(array[j]) > 0)
    //            {
    //                int temp = array[i];
    //                array[i] = array[j];
    //                array[j] = temp;
    //            }
    //        }
    //    }
    //}
    //static public void OrderBy2(string[] array)
    //{
    //    for (int i = 0; i < array.Length; i++)
    //    {
    //        for (int j = i + 1; j < array.Length; j++)
    //        {
    //            if (array[i].CompareTo(array[j]) > 0)
    //            {
    //                var temp = array[i];
    //                array[i] = array[j];
    //                array[j] = temp;
    //            }
    //        }
    //    }
    //}
    //如果 需要比较对象的其它属性，还要引入一个参数，指明是哪个属性
    //static public void OrderBy3<T>(T[] array)  //用于整数，字符串，对象默认字段 数组比较
    //    where T:IComparable<T>
    //{
    //    for (int i = 0; i < array.Length; i++)
    //    {
    //        for (int j = i + 1; j < array.Length; j++)
    //        {
    //            if (array[i].CompareTo(array[j]) > 0)
    //            {
    //                var temp = array[i];
    //                array[i] = array[j];
    //                array[j] = temp;
    //            }
    //        }
    //    }
    //}
    //static public void OrderBy3<T>(T[] array, IComparer<T> compare)
    //    where T : IComparable<T>//对象 非 默认字段 数组比较
    //{
    //    for (int i = 0; i < array.Length; i++)
    //    {
    //        for (int j = i + 1; j < array.Length; j++)
    //        {
    //            //默认    张三 年龄  李四 年龄  if (array[i].CompareTo(array[j]) > 0)
    //            //非默认 张三 身高  李四 身高  if (compare.Compare(array[i],array[j])>0)             
    //            if (compare.Compare(array[i],array[j])>0)
    //            {
    //                var temp = array[i];
    //                array[i] = array[j];
    //                array[j] = temp;
    //            }
    //        }
    //    }
    //}

    /// <summary>
    /// 升序排序
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    /// <typeparam name="TKey">数据类型字段的类型</typeparam>
    /// <param name="array">数据类型对象的数组</param>
    /// <param name="handler">
    /// 委托对象：负责 从某个类型中选取某个字段 返回这个字段的值
    /// </param>
    static public void OrderBy<T, TKey>
        (this T[] array, SelectHandler<T, TKey> handler)
        where TKey : IComparable<TKey>//对象 非 默认字段 数组比较
    {
        for (int i = 0; i < array.Length; i++)
        {
            for (int j = i + 1; j < array.Length; j++)
            {
                //默认    张三 年龄  李四 年龄  if (array[i].CompareTo(array[j]) > 0)
                //非默认 张三 身高  李四 身高  if (compare.Compare(array[i],array[j])>0)     
                //使用委托取属性的值 去比较
                if (handler(array[i]).CompareTo(handler(array[j]))> 0)
                {
                    var temp = array[i];
                    array[i] = array[j];
                    array[j] = temp;
                }
            }
        }
    }

    /// <summary>
    /// 降序排序
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    /// <typeparam name="TKey">数据类型字段的类型</typeparam>
    /// <param name="array">数据类型对象的数组</param>
    /// <param name="handler">
    /// 委托对象：负责 从某个类型中选取某个字段 返回这个字段的值
    /// </param>
    static public void OrderByDescending<T, TKey>
        (this T[] array, SelectHandler<T, TKey> handler)
        where TKey : IComparable<TKey>
    {
        for (int i = 0; i < array.Length; i++)
        {
            for (int j = i + 1; j < array.Length; j++)
            {              
                if (handler(array[i]).CompareTo(handler(array[j])) < 0)
                {
                    var temp = array[i];
                    array[i] = array[j];
                    array[j] = temp;
                }
            }
        }
    }


    /// <summary>
    /// 返回最大的
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    /// <typeparam name="TKey">数据类型字段的类型</typeparam>
    /// <param name="array">数据类型对象的数组</param>
    /// <param name="handler">
    /// 委托对象：负责 从某个类型中选取某个字段 返回这个字段的值
    /// </param>
    static public T Max<T, TKey>
        (this T[] array, SelectHandler<T, TKey> handler)
        where TKey : IComparable<TKey>
    {
        T temp =default(T);
        temp = array[0];
        for (int i = 1; i < array.Length; i++)
        {
            if (handler(temp).CompareTo(handler(array[i])) < 0)
            {
               temp = array[i];               
            }
        }
        return temp;
    }

    /// <summary>
    /// 返回最小的
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    /// <typeparam name="TKey">数据类型字段的类型</typeparam>
    /// <param name="array">数据类型对象的数组</param>
    /// <param name="handler">
    /// 委托对象：负责 从某个类型中选取某个字段 返回这个字段的值
    /// </param>
    static public T Min<T, TKey>
        (this T[] array, SelectHandler<T, TKey> handler)
        where TKey : IComparable<TKey>
    {
        T temp = default(T);
        temp = array[0];
        for (int i = 1; i < array.Length; i++)
        {
            if (handler(temp).CompareTo(handler(array[i])) > 0)
            {
                temp = array[i];
            }
        }
        return temp;
    }

    //实现数组工具类的 通用的查找的方法 Find
    //给定一个查找的条件？ 返回满足条件的一个
    static public T Find<T>(this T[] array,FindHandler<T> handler)
    {
        T temp = default(T);
        for(int i=0;i<array.Length;i++)
        {
            if (handler(array[i]))
            {
                return array[i];
            }
        }
        return temp;    
    }
    //查找所有的方法 FindAll
    //给定一个查找的条件？ 返回满足条件的所有的
    static public T[] FindAll<T>(this T[] array, FindHandler<T> handler)
    {
        List<T> list = new List<T>();
        for (int i = 0; i < array.Length; i++)
        {
            if (handler(array[i]))
            {
                list.Add(array[i]);
            }
        }
        return list.ToArray();  
    }

    //选择：选取数组中对象的某些成员形成一个独立的数组
    //多个学生【id age tall score】   【60,50,70,80】
    //                                              【“zs”，“ls”】
    static public TKey[] Select<T, TKey>(this T[] array, SelectHandler<T, TKey> handler)
    {
        TKey[] keys = new TKey[array.Length];
        for (int i = 0; i < array.Length; i++)
        {
            keys[i] = handler(array[i]);
        }
        return keys;   
    }

    public static Q[] Select_QTX<T,Q>(this T[] arrau,Func<T,Q> condition)
    {
        // 存储筛选出来满足条件的元素
        Q[] result = new Q[arrau.Length];
        for (int i = 0; i < arrau.Length; i++)
        {
            // 筛选的条件 [满足筛选条件，就讲改元素存在result]
            result[i] = condition(arrau[i]);
        }
        return result;
    }
}

