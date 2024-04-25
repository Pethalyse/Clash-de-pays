using System;
using Gameplay.Components;
using Manager;
using UnityEngine;

namespace Env
{
    public class SpellBar : Spells
    {
        private SpellComponent _playerSpellComponent;
        [SerializeField] private int index;
        private void Start()
        {
            _playerSpellComponent = GameManager.Instance.GetMainPlayer().GetSpellComponent();
        }

        private void Update()
        {
            UpdateCooldownFromIndex(index);
        }

        private void UpdateCooldownFromIndex(int i)
        {
            var spell = _playerSpellComponent.GetSpellFromIndex(i);
            if (!spell) return;
            var cd = _playerSpellComponent.GetCooldownOfSpell(spell);
            OnTimerSpellCooldownChanged(spell.GetCooldown(), cd);
        }
    }
}