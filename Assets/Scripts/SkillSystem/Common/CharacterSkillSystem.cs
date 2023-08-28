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
    /// ��װ����ϵͳ���ṩ�򵥵��ͷŹ��ܡ�
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
            //���ɼ���
            skillManager.GenerateSkill(skill);
        }
        // һ�ֽ������ ��������
        private SkillData skill;
        /// <summary>
        /// ʹ�ü��ܹ���(Ϊ����ṩ)
        /// </summary>
        /// 
        public void AttackUseSkill(int skillID, bool isBatter = false)
        {
            // ��������������һ���ͷŵļ����л�ȡ�������ܱ��
            if (skill != null && isBatter)
                skillID = skill.nextBatterId;
            //׼������
            skill = skillManager.PrepareSkill(skillID);
            if (skill == null) return;

            //���Ŷ���
            // ���Ŷ���Բ�� ...�չ�... ���ɼ���Բ�� ���ɼ����չ� ���Ŷ����չ� ���ɼ����չ�
            anim.SetBool(skill.animationName, true);

            // ����ǵ���
            // skill.attackType == skillAttackType.Single
            if (skill.attackType != SkillAttackType.SingleAttack) return;
            // -- ����Ŀ��
            Transform targetTF = SelectTarget();
            //... ����Ŀ��
            Vector3 v3 = new Vector3(targetTF.position.x, 0, targetTF.position.z);
            //transform.LookAt(targetTF);
            transform.LookAt(v3);
            //... ѡ��Ŀ��
            // 1. ѡ��Ŀ�� : ���ָ��ʱ����ȡ��ѡ��
            // 2. ѡ��AĿ�꣬���Զ�ȡ��ǰ����ѡ��B,����Ҫ�ֶ���Aȡ��
            //          (����˼��: �洢�ϴ�ѡ�е�����)
            // ��ȡ���ϴ�ѡ�е�����
            //SetSelectedActiveFx(false);

            selectedTarget = targetTF;
            // ѡ�е�ǰ����
           
            //SetSelectedActiveFx(true);
        }


        public void AttackCombo(int step)
        {
                
            int skillID = AttackButtonData.attackDatas[step];
            //print(skillID + "   skill   " + step );
            
            skill = skillManager.PrepareSkill(skillID);
            //if (skill == null) return;

            //���Ŷ���
            // ���Ŷ���Բ�� ...�չ�... ���ɼ���Բ�� ���ɼ����չ� ���Ŷ����չ� ���ɼ����չ�
            anim.SetTrigger(skill.animationName);
            anim.SetInteger("ComboStep", step);
            // ����ǵ���
            // skill.attackType == skillAttackType.Single
            //if (skill.attackType != SkillAttackType.SingleAttack) return;
            // -- ����Ŀ��
            Transform targetTF = SelectTarget();
            //... ����Ŀ��
            if (targetTF != null)
            {
                Vector3 v3 = new Vector3(targetTF.position.x, transform.position.y, targetTF.position.z);
                //transform.LookAt(targetTF);
                transform.LookAt(v3);
            }
           
            //... ѡ��Ŀ��
            // 1. ѡ��Ŀ�� : ���ָ��ʱ����ȡ��ѡ��
            // 2. ѡ��AĿ�꣬���Զ�ȡ��ǰ����ѡ��B,����Ҫ�ֶ���Aȡ��
            //          (����˼��: �洢�ϴ�ѡ�е�����)
            // ��ȡ���ϴ�ѡ�е�����
            SetSelectedActiveFx(false);

            selectedTarget = targetTF;
            // ѡ�е�ǰ����

            SetSelectedActiveFx(true);
        }

        [HideInInspector]
        // ѡ�е�Ŀ��
        public Transform selectedTarget;

        private void SetSelectedActiveFx(bool state)
        {
            if (selectedTarget == null) return;
            var selected = selectedTarget.GetComponent<CharacterSelected>();
            if (selected) selected.SetSelecteActive(state); // ��������ã�ֻ��һЩ�ű��������ڵĺ���(����Update��Start��)�Ż���Ӱ�죬���Լ�д�ĺ�������Ӱ��


        }

        private Transform SelectTarget()
        {
            Transform[] target = new SectorAttackSelector().SelectTarget(skill, transform);
            return target.Length != 0? target[0] : null;
        }

        /// <summary>
        /// ʹ��������� (Ϊnpc�ṩ)
        /// </summary>
        public void UseRandomSkill()
        {
            // ����: �� �������� ��ѡ������ļ���
            // ��ɸѡ�����п����ͷŵļ��ܣ��ٲ����������
            var usableSkils = skillManager.skills.FindAll( s => skillManager.PrepareSkill(s.skillID) != null);
            if (usableSkils.Length == 0) return;
            int index = Random.Range(0, usableSkils.Length);
            AttackUseSkill(usableSkils[index].skillID);
        }
    }

}
