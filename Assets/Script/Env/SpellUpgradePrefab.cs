using System;
using Gameplay.Spells;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Env
{
    public class SpellUpgradePrefab : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private Button button;

        private Spell _spell;
        private bool _selected;
        private int _level;

        public bool Selected
        {
            get => _selected;
            set
            {
                _selected = value;
                if(_selected)
                    OnSelectedChange?.Invoke(_level, _spell);
            }
        }
        public delegate void OnSelectedChangedDelegate(int level,Spell spell);
        public event OnSelectedChangedDelegate OnSelectedChange;

        public void InitObject(int level, Spell spell)
        {
            _level = level;
            _spell = spell;

            image.sprite = spell.GetSpellSprite();
            button.onClick.AddListener(TaskOnClick);
        }

        private void TaskOnClick()
        {
            Selected = true;
        }

        public void TaskOnClickPerformed(InputAction.CallbackContext obj)
        {
            TaskOnClick();
        }
    }
}