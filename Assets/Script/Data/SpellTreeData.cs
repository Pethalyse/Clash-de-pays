using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using AYellowpaper.SerializedCollections;
using Enum;
using Gameplay.Spells;
using NUnit.Framework;
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

        public SpellNode GetNodeFromIndexModulo5(int index)
        {
            return index switch
            {
                0 => spell5,
                1 => spell1,
                2 => spell2,
                3 => spell3,
                4 => spell4,
                _ => throw new Exception("Index SpellTree not possible")
            };
        }

        public List<Spell> GetSpellOfLevelFromSpellNode(int level, SpellNode spell)
        {
            var list = new List<Spell>();

            if (!spell || spell.level > level)
            {
                return list;
            }
            
            if(spell.level == level)
            {
                list.Add(spell.spell);
                return list;
            }

            foreach (var spellNode in spell.parent)
            {
                list.AddRange(GetSpellOfLevelFromSpellNode(level, spellNode));
            }

            return list;
        }
    }
}