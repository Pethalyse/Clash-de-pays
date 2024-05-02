using System.Collections.Generic;
using Gameplay.Components;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay
{
    [RequireComponent(typeof(MovementComponent))]
    [RequireComponent(typeof(LifeComponent))]
    [RequireComponent(typeof(SpellComponent))]
    [RequireComponent(typeof(SpellTreeComponent))]
    [RequireComponent(typeof(XpComponent))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private bool isHumanPlayer;
        private MovementComponent _movementComponent;
        private LifeComponent _lifeComponent;
        private SpellComponent _spellComponent;
        private PlayerInputs _playerInputs;
        private XpComponent _xpComponent;

        private InputAction _moveAction;
        private InputAction _lookAction;
        private InputAction _fireAction;
        private InputAction _jumpAction;
        private InputAction _dashAction;
        
        private InputAction _upgradeSpell1Action;
        private InputAction _upgradeSpell2Action;
        private InputAction _upgradeSpell3Action;
        private InputAction _upgradeSpell4Action;
        private InputAction _upgradeSpell5Action;
        
        private void Awake()
        {
            _movementComponent = GetComponent<MovementComponent>();
            _spellComponent = GetComponent<SpellComponent>();
            _lifeComponent = GetComponent<LifeComponent>();
            _xpComponent = GetComponent<XpComponent>();
            _playerInputs = new PlayerInputs();
        }

        private void OnEnable()
        {
            _moveAction = _playerInputs.Player.Move;
            _moveAction.Enable();
            
            _jumpAction = _playerInputs.Player.Jump;
            _jumpAction.performed += JumpActionOnPerformed;
            _jumpAction.Enable();
            
            _lookAction = _playerInputs.Player.Look;
            _lookAction.Enable();
            
            _fireAction = _playerInputs.Player.Fire;
            _fireAction.performed += FireActionOnPerformed;
            _fireAction.Enable();
            
            _dashAction = _playerInputs.Player.Dash;
            _dashAction.Enable();

            _upgradeSpell1Action = _playerInputs.Player.UpgradeSpellChoice1;
            _upgradeSpell1Action.Enable();
            
            _upgradeSpell2Action = _playerInputs.Player.UpgradeSpellChoice2;
            _upgradeSpell2Action.Enable();
            
            _upgradeSpell3Action = _playerInputs.Player.UpgradeSpellChoice3;
            _upgradeSpell3Action.Enable();
            
            _upgradeSpell4Action = _playerInputs.Player.UpgradeSpellChoice4;
            _upgradeSpell4Action.Enable();
            
            _upgradeSpell5Action = _playerInputs.Player.UpgradeSpellChoice5;
            _upgradeSpell5Action.Enable();
        }

        private void OnDisable()
        {
            _moveAction.Disable();
            _lookAction.Disable();
            _fireAction.performed -= FireActionOnPerformed;
            _fireAction.Disable();
            _jumpAction.performed -= JumpActionOnPerformed;
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
                
                //Clique avant puis bouger marche pas trop sur les cotés AVEC LA MANETTE
                var dashDir = _dashAction.ReadValue<float>();
                if(dashDir > 0 && moveDir != Vector2.zero)
                    _movementComponent.Dash(moveDir);
        }
        
        private void JumpActionOnPerformed(InputAction.CallbackContext context)
        {
            if(isHumanPlayer)
                _movementComponent.Jump();
            
            if(isHumanPlayer)
                _xpComponent.LevelUp();
        }
        
        private void FireActionOnPerformed(InputAction.CallbackContext obj)
        {
            if(isHumanPlayer)
                _spellComponent.Cast();
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

        public List<InputAction> GetInputActionsUpgradeSpells()
        {
            return new List<InputAction> 
                { 
                    _upgradeSpell1Action, 
                    _upgradeSpell2Action,
                    _upgradeSpell3Action, 
                    _upgradeSpell4Action, 
                    _upgradeSpell5Action 
                };
        }
    }
}
