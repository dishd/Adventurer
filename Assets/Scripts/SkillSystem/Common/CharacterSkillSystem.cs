using ARPGDemo.Skill;
using ARPGDemo01.Character;
using ns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace ARPGDemo.Skill
{
    [RequireComponent(typeof(CharacterSkillManager), typeof(AnimationEventBehaviour))]
    /// <summary>
    /// 封装技能系统，提供简单的释放功能。
    /// </summary>
    public class CharacterSkillSystem : MonoBehaviour
    {
        private CharacterSkillManager skillManager;
        private Animator anim;
        private void Start()
        {
            skillManager = GetComponent<CharacterSkillManager>();
            anim = GetComponentInChildren<Animator>();
            GetComponentInChildren<AnimationEventBehaviour>().attackHandler += DeploySkill;
        }

        private void DeploySkill()
        {
            //生成技能
            skillManager.GenerateSkill(skill);
        }
        // 一种解决方法 动作队列
        private SkillData skill;
        /// <summary>
        /// 使用技能攻击(为玩家提供)
        /// </summary>
        /// 
        public void AttackUseSkill(int skillID, bool isBatter = false)
        {
            // 如果连击，则从上一个释放的技能中获取连击技能编号
            if (skill != null && isBatter)
                skillID = skill.nextBatterId;
            //准备技能
            skill = skillManager.PrepareSkill(skillID);
            if (skill == null) return;

            //播放动画
            // 播放动画圆形 ...普攻... 生成技能圆形 生成技能普攻 播放动画普攻 生成技能普攻
            anim.SetBool(skill.animationName, true);

            // 如果是单攻
            // skill.attackType == skillAttackType.Single
            if (skill.attackType != SkillAttackType.SingleAttack) return;
            // -- 查找目标
            Transform targetTF = SelectTarget();
            //... 朝向目标
            Vector3 v3 = new Vector3(targetTF.position.x, 0, targetTF.position.z);
            //transform.LookAt(targetTF);
            transform.LookAt(v3);
            //... 选择目标
            // 1. 选择目标 : 间隔指定时间受取消选中
            // 2. 选中A目标，在自动取消前，又选中B,则需要手动将A取消
            //          (核心思想: 存储上次选中的物体)
            // 先取消上次选中的物体
            //SetSelectedActiveFx(false);

            selectedTarget = targetTF;
            // 选中当前物体
           
            //SetSelectedActiveFx(true);
        }


        public void AttackCombo(int step)
        {
                
            int skillID = AttackButtonData.attackDatas[step];
            //print(skillID + "   skill   " + step );
            
            skill = skillManager.PrepareSkill(skillID);
            //if (skill == null) return;

            //播放动画
            // 播放动画圆形 ...普攻... 生成技能圆形 生成技能普攻 播放动画普攻 生成技能普攻
            anim.SetTrigger(skill.animationName);
            anim.SetInteger("ComboStep", step);
            // 如果是单攻
            // skill.attackType == skillAttackType.Single
            //if (skill.attackType != SkillAttackType.SingleAttack) return;
            // -- 查找目标
            Transform targetTF = SelectTarget();
            //... 朝向目标
            if (targetTF != null)
            {
                Vector3 v3 = new Vector3(targetTF.position.x, transform.position.y, targetTF.position.z);
                //transform.LookAt(targetTF);
                transform.LookAt(v3);
            }
           
            //... 选择目标
            // 1. 选择目标 : 间隔指定时间受取消选中
            // 2. 选中A目标，在自动取消前，又选中B,则需要手动将A取消
            //          (核心思想: 存储上次选中的物体)
            // 先取消上次选中的物体
            SetSelectedActiveFx(false);

            selectedTarget = targetTF;
            // 选中当前物体

            SetSelectedActiveFx(true);
        }

        [HideInInspector]
        // 选中的目标
        public Transform selectedTarget;

        private void SetSelectedActiveFx(bool state)
        {
            if (selectedTarget == null) return;
            var selected = selectedTarget.GetComponent<CharacterSelected>();
            if (selected) selected.SetSelecteActive(state); // 组件被禁用，只有一些脚本生命周期的函数(例如Update、Start等)才会受影响，而自己写的函数不受影响


        }

        private Transform SelectTarget()
        {
            Transform[] target = new SectorAttackSelector().SelectTarget(skill, transform);
            return target.Length != 0? target[0] : null;
        }

        /// <summary>
        /// 使用随机技能 (为npc提供)
        /// </summary>
        public void UseRandomSkill()
        {
            // 需求: 从 管理器中 挑选出随机的技能
            // 先筛选出所有可以释放的技能，再产生随机数。
            var usableSkils = skillManager.skills.FindAll( s => skillManager.PrepareSkill(s.skillID) != null);
            if (usableSkils.Length == 0) return;
            int index = Random.Range(0, usableSkils.Length);
            AttackUseSkill(usableSkils[index].skillID);
        }
    }

}
