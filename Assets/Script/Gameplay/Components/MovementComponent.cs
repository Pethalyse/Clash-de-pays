using UnityEngine;

namespace Gameplay.Components
{
    public class MovementComponent : MonoBehaviour
    {
        private Vector3 _defaultPosition;
    
        [SerializeField] private float speed;
    
        private void Awake()
        {
            _defaultPosition = transform.position;
        }

        public void Move(Vector3 direction)
        {
            transform.position += direction * (speed * Time.deltaTime);
        }

        public void ResetPosition()
        {
            transform.position = _defaultPosition;
        }
    }
}