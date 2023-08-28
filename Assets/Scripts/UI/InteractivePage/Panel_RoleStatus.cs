using ARPGDemo01.Character;
using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ns
{
    /// <summary>
    /// 
    /// </summary>
    public class Panel_RoleStatus : UIScene
    {

        private UISlider mpSlider;
        private UISlider hpSlider;

        private UILabel hpLabel;
        private UILabel mpLabel;
        private UILabel nameLabel;

        private PlayerStatus cs;
        protected override void Start()
        {
            base.Start();
            cs = FindObjectOfType<PlayerStatus>();

            mpSlider = transform.FindChildCompentByName<UISlider>("MP");
            hpSlider = transform.FindChildCompentByName<UISlider>("HP");
            nameLabel = transform.FindChildCompentByName<UILabel>("PlayerName");

            hpLabel = hpSlider.GetComponentInChildren<UILabel>();
            mpLabel = mpSlider.GetComponentInChildren<UILabel>();

            nameLabel.text = cs.playerName;

            SetHPAndMP(cs.HP.ToString(), cs.SP.ToString());
        }

        public void setHP(string hp)
        {
            hpLabel.text = hp;
            hpSlider.value = (float)(cs.HP / cs.maxHP);
        }

        public void SetMP(string mp)
        {
            mpLabel.text = mp;
            mpSlider.value = (float)(cs.SP / cs.maxSP);
        }

        public void SetHPAndMP(string hp, string mp)
        {
            setHP(hp);
            SetMP(mp);
        }
    
    }

}
