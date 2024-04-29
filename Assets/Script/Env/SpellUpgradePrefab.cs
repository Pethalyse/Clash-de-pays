using System;
using Gameplay.Spells;
using UnityEngine;
using UnityEngine.UI;

namespace Env
{
    public class SpellUpgradePrefab : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private Button button;

        private Spell _spell;
        private bool _selected;
        public bool Selected
        {
            get => _selected;
            set
            {
                _selected = value;
                if(_selected)
                    OnSelectedChange?.Invoke(_spell);
            }
        }
        public delegate void OnSelectedChangedDelegate(Spell spell);
        public event OnSelectedChangedDelegate OnSelectedChange;

        public void InitObject(Spell spell)
        {
            _spell = spell;

            image.sprite = spell.GetSpellSprite();
            button.onClick.AddListener(TaskOnClick);
        }

        private void TaskOnClick()
        {
            Selected = true;
        }
    }
}