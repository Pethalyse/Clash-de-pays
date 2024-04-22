using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gameplay
{
    [RequireComponent(typeof(MovementComponent))]
    [RequireComponent(typeof(LifeComponent))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private bool isHumanPlayer;
        private MovementComponent _movementComponent;
        private LifeComponent _lifeComponent;
        
        private void Awake()
        {
            _movementComponent = GetComponent<MovementComponent>();
            _lifeComponent = GetComponent<LifeComponent>();
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
            }
        }

        public LifeComponent GetLifeComponent()
        {
            return _lifeComponent;
        }
    }
}
