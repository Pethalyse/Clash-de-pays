using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay.Components
{
    [RequireComponent(typeof(Rigidbody))]
    public class MovementComponent : MonoBehaviour
    {
        private Vector3 _defaultPosition;
        private Rigidbody _body;
    
        [SerializeField] private float speed;
        [SerializeField] private float dashForce;
        [SerializeField] private float jumpForce;
        private bool _isGrounded;
        private bool _canDash;
        private const float DashCooldown = 3f;
        private float _timerDashCooldown;
        public delegate void OnTimerDashCooldownDelegate(float maxTimer,float timer);
        public event OnTimerDashCooldownDelegate OnTimerDashCooldownChanged;

        private float TimerDashCooldown
        {
            get => _timerDashCooldown;
            set
            {
                _timerDashCooldown = value;
                OnTimerDashCooldownChanged?.Invoke(DashCooldown,_timerDashCooldown);
                
            }
        }

        private LayerMask _groundLayerMask;
        [SerializeField] private Transform groundCheck;

        private void Awake()
        {
            _defaultPosition = transform.position;
            _body = GetComponent<Rigidbody>();
            _body.collisionDetectionMode = CollisionDetectionMode.Continuous;

            _groundLayerMask = LayerMask.GetMask("Ground");
        }

        private void Update()
        {
            if(TimerDashCooldown > 0 && !_canDash)
                TimerDashCooldown -= Time.deltaTime;

            if (TimerDashCooldown <= 0)
                ChangeCanDash(true);

        }

        private void FixedUpdate()
        {
            _isGrounded = Physics.Raycast(groundCheck.position, Vector3.down, 0.07f, _groundLayerMask);
        }

        public void Move(Vector2 direction)
        {
            AddForceToBody(direction, speed);
        }
        public void Dash(Vector2 direction)
        {
            if (!_canDash) return;
                AddForceToBody(direction, dashForce);
                ChangeCanDash(false);
                TimerDashCooldown = DashCooldown;
        }

        private void ChangeCanDash(bool dash)
        {
            _canDash = dash;
        }

        private void AddForceToBody(Vector2 direction, float force)
        {
            var velocity = _body.velocity;
            velocity.x = force * direction.x;
            velocity.z = force * direction.y;
            _body.velocity = velocity;
        }
        
        public void Look(Vector2 direction)
        {
            
        }

        public void Jump()
        {
            if(_isGrounded)
                _body.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        public void ResetPosition()
        {
            transform.position = _defaultPosition;
        }
    }
}