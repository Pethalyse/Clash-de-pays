using System;
using Gameplay.Components;
using Gameplay.Spells;
using Manager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Env
{
    public class SpellBar : Spells
    {
        private SpellComponent _playerSpellComponent;
        [SerializeField] private Image spellImg;
        [SerializeField] private Image spellSelected;
        [SerializeField] private Image spellUpgrade;
        [SerializeField] private Sprite defaultImg;
        [SerializeField] private TextMeshProUGUI costText;
        [SerializeField] private int index;
        private void Start()
        {
            _playerSpellComponent = GameManager.Instance.GetMainPlayer().GetSpellComponent();
        }

        private void Update()
        {
            UpdateCooldownFromIndex(index);
            ChangeSpellImage(index);
            spellSelected.enabled = _playerSpellComponent.GetSpellIsSelected(index);
            spellUpgrade.enabled = _playerSpellComponent.GetSpellIsUpgrade(index);
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
            costText.text = spell ? $"{spell.GetManaCost()}" : $"";
        }
    }
}