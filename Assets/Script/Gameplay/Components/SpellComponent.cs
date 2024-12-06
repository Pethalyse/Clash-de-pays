using System;
using System.Collections.Generic;
using Enum;
using Gameplay.Spells;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

namespace Gameplay.Components
{
    public class SpellComponent : MonoBehaviour
    {
        
        [SerializeField] private Elements element;
        
        private int _manaMax = 100;
        private const float GrowValue = 7.4f;
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
        private readonly Spell[] _spells = new Spell[5];
        private readonly bool[] _spellsSelected = new bool[5];
        private readonly bool[] _spellsIsUpgrade = new bool[5];
        private Dictionary<int, float> _cooldownSpells;

        private void Start()
        {
            ManaActual = _manaMax;
            _cooldownSpells = new Dictionary<int, float>();
            for (var i = 0; i <= 4; i++)
            {
                _cooldownSpells.Add(i, 0f);
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
            var clone = new Dictionary<int, float>(_cooldownSpells);
            foreach (var (index, cd) in clone)
            {
                if (!(cd > 0)) continue;

                var cooldown = GetCooldownOfIndex(index);
                
                cooldown -= Time.deltaTime;
                if (cooldown < 0f)
                    cooldown = 0f;
                
                SetInCooldown(index, cooldown);

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
            var index = _currentIndexSpell;
            var spell = GetSpellFromIndex(index);
            if(spell == null) return;
                if (GetCooldownOfIndex(index) != 0f) return;
                if (ManaActual < spell.GetManaCost()) return;
                    Instantiate(spell, spellSpawn.position, spellSpawn.rotation);
                    SetInCooldown(index, spell.GetCooldown());
                    RemoveMana(spell.GetManaCost());
        }
        private void SetInCooldown(int index, float cooldown)
        {
            _cooldownSpells[index] = cooldown;
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
                return _spells[index];
            }

            throw new Exception("Spell index not possible !");

        }

        public float GetCooldownOfIndex(int index)
        {
            if (_cooldownSpells.TryGetValue(index, out var ofSpell))
                return ofSpell;
            
            throw new Exception("Spell not contain !");
        }

        public void AddSpell(int index, Spell spell)
        {
            _spells[index] = spell;
        }

        public bool GetSpellIsSelected(int i)
        {
            return _spellsSelected[i];
        }
        
        public bool GetSpellIsUpgrade(int i)
        {
            return _spellsIsUpgrade[i];
        }

        public void ChangeSpellUpgrade(int i)
        {
            if (i > 4)
                throw new Exception("Index spell upgrade not possible");
            for (var l = 0; l < _spellsIsUpgrade.Length; l++)
            {
                _spellsIsUpgrade[l] = false;
            }
            if(i >= 0)
                _spellsIsUpgrade[i] = true;
        }
        
        public void ChangeSpellSelected(int i)
        {
            if (i is < 0 or > 4)
                throw new Exception("Index spell selected not possible");
            for (var l = 0; l < _spellsSelected.Length; l++)
            {
                _spellsSelected[l] = false;
            }
            _spellsSelected[i] = true;
        }
        
        public void LevelUp()
        {
            var add = Mathf.RoundToInt(GrowValue * _manaMax / 100);
            _manaMax += add;
            ManaActual += add;
        }

        public Elements GetElement()
        {
            return element;
        }
    }
}