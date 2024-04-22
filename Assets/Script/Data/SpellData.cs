using Enum;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "[Spell] ", menuName = "Spell")]
    public class SpellData : ScriptableObject
    {
        public string nom;
        public string description;

        public int value;
        public int manaCost;
        public float cooldown;
        public float duration;
        public float speed;
        public float radius;
        public Elements element;
    }
}