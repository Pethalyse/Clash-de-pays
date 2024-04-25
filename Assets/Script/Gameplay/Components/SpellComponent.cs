using System;
using System.Collections.Generic;
using Gameplay.Spells;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

namespace Gameplay.Components
{
    public class SpellComponent : MonoBehaviour
    {
        private int _manaMax = 100;
        private int _manaActual;
        private int _regenMana = 2;
        private float _timeManaRegen;
        private const int HowManySecondsForRegenMana = 1;
        public delegate void OnManaChangedDelegate(int maxMana,int mana);
        public event OnManaChangedDelegate OnManaChanged;
        private int ManaActual
        {
            get => _manaActual;
            set
            {
                _manaActual = value;
                OnManaChanged?.Invoke(_manaMax,_manaActual);
            }
        }
        
        [SerializeField] private Transform spellSpawn;

        private int _currentIndexSpell;
        [SerializeField] private Spell[] spells = new Spell[5];
        private Dictionary<Spell, float> _cooldownSpells;

        private void Start()
        {
            ManaActual = _manaMax;
            _cooldownSpells = new Dictionary<Spell, float>();
            foreach (var spell in spells)
            {
                if(spell != null) 
                    _cooldownSpells.Add(spell, 0f);
            }
        }

        private void Update()
        {
            RegenMana();
            UpdateCooldownSpells();
        }

        private void RegenMana()
        {
            if (ManaActual >= _manaMax) return;
                _timeManaRegen += Time.deltaTime;
            
            if (!(_timeManaRegen >= HowManySecondsForRegenMana)) return;
                AddMana(_regenMana);
                _timeManaRegen -= HowManySecondsForRegenMana;
        }
        
        private void UpdateCooldownSpells()
        {
            var clone = new Dictionary<Spell, float>(_cooldownSpells);
            foreach (var (spell, cd) in clone)
            {
                if (!(cd > 0)) continue;

                var cooldown = GetCooldownOfSpell(spell);
                
                cooldown -= Time.deltaTime;
                if (cooldown < 0f)
                    cooldown = 0f;
                
                SetInCooldown(spell, cooldown);

            }
        }

        public void ChangeIndexSpellWithIndex(int index)
        {
            _manaActual -= 10;
            if (index is >= 0 and <= 4)
            {
                _currentIndexSpell = index;
            }
        }
        public void ChangeIndexSpellScroll(bool upOrDown)
        {
            if (upOrDown)
                ChangeIndexSpellWithIndex(_currentIndexSpell + 1);
            else
                ChangeIndexSpellWithIndex(_currentIndexSpell - 1);
        }
        public void Cast()
        {
            var spell = GetSpellFromIndex(_currentIndexSpell);
            if(spell == null) return;
                if (GetCooldownOfSpell(spell) != 0f) return;
                if (ManaActual < spell.GetManaCost()) return;
                    Instantiate(spell, spellSpawn.position, spellSpawn.rotation);
                    SetInCooldown(spell, spell.GetCooldown());
                    RemoveMana(spell.GetManaCost());
        }
        private void SetInCooldown(Spell spell, float cooldown)
        {
            _cooldownSpells[spell] = cooldown;
        }

        public void RemoveMana(int manaMinus)
        {
            ManaActual -= manaMinus;

            if (ManaActual < 0)
                ManaActual = 0;
        }

        private void AddMana(int manaPlus)
        {
            ManaActual += manaPlus;

            if (ManaActual > _manaMax)
                ManaActual = _manaMax;
        }
        
        [CanBeNull]
        public Spell GetSpellFromIndex(int index)
        {
            if (index is >= 0 and <= 4)
            {
                return spells[index];
            }

            throw new Exception("Spell index not possible !");

        }

        public float GetCooldownOfSpell(Spell spell)
        {
            if (_cooldownSpells.TryGetValue(spell, out var ofSpell))
                return ofSpell;
            
            throw new Exception("Spell not contain !");
        }
    }
}