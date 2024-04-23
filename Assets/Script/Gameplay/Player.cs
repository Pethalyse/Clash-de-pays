using System;
using Gameplay.Components;
using UnityEngine;
using UnityEngine.InputSystem;

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
        private PlayerInputs _playerInputs;

        private InputAction _moveAction;
        private InputAction _lookAction;
        private InputAction _fireAction;
        private InputAction _jumpAction;
        private InputAction _dashAction;
        
        private void Awake()
        {
            _movementComponent = GetComponent<MovementComponent>();
            _spellComponent = GetComponent<SpellComponent>();
            _lifeComponent = GetComponent<LifeComponent>();
            _playerInputs = new PlayerInputs();
        }

        private void OnEnable()
        {
            _moveAction = _playerInputs.Player.Move;
            _moveAction.Enable();
            
            _jumpAction = _playerInputs.Player.Jump;
            _jumpAction.performed += OnJump;
            _jumpAction.Enable();
            
            _lookAction = _playerInputs.Player.Look;
            _lookAction.Enable();
            
            _fireAction = _playerInputs.Player.Fire;
            _fireAction.Enable();
            
            _dashAction = _playerInputs.Player.Dash;
            _dashAction.Enable();
        }

        private void OnDisable()
        {
            _moveAction.Disable();
            _lookAction.Disable();
            _fireAction.Disable();
            _jumpAction.performed -= OnJump;
            _jumpAction.Disable();
            _dashAction.Disable();
        }

        private void Update()
        {
            // if (isHumanPlayer)
            // {
            //     if (Input.GetKey(KeyCode.UpArrow))
            //         _movementComponent.Move(Vector3.forward);
            //     if (Input.GetKey(KeyCode.DownArrow))
            //         _movementComponent.Move(Vector3.back);
            //     if (Input.GetKey(KeyCode.LeftArrow))
            //         _movementComponent.Move(Vector3.left);
            //     if (Input.GetKey(KeyCode.RightArrow))
            //         _movementComponent.Move(Vector3.right);
            //     if(Input.GetKey(KeyCode.Alpha1))
            //         _spellComponent.ChangeIndexSpellWithIndex(0);
            //     if(Input.GetKey(KeyCode.Alpha2))
            //         _spellComponent.ChangeIndexSpellWithIndex(1);
            //     if(Input.GetKey(KeyCode.Alpha3))
            //         _spellComponent.ChangeIndexSpellWithIndex(2);
            //     if(Input.GetKey(KeyCode.Alpha4))
            //         _spellComponent.ChangeIndexSpellWithIndex(3);
            //     if(Input.GetKey(KeyCode.Alpha5))
            //         _spellComponent.ChangeIndexSpellWithIndex(4);
            // }
        }

        private void FixedUpdate()
        {
            if (!isHumanPlayer) return;
            
                var moveDir = _moveAction.ReadValue<Vector2>();
                _movementComponent.Move(moveDir);
                
                var lookDir = _lookAction.ReadValue<Vector2>();
                _movementComponent.Look(lookDir);
                
                var dashDir = _dashAction.ReadValue<float>();
                if(dashDir > 0 && moveDir != Vector2.zero)
                    _movementComponent.Dash(moveDir);

                if (dashDir > 0)
                {
                    _spellComponent.ChangeIndexSpellWithIndex(4);
                }
        }
        
        private void OnJump(InputAction.CallbackContext context)
        {
            if(isHumanPlayer)
                _movementComponent.Jump();
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
