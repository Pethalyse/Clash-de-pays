using System.Collections.Generic;
using Gameplay.Spells;
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
            foreach (var (spell, cd) in _cooldownSpells)
            {
                if (!(cd > 0)) continue;

                var cooldown = _cooldownSpells[spell];
                
                cooldown -= Time.deltaTime;
                if (cooldown < 0f)
                    cooldown = 0f;
                
                _cooldownSpells[spell] = cooldown;

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
        private void Cast()
        {
            var spell = spells[_currentIndexSpell];
            if (_cooldownSpells[spell] != 0f) return;
            
                Instantiate(spell, spellSpawn);
                SetInCooldown(spell);
                RemoveMana(spell.GetManaCost());
        }
        private void SetInCooldown(Spell spell)
        {
            _cooldownSpells[spell] = spell.GetCooldown();
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
    }
}