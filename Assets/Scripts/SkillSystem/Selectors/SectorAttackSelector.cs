using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using ARPGDemo01.Character;

namespace ARPGDemo.Skill
{
    /// <summary>
    /// ���� / Բ��ѡ��
    /// </summary>
    public class SectorAttackSelector : IAttackSelector
    {
        //Transform[] IAttackSelector.SelectTarget(SkillData data, Transform skillTF)
        //{
        //    // 1.���ݼ��������еı�ǩ ��ȡ����Ŀ��
        //    // data.attackTargetTags
        //    // string[]     data.attackTargetTags
        //    List<Transform> targets = new List<Transform>();
        //    for (int i = 0; i < data.attackTargetTags.Length; i++)
        //    {
        //        GameObject[] tempGOArray = GameObject.FindGameObjectsWithTag(data.attackTargetTags[i]);
        //        // GameObjet[]  ---> Transform[]
        //        targets.AddRange(tempGOArray.Select(g => g.transform));
        //    }

        //    // �жϹ�����Χ(����/Բ��)
        //    targets = targets.FindAll(t =>
        //        Vector3.Distance(t.position, skillTF.position) <= data.attackDistance &&
        //        Vector3.Angle(skillTF.forward, t.position - skillTF.position) <= data.attackAngle / 2
        //    ); // vector.Angle The angle returned will always be between 0 and 180 degrees,

        //    // ɸѡ����Ľ�ɫ
        //    targets.FindAll(t => t.GetComponent<CharacterStatus>().HP > 0);

        //    //����Ŀ��(����/Ⱥ��)
        //    // data.attackType
        //    Transform[] result = targets.ToArray();
        //    if (data.attackType == SkillAttackType.GroupAttack || result.Length == 0) 
        //        return result;
        //    // �������(С)�ĵ���
        //    Transform min = result.Min(t => Vector3.Distance(t.position, skillTF.position));
        //    return new Transform[] { min };

        //}  
        public Transform[] SelectTarget(SkillData data, Transform skillTF)
        {
            // 1.���ݼ��������еı�ǩ ��ȡ����Ŀ��
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

            // �жϹ�����Χ(����/Բ��)
            targets = targets.FindAll(t =>
                Vector3.Distance(t.position, skillTF.position) <= data.attackDistance &&
                Vector3.Angle(skillTF.forward, t.position - skillTF.position) <= data.attackAngle / 2
            ); // vector.Angle The angle returned will always be between 0 and 180 degrees,

            // ɸѡ����Ľ�ɫ
            targets = targets.FindAll(t => t.GetComponent<CharacterStatus>().HP > 0);

            //����Ŀ��(����/Ⱥ��)
            // data.attackType
            Transform[] result = targets.ToArray();
            if (data.attackType == SkillAttackType.GroupAttack || result.Length == 0)
                return result;
            // �������(С)�ĵ���
            Transform min = result.Min(t => Vector3.Distance(t.position, skillTF.position));
            return new Transform[] { min };
        }
    }

}
