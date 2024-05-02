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
        
        [SerializeField] private Elements element;

        private SpellTreeData _tree;

        private void Awake()
        {
            _spellComponent = GetComponent<SpellComponent>();
        }

        private void Start()
        {
            _tree = SpellTreeManager.Instance.GetTreeByElement(element);
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