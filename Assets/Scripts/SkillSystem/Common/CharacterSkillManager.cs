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
    /// 技能管理器
    /// </summary>
    
    public class CharacterSkillManager : MonoBehaviour
    {
        // 技能列表
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

        // 初始化技能
        private void InitSkill(SkillData data)
        {
            /*
             *                            资源映射表
             *      资源名称                                     资源完整路径
             * BaseMeleeAttackSkill  =   Skills/BaseMeleeAttackSkill
             */

            //data.prefabName ---> data.skillPrefab
            //data.skillPrefab = Resources.Load<GameObject>("Skill/" + data.prefabName);
            // 仅仅根据资源名称获取资源
            //data.skillPrefab = ResourceManager.Load<GameObject>(data.prefabName);

            if (data.prefabName == "") return;
            data.skillPrefab = Resources.Load<GameObject>(data.prefabName);
            //string ss = ResourceManager.ddd("https://www.bilibili.com/");
            //ResourceManager

            data.owner = gameObject;

        }
        // 准备技能(判断技能释放条件 : 冷却 法力)

        public SkillData PrepareSkill(int id)
        {
            // 根据 id 查找技能数据
            SkillData data =  skills.Find(s  => s.skillID == id);
            //获取当前角色法力

            CharacterStatus cs = GetComponent<CharacterStatus>();
            float sp = cs.SP;
            // 判断条件  返回技能数据
            if (data != null && data.coolRemain <= 0 && data.costSP <= sp)
            {
                return data;
            }
            return null;

        }

        //生成技能
        public void GenerateSkill(SkillData data)
        {
            // 创建技能预制件
            // GameObject skillGo = Instantiate(data.skillPrefab, transform.position, transform.rotation);
            GameObject skillGo = GameObjectPool.Instance.CreateObject(data.prefabName, data.skillPrefab, transform.position, transform.rotation);

            // 传递技能数据
            SkillDeployer deployer = skillGo.GetComponent<SkillDeployer>();
            deployer.SkillData = data; // 内部创建算法对象
            deployer.DeploySkill(); // 内部执行算法对象

            //销毁技能
            //Destroy(skillGo, data.durationTime);
            GameObjectPool.Instance.CollectObject(skillGo, data.durationTime);

            // 开启技能冷却
            StartCoroutine(CoolTimeDown(data));
        }

        // 技能冷却
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
