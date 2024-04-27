using System;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using Enum;
using Gameplay.Spells;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "[SpellTree] ", menuName = "SpellTree")]
    public class SpellTreeData : ScriptableObject
    {
        public Elements element;

        public SpellNode spell1;
        public SpellNode spell2;
        public SpellNode spell3;
        public SpellNode spell4;
        public SpellNode spell5;

        public SpellNode GetNodeFromIndex(int index)
        {
            return index switch
            {
                0 => spell1,
                1 => spell2,
                2 => spell3,
                3 => spell4,
                4 => spell5,
                _ => throw new Exception("Index SpellTree not possible")
            };
        }
    }
}