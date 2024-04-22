using Gameplay.Components;
using Unity.VisualScripting;
using UnityEngine;

namespace Gameplay
{
    [RequireComponent(typeof(MovementComponent))]
    [RequireComponent(typeof(LifeComponent))]
    [RequireComponent(typeof(SpellComponent))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private bool isHumanPlayer;
        private MovementComponent _movementComponent;
        private LifeComponent _lifeComponent;
        private SpellComponent _spellComponent;
        
        private void Awake()
        {
            _movementComponent = GetComponent<MovementComponent>();
            _lifeComponent = GetComponent<LifeComponent>();
            _spellComponent = GetComponent<SpellComponent>();
        }

        private void Update()
        {
            if (isHumanPlayer)
            {
                if (Input.GetKey(KeyCode.UpArrow))
                    _movementComponent.Move(Vector3.forward);
                if (Input.GetKey(KeyCode.DownArrow))
                    _movementComponent.Move(Vector3.back);
                if (Input.GetKey(KeyCode.LeftArrow))
                    _movementComponent.Move(Vector3.left);
                if (Input.GetKey(KeyCode.RightArrow))
                    _movementComponent.Move(Vector3.right);
                if(Input.GetKey(KeyCode.Alpha1))
                    _spellComponent.ChangeIndexSpellWithIndex(0);
                if(Input.GetKey(KeyCode.Alpha2))
                    _spellComponent.ChangeIndexSpellWithIndex(1);
                if(Input.GetKey(KeyCode.Alpha3))
                    _spellComponent.ChangeIndexSpellWithIndex(2);
                if(Input.GetKey(KeyCode.Alpha4))
                    _spellComponent.ChangeIndexSpellWithIndex(3);
                if(Input.GetKey(KeyCode.Alpha5))
                    _spellComponent.ChangeIndexSpellWithIndex(4);
            }
        }

        public LifeComponent GetLifeComponent()
        {
            return _lifeComponent;
        }
        
        public SpellComponent GetSpellComponent()
        {
            return _spellComponent;
        }
    }
}
