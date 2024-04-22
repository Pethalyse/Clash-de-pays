using System;
using Data;
using UnityEngine;

namespace Gameplay.Spells
{
    public abstract class Spell : MonoBehaviour
    {
        [SerializeField] protected SpellData spellData;
        protected void DestroySpell()
        {
            Destroy(gameObject);
        }
        public float GetCooldown()
        {
            return spellData.cooldown;
        }
        public int GetManaCost()
        {
            return spellData.manaCost;
        }
    }
}