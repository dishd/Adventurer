using ARPGDemo01.Character;
using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace ns
{
    /// <summary>
    /// 
    /// </summary>
    public class Panel_CharacterInfo : UIScene
    {
        private UILabel NameLabel; // ����
        private UILabel LvLabel; //�ȼ�
        private UILabel HPLabel; // Ѫ��
        private UILabel MPLabel; // ����

        private UILabel ExpLabel; // ����
        private UILabel Force; // �﹥
        private UILabel hitRateLabel; //������
        private UILabel Power;  // ����
        private UILabel PhysicalDEF; // ���
        private UILabel Dodge; // ����
        private UILabel Intelligence; // ����
        private UILabel Gongfa; //����
        private UILabel CriticalChance; // ����
        private UILabel AttackSpeed; // �����ٶ�
        private UILabel MDEF; // ����
        private UILabel CCDEF; // ����


        private Transform tf; //ģ�͵ļ��صط�
        private PlayerStatus status; // ���״̬


        private new void Start()
        {

            GetWidget("Close").MouseClick = func;
            NameLabel = transform.FindChildCompentByName<UILabel>("NameLabel");
            LvLabel = transform.FindChildCompentByName<UILabel>("LvLabel");
            HPLabel = transform.FindChildCompentByName<UILabel>("HPLabel");
            MPLabel = transform.FindChildCompentByName<UILabel>("MPLabel");


            ExpLabel = transform.FindChildCompentByName<UILabel>("ExpLabel");
            Force = transform.FindChildCompentByName<UILabel>("Force");
            hitRateLabel = transform.FindChildCompentByName<UILabel>("HitRateLabel");
            Power = transform.FindChildCompentByName<UILabel>("Power");
            PhysicalDEF = transform.FindChildCompentByName<UILabel>("PhysicalDEF");
            Dodge = transform.FindChildCompentByName<UILabel>("Dodge");
            Intelligence = transform.FindChildCompentByName<UILabel>("Intelligence");
            Gongfa = transform.FindChildCompentByName<UILabel>("Gongfa");
            CriticalChance = transform.FindChildCompentByName<UILabel>("CriticalChance");
            AttackSpeed = transform.FindChildCompentByName<UILabel>("AttackSpeed");
            MDEF = transform.FindChildCompentByName<UILabel>("MDEF");
            CCDEF = transform.FindChildCompentByName<UILabel>("CCDEF");

            Init();
            
            tf = transform.FindChildByName("LoadMode");

            status = GameObject.FindWithTag("Player").GetComponent<PlayerStatus>();

            LoadModeFunc();
        }

        private void OnEnable()
        {
            SetInfoData(status);
        }

        private void func(UISceneWidget eventObj)
        {
            SetVisible(false);
        }

        private void Init()
        {
            hitRateLabel.text = "100";
            Power.text = "10";
            PhysicalDEF.text = "30";
            Dodge.text = "0";
            Gongfa.text = "���";
            CriticalChance.text = "0";
            MDEF.text = "0";
            CCDEF.text = "0";
            
        }

        public void SetInfoData(PlayerStatus ps)
        {
            if (ps == null) { return; }
            NameLabel.text = ps.playerName;
            LvLabel.text = ps.lv.ToString();
            HPLabel.text = ps.HP.ToString();
            MPLabel.text = ps.SP.ToString();
            ExpLabel.text = ps.exp.ToString();
            Force.text = ps.baseATK.ToString();
            Intelligence.text = ps.intellect.ToString();
            AttackSpeed.text = ps.atkSpeed.ToString();
        }

        

        private void LoadModeFunc()
        {
            //UnityEngine.Object go = Resources.Load(CharacterTemplate.Instance.jobModel);
            UnityEngine.Object go = Resources.Load("Character/Warrior");
            GameObject g = Instantiate(go,tf, false) as GameObject;
            //g.layer = LayerMask.NameToLayer("UI");
            g.transform.localPosition = Vector3.zero;
            g.transform.ChildrenDelegate((tf) => { tf.gameObject.layer = LayerMask.NameToLayer("UI"); });
            SpinWithMouse s = g.AddComponent<SpinWithMouse>();
            s.target = g.transform;

           
            
        }
    }

}
