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
        private Dictionary<int, SpellTreeComponent> _waitingCreation;
        private void OnEnable()
        {
            _player = GameManager.Instance.GetMainPlayer();
            _player.GetXpComponent().OnLevelChangedSpellTreeUpgrade += CreateImagesUpgradeSpell;
            _allButtonForLevel = new List<SpellUpgradePrefab>();
            _waitingCreation = new Dictionary<int, SpellTreeComponent>();
        }

        private void OnDisable()
        {
            _player.GetXpComponent().OnLevelChangedSpellTreeUpgrade -= CreateImagesUpgradeSpell;
        }

        private void CreateImagesUpgradeSpell(int level, SpellTreeComponent spellTreeComponent)
        {
            if (_allButtonForLevel.Count == 0)
            {
                var spellList = spellTreeComponent.GetUpgradeSpellFromLevel(level);
                foreach (var spell in spellList)
                {
                    var create = Instantiate(childToCreate, transform);
                    _allButtonForLevel.Add(create);
                    create.InitObject(level, spell);
                    create.OnSelectedChange += AddSpellToPlayerOnClickedButton;
                }
                _player.GetSpellComponent().ChangeSpellUpgrade(GetIndexSpell(level));
                BindInputsActions();
            }
            else
            {
                var spellList = spellTreeComponent.GetUpgradeSpellFromLevel(level);
                if(spellList.Count != 0)
                    _waitingCreation.Add(level, spellTreeComponent);
            }
        }

        private void AddSpellToPlayerOnClickedButton(int level ,Spell spell)
        {
            var index = GetIndexSpell(level);

            _player.GetSpellComponent().AddSpell(index, spell);
            _player.GetSpellComponent().ChangeSpellUpgrade(-1);
            DestroyAllButton();
        }

        private static int GetIndexSpell(int level)
        {
            var index = (level % 5) switch
            {
                1 => 0,
                2 => 1,
                3 => 2,
                4 => 3,
                0 => 4,
                _ => throw new ArgumentOutOfRangeException()
            };
            return index;
        }

        private void DestroyAllButton()
        {
            UnBindInputsActions();
            foreach (var button in _allButtonForLevel)
            {
                Destroy(button.gameObject);
            }
            
            _allButtonForLevel.Clear();
            
            foreach (var (i, value) in _waitingCreation)
            {
                CreateImagesUpgradeSpell(i, value);
                _waitingCreation.Remove(i);
                break;
            }
        }

        private void BindInputsActions()
        {
            var listActions = _player.GetInputActionsUpgradeSpells();
            for(var i = 0; i<_allButtonForLevel.Count; i++)
            {
                var action = listActions[i];
                if(action == null)
                    throw new Exception($"Les actions n'existent pas à partir de cet index d'amélioration : {i}");
                
                action.performed +=
                    _allButtonForLevel[i].GetComponent<SpellUpgradePrefab>().TaskOnClickPerformed;
            }
        }
        
        private void UnBindInputsActions()
        {
            var listActions = _player.GetInputActionsUpgradeSpells();
            for(var i = 0; i<_allButtonForLevel.Count; i++)
            {
                var action = listActions[i];
                if(action == null)
                    throw new Exception($"Les actions n'existent pas à partir de cet index d'amélioration : {i}");
                
                action.performed -=
                    _allButtonForLevel[i].GetComponent<SpellUpgradePrefab>().TaskOnClickPerformed;
            }
        }
    }
}