using ns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPGDemo01.Character
{
    /// <summary>
    /// 
    /// </summary>

    public class EnemyStatus : ARPGDemo01.Character.CharacterStatus
    {
        protected override void Death()
        {
            base.Death();
            MonstartManager.Instance.MonstartDeath();
            Destroy(GetComponent<TestNPC>()._heroPanel, 10);
            Destroy(gameObject, 10);
        }
    }

}
