using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using ARPGDemo01.Character;

namespace ARPGDemo.Skill
{
    /// <summary>
    /// 扇形 / 圆形选区
    /// </summary>
    public class SectorAttackSelector : IAttackSelector
    {
        //Transform[] IAttackSelector.SelectTarget(SkillData data, Transform skillTF)
        //{
        //    // 1.根据技能数据中的标签 获取所有目标
        //    // data.attackTargetTags
        //    // string[]     data.attackTargetTags
        //    List<Transform> targets = new List<Transform>();
        //    for (int i = 0; i < data.attackTargetTags.Length; i++)
        //    {
        //        GameObject[] tempGOArray = GameObject.FindGameObjectsWithTag(data.attackTargetTags[i]);
        //        // GameObjet[]  ---> Transform[]
        //        targets.AddRange(tempGOArray.Select(g => g.transform));
        //    }

        //    // 判断攻击范围(扇形/圆形)
        //    targets = targets.FindAll(t =>
        //        Vector3.Distance(t.position, skillTF.position) <= data.attackDistance &&
        //        Vector3.Angle(skillTF.forward, t.position - skillTF.position) <= data.attackAngle / 2
        //    ); // vector.Angle The angle returned will always be between 0 and 180 degrees,

        //    // 筛选出活的角色
        //    targets.FindAll(t => t.GetComponent<CharacterStatus>().HP > 0);

        //    //返回目标(单攻/群攻)
        //    // data.attackType
        //    Transform[] result = targets.ToArray();
        //    if (data.attackType == SkillAttackType.GroupAttack || result.Length == 0) 
        //        return result;
        //    // 距离最近(小)的敌人
        //    Transform min = result.Min(t => Vector3.Distance(t.position, skillTF.position));
        //    return new Transform[] { min };

        //}  
        public Transform[] SelectTarget(SkillData data, Transform skillTF)
        {
            // 1.根据技能数据中的标签 获取所有目标
            // data.attackTargetTags
            // string[]     data.attackTargetTags
            List<Transform> targets = new List<Transform>();
            for (int i = 0; i < data.attackTargetTags.Length; i++)
            {
                GameObject[] tempGOArray = GameObject.FindGameObjectsWithTag(data.attackTargetTags[i]);
                if (tempGOArray != null)
                // GameObjet[]  ---> Transform[]
                    targets.AddRange(tempGOArray.Select(g => g.transform));
            }

            // 判断攻击范围(扇形/圆形)
            targets = targets.FindAll(t =>
                Vector3.Distance(t.position, skillTF.position) <= data.attackDistance &&
                Vector3.Angle(skillTF.forward, t.position - skillTF.position) <= data.attackAngle / 2
            ); // vector.Angle The angle returned will always be between 0 and 180 degrees,

            // 筛选出活的角色
            targets = targets.FindAll(t => t.GetComponent<CharacterStatus>().HP > 0);

            //返回目标(单攻/群攻)
            // data.attackType
            Transform[] result = targets.ToArray();
            if (data.attackType == SkillAttackType.GroupAttack || result.Length == 0)
                return result;
            // 距离最近(小)的敌人
            Transform min = result.Min(t => Vector3.Distance(t.position, skillTF.position));
            return new Transform[] { min };
        }
    }

}
