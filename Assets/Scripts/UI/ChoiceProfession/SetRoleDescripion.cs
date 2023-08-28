using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ns
{
    /// <summary>
    /// 在 ChoiceProfession 面板中设置 角色的相关描述
    /// </summary>
    public class SetRoleDescripion : MonoSingleton<SetRoleDescripion>
    {
        private UILabel roleNameLable;
        private UILabel weapNameLable;
        private UILabel proNameLable;
        private UILabel descriptionLable;

        public override void Init()
        {
            base.Init();
            roleNameLable = transform.FindChildCompentByName<UILabel>("RoleNameLabel");
            weapNameLable = transform.FindChildCompentByName<UILabel>("WeaponNameLabel");
            proNameLable = transform.FindChildCompentByName<UILabel>("CharacteristicNameLabel");
            descriptionLable = transform.FindChildCompentByName<UILabel>("DescriptionLable");

            roleNameLable.text = CharacterTemplate.Instance.characterName;
        }
        public void SetDescription(string weapenName, string proName, string anoter)
        {
            weapNameLable.text = weapenName;
            proNameLable.text = proName;
            descriptionLable.text = anoter;
        }
    }

}
