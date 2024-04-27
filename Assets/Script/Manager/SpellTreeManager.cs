using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using Enum;
using Gameplay.Components;
using UnityEngine;

namespace Manager
{
    public class SpellTreeManager : MonoBehaviour
    {
        public static SpellTreeManager Instance;
        [SerializeField] private List<SpellTreeData> tree;

        private void Awake()
        {
            Instance = this;
        }

        public SpellTreeData GetTreeByElement(Elements element)
        {
            foreach (var t in tree.Where(t => t.element == element))
            {
                return t;
            }

            throw new Exception("This element don't have a tree");
        }
    }
}