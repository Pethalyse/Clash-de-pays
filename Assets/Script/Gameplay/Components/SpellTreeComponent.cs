using System;
using System.Collections.Generic;
using Data;
using Enum;
using Gameplay.Spells;
using Manager;
using UnityEngine;

namespace Gameplay.Components
{
    [RequireComponent(typeof(SpellComponent))]
    public class SpellTreeComponent : MonoBehaviour
    {
        private SpellComponent _spellComponent;
        
        private SpellTreeData _tree;

        private void Awake()
        {
            _spellComponent = GetComponent<SpellComponent>();
        }

        private void Start()
        {
            _tree = SpellTreeManager.Instance.GetTreeByElement(_spellComponent.GetElement());
        }

        public List<Spell> GetUpgradeSpellFromLevel(int level)
        {
            var spellNode = _tree.GetNodeFromIndexModulo5(level % 5);
            var listSpell = _tree.GetSpellOfLevelFromSpellNode(level, spellNode);
            return listSpell;
        }

        public SpellComponent GetSpellComponent()
        {
            return _spellComponent;
        }
        
    }
}