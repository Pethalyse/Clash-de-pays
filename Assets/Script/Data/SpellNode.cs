using System.Collections.Generic;
using Gameplay.Spells;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "[SpellNode] ", menuName = "SpellNode")]
    public class SpellNode : ScriptableObject
    {
        public List<SpellNode> parent;
        public List<SpellNode> child;
        public Spell spell;

        public int level;
    }
}