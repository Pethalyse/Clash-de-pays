using System;
using System.Collections.Generic;
using Gameplay;
using Gameplay.Components;
using Gameplay.Spells;
using Manager;
using Unity.VisualScripting;
using UnityEngine;

namespace Env
{
    public class SpellUpgrade : MonoBehaviour
    {

        [SerializeField] private SpellUpgradePrefab childToCreate;
        private Player _player;
        
        private List<SpellUpgradePrefab> _allButtonForLevel;
        private void Start()
        {
            _player = GameManager.Instance.GetMainPlayer();
            _player.GetXpComponent().OnLevelChangedSpellTreeUpgrade += CreateImagesUpgradeSpell;
            _allButtonForLevel = new List<SpellUpgradePrefab>();
        }

        private void CreateImagesUpgradeSpell(int level, SpellTreeComponent spellTreeComponent)
        {
            var spellList = spellTreeComponent.GetUpgradeSpellFromLevel(level);
            foreach (var spell in spellList)
            {
                var create = Instantiate(childToCreate, transform);
                _allButtonForLevel.Add(create);
                create.InitObject(spell);
                create.OnSelectedChange += AddSpellToPlayerOnClickedButton;
            }
        }

        private void AddSpellToPlayerOnClickedButton(Spell spell)
        {
            var index = (_player.GetXpComponent().Level % 5) switch
            {
                1 => 0,
                2 => 1,
                3 => 2,
                4 => 3,
                0 => 4,
                _ => throw new ArgumentOutOfRangeException()
            };

            _player.GetSpellComponent().AddSpell(index, spell);
            DestroyAllButton();
        }

        private void DestroyAllButton()
        {
            foreach (var button in _allButtonForLevel)
            {
                Destroy(button.gameObject);
            }
            
            _allButtonForLevel.Clear();
        }
    }
}