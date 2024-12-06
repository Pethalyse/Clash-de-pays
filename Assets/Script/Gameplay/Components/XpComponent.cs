using UnityEngine;

namespace Gameplay.Components
{
    [RequireComponent(typeof(SpellTreeComponent))]
    [RequireComponent(typeof(LifeComponent))]
    public class XpComponent : MonoBehaviour
    {
        private SpellTreeComponent _spellTreeComponent;
        private LifeComponent _lifeComponent;
        private int _level;
        private int _maxLevel = 25;
        private int Level
        {
            get => _level;
            set
            {
                _level = value;
                OnLevelChangedSpellTreeUpgrade?.Invoke(_level, _spellTreeComponent);
                OnLevelChanged?.Invoke(_level);
            }
        }
        private int _xp;
        private int _xpBeforeLevelUp = 100;
        private int Xp
        {
            get => _xp;
            set
            {
                _xp = value;
                OnXpChangedText?.Invoke(_xpBeforeLevelUp,_xp);
            }
        }
        
        public delegate void OnLevelChangedSpellTreeUpgradeDelegate(int level, SpellTreeComponent spellTreeComponent);
        public delegate void OnLevelChangedDelegate(int level);
        public delegate void OnXpChangedTextDelegate(int xp, int xpBeforeLevelUp);
        public event OnLevelChangedSpellTreeUpgradeDelegate OnLevelChangedSpellTreeUpgrade;
        public event OnLevelChangedDelegate OnLevelChanged;
        public event OnXpChangedTextDelegate OnXpChangedText;

        private void Awake()
        {
            _spellTreeComponent = GetComponent<SpellTreeComponent>();
            _lifeComponent = GetComponent<LifeComponent>();
        }

        private void Start()
        {
            LevelUp();
        }

        public void LevelUp()
        {
            if (Level >= _maxLevel) return;
            Level++;
            ChangeXpBeforeLevelUp();
            if(Level == _maxLevel) Xp = _xpBeforeLevelUp;
            _lifeComponent.LevelUp();
            _spellTreeComponent.GetSpellComponent().LevelUp();
        }

        public void AddXp(int value)
        {
            Xp += value;
            if (Xp < _xpBeforeLevelUp) return;
            MinusXpBeforeLevelUp();
            LevelUp();
        }

        private void MinusXpBeforeLevelUp()
        {
            Xp -= _xpBeforeLevelUp;
        }

        private void ChangeXpBeforeLevelUp()
        {
            _xpBeforeLevelUp = 100 * Level;
        }
    }
}