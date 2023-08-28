using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ns
{
    /// <summary>
    /// 
    /// </summary>
    /// 

    public class Skill
    {
        public int ID;
        public string iconName;
    }

    public class SkillBindButtondData : MonoSingleton<SkillBindButtondData>
    {
        public Skill[] skills = new Skill[4];

        public override void Init()
        {
            base.Init();

            for (int i = 0; i < 4; i++)
            {
                Skill s = new Skill()
                {
                    ID = 0,
                    iconName = null
                };

                skills[i] = s;
            }
        }

        public void SetNull(int index)
        {
            skills[index].ID = 0;
            skills[index].iconName = null;
        }
    }

}
