using ARPGDemo.Skill;
using ns;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;

namespace ARPGDemo01.Character
{
    /// <summary>
    /// 获取输入以控制角色
    /// </summary>
    [RequireComponent(typeof(PlayerStatus), typeof(CharacterMotor))]    
    
    public class CharacterInputController : MonoBehaviour
    {
        private ETCJoystick joystick;
        private CharacterMotor chMotor;
        private Animator anim;
        private PlayerStatus status;
        //private ETCButton[] skillButtons;

        private UISceneWidget[] widgets;
        private CharacterSkillSystem skillSystem;

        private UnityEngine.Camera cam;
        private void Awake()
        {
            locker = new object();
            playerLocker = new object();
            joystick = FindObjectOfType<ETCJoystick>();
            chMotor = GetComponent<CharacterMotor>();
            anim = GetComponentInChildren<Animator>();
            status = GetComponent<PlayerStatus>(); 
            skillSystem = GetComponentInChildren<CharacterSkillSystem>();
            cam = UnityEngine.Camera.main;
         
        }


        public void SetSkill(UISceneWidget[] w)
        {
            widgets = w;

            for (int i = 0; i < widgets.Length; i++)
            {
                if (i < 4)
                {
                    widgets[i].MouseClick = SkillClickDown;
                }
                else
                {
                    widgets[i].MouseClick = AttackFunction;
                }
            }

        }

        private void OnEnable()
        {
            // 注册事件
            joystick.onMove.AddListener(OnJoystickMove);
            joystick.onMoveStart.AddListener(OnJoystickMoveStart);
            joystick.onMoveEnd.AddListener(OnJoystickMoveEnd);
      
        }

        public bool isDisplay;
        private object playerLocker;
        private void a2(UISceneWidget eventObj)
        {
            if (isDisplay) return;
            lock (playerLocker)
            {
                isDisplay = true;
                skillSystem.AttackUseSkill(2);
               
            }
        }

        private void SkillClickDown(UISceneWidget eventObj)
        {
            string n = eventObj.name;
            int index = int.Parse(n.Substring(n.Length - 1));
            index -= 1;

            lock (playerLocker)
            {
                isDisplay = true;

                if (SkillBindButtondData.Instance.skills[index].ID != 0)
                {
                    skillSystem.AttackUseSkill(SkillBindButtondData.Instance.skills[index].ID);
                }
            }

        }


        private float lastPressTime = 0;
        public bool isAttacking = false;
        private int comboStep = 0;

        private object locker;
        private void AttackFunction(UISceneWidget eventObj)
        {

            if (isAttacking) return;
            if (isDisplay) return;


            lock (playerLocker)
            {
                isDisplay= true;
                lock (locker)
                {
                    isAttacking = true;

                    if (lastPressTime == 0)
                        comboStep = 0;
                    else if (Time.time - lastPressTime > 2f)
                    {
                        comboStep = 0;
                    }
                    else if (Time.time - lastPressTime <= 2f)
                    {
                        comboStep = (comboStep + 1) % AttackButtonData.attackDatas.Length;
                    }

                    skillSystem.AttackCombo(comboStep);

                    lastPressTime = Time.time;
                }
            }
        }


        private void OnDisable()
        {
            // 注销事件
            joystick.onMove.RemoveListener(OnJoystickMove);
            joystick.onMoveStart.RemoveListener(OnJoystickMoveStart);
            joystick.onMoveEnd.RemoveListener(OnJoystickMoveEnd);

        }

        private void OnJoystickMove(Vector2 dir)
        {
       
            chMotor.LookAtTarget(new Vector3(cam.transform.forward.x, 0, cam.transform.forward.z));
            chMotor.Movement(new Vector3(dir.x, 0, dir.y));
        }

        private void OnJoystickMoveStart()
        {
            anim.SetBool(status.chParams.run, true);
        }

        private void OnJoystickMoveEnd()
        {
            anim.SetBool(status.chParams.run, false);
        }

    }

}
