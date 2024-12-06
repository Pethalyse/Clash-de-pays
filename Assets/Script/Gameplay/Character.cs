using Data;
using Gameplay.Components;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gameplay
{
    [RequireComponent(typeof(MovementComponent))]
    [RequireComponent(typeof(LifeComponent))]
    [RequireComponent(typeof(SpellComponent))]
    [RequireComponent(typeof(XpComponent))]
    public abstract class Character : MonoBehaviour
    {
        private MovementComponent _movementComponent;
        private LifeComponent _lifeComponent;
        private SpellComponent _spellComponent;
        private XpComponent _xpComponent;

        [SerializeField] private CharacterData data;
        
        protected void Awake()
        {
            _movementComponent = GetComponent<MovementComponent>();
            _spellComponent = GetComponent<SpellComponent>();
            _lifeComponent = GetComponent<LifeComponent>();
            _xpComponent = GetComponent<XpComponent>();
        }
        
        public LifeComponent GetLifeComponent()
        {
            return _lifeComponent;
        }
        public SpellComponent GetSpellComponent()
        {
            return _spellComponent;
        }
        public MovementComponent GetMovementComponent()
        {
            return _movementComponent;
        }

        public XpComponent GetXpComponent()
        {
            return _xpComponent;
        }
    }
}