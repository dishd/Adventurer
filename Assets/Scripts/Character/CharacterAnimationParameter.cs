using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPGDemo01.Character
{
    /// <summary>
    /// animator字段
    /// </summary>
    [System.Serializable] // 可以序列号 讲当前对象"嵌入"到脚本 可以在编译器中属性
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
