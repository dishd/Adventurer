using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPGDemo01.Character
{
    /// <summary>
    /// animator�ֶ�
    /// </summary>
    [System.Serializable] // �������к� ����ǰ����"Ƕ��"���ű� �����ڱ�����������
    public class CharacterAnimationParameter
    {
        public string run = "run";

        public string death = "death";

        public string idle = "idle";

        public string attack01 = "attack01";

        public string attack02 = "attack02";

        public string attack03 = "attack03";

        public string attack = "attack";

        public string walk = "walk";
    }


}
