using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using UnityEngine.TextCore.Text;
using ARPGDemo01.Character;
using ns;

namespace ARPGDemo.Skill
{
    /// <summary>
    /// ���ܹ�����
    /// </summary>
    
    public class CharacterSkillManager : MonoBehaviour
    {
        // �����б�
        public SkillData[] skills;

        private void Start()
        {   
            for (int i = 0; i < skills.Length; i++)
            {
                skills[i].impactType = new string[] { "Damage", "CostSP" };
            }

            for (int i = 0; i < skills.Length; i++)
            {
                InitSkill(skills[i]);
            }
        }

        // ��ʼ������
        private void InitSkill(SkillData data)
        {
            /*
             *                            ��Դӳ���
             *      ��Դ����                                     ��Դ����·��
             * BaseMeleeAttackSkill  =   Skills/BaseMeleeAttackSkill
             */

            //data.prefabName ---> data.skillPrefab
            //data.skillPrefab = Resources.Load<GameObject>("Skill/" + data.prefabName);
            // ����������Դ���ƻ�ȡ��Դ
            //data.skillPrefab = ResourceManager.Load<GameObject>(data.prefabName);

            if (data.prefabName == "") return;
            data.skillPrefab = Resources.Load<GameObject>(data.prefabName);
            //string ss = ResourceManager.ddd("https://www.bilibili.com/");
            //ResourceManager

            data.owner = gameObject;

        }
        // ׼������(�жϼ����ͷ����� : ��ȴ ����)

        public SkillData PrepareSkill(int id)
        {
            // ���� id ���Ҽ�������
            SkillData data =  skills.Find(s  => s.skillID == id);
            //��ȡ��ǰ��ɫ����

            CharacterStatus cs = GetComponent<CharacterStatus>();
            float sp = cs.SP;
            // �ж�����  ���ؼ�������
            if (data != null && data.coolRemain <= 0 && data.costSP <= sp)
            {
                return data;
            }
            return null;

        }

        //���ɼ���
        public void GenerateSkill(SkillData data)
        {
            // ��������Ԥ�Ƽ�
            // GameObject skillGo = Instantiate(data.skillPrefab, transform.position, transform.rotation);
            GameObject skillGo = GameObjectPool.Instance.CreateObject(data.prefabName, data.skillPrefab, transform.position, transform.rotation);

            // ���ݼ�������
            SkillDeployer deployer = skillGo.GetComponent<SkillDeployer>();
            deployer.SkillData = data; // �ڲ������㷨����
            deployer.DeploySkill(); // �ڲ�ִ���㷨����

            //���ټ���
            //Destroy(skillGo, data.durationTime);
            GameObjectPool.Instance.CollectObject(skillGo, data.durationTime);

            // ����������ȴ
            StartCoroutine(CoolTimeDown(data));
        }

        // ������ȴ
        private  IEnumerator CoolTimeDown(SkillData data)
        {
            // data.coolTime ---> data.coolRemain
            data.coolRemain = data.coolTime;
            while (data.coolRemain > 0)
            {
                yield return new WaitForSeconds(1);
                data.coolRemain--;
            }
            
        }
    }

}
