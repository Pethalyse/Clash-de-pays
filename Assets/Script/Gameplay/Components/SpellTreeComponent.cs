using System;
using Data;
using Enum;
using Manager;
using UnityEngine;

namespace Gameplay.Components
{
    [RequireComponent(typeof(XpComponent))]
    [RequireComponent(typeof(SpellComponent))]
    public class SpellTreeComponent : MonoBehaviour
    {
        private XpComponent _xpComponent;
        private SpellComponent _spellComponent;
        
        [SerializeField] private Elements element;

        private SpellTreeData _tree;

        private void Awake()
        {
            _spellComponent = GetComponent<SpellComponent>();
            _xpComponent = GetComponent<XpComponent>();
        }

        private void Start()
        {
            _tree = SpellTreeManager.Instance.GetTreeByElement(element);
            // _spellComponent.AddSpell(0 ,_tree.spell1.spell);
        }
        
    }
}