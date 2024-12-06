using Enum;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "CharacterData", fileName = "[CharacterData] ")]
    public class CharacterData : ScriptableObject
    {
        [Header("SpellComponent")] 
        public Elements elements;
        public int baseMana;
        public float growManaValue;
        public int regenMana;
        public int howManySecondsForRegenMana;

        [Header("LifeComponent")] 
        public int baseLife;
        public float growLifeValue;
        public int regenLife;

        [Header("XpComponent")] 
        public int level;
        public int maxLevel;
    }
}