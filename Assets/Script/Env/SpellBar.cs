using System;
using Gameplay.Components;
using Gameplay.Spells;
using Manager;
using UnityEngine;
using UnityEngine.UI;

namespace Env
{
    public class SpellBar : Spells
    {
        private SpellComponent _playerSpellComponent;
        [SerializeField] private Image spellImg;
        [SerializeField] private Sprite defaultImg;
        [SerializeField] private int index;
        private void Start()
        {
            _playerSpellComponent = GameManager.Instance.GetMainPlayer().GetSpellComponent();
        }

        private void Update()
        {
            UpdateCooldownFromIndex(index);
            ChangeSpellImage(index);
        }

        private void UpdateCooldownFromIndex(int i)
        {
            var spell = _playerSpellComponent.GetSpellFromIndex(i);
            if (!spell) return;
            var cd = _playerSpellComponent.GetCooldownOfIndex(i);
            OnTimerSpellCooldownChanged(spell.GetCooldown(), cd);
        }

        private void ChangeSpellImage(int i)
        {
            var spell = _playerSpellComponent.GetSpellFromIndex(i);
            spellImg.sprite = spell ? spell.GetSpellSprite() : defaultImg;
        }
    }
}