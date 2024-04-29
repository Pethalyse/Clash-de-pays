using System;
using UnityEngine;

namespace Gameplay.Components
{
    [RequireComponent(typeof(SpellTreeComponent))]
    public class XpComponent : MonoBehaviour
    {
        private SpellTreeComponent _spellTreeComponent;
        private int _level;
        
        public int Level
        {
            get => _level;
            private set
            {
                _level = value;
                OnLevelChangedSpellTreeUpgrade?.Invoke(_level, _spellTreeComponent);
            }
        }
        
        public delegate void OnLevelChangedSpellTreeUpgradeDelegate(int level, SpellTreeComponent spellTreeComponent);
        public event OnLevelChangedSpellTreeUpgradeDelegate OnLevelChangedSpellTreeUpgrade;

        private void Awake()
        {
            _spellTreeComponent = GetComponent<SpellTreeComponent>();
        }

        private void Start()
        {
            LevelUp();
        }

        public int GetLevel()
        {
            return Level;
        }

        public void LevelUp()
        {
            Level++;
        }
    }
}