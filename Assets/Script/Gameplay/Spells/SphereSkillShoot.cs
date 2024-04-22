using Gameplay.Components;
using UnityEngine;

namespace Gameplay.Spells
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(SphereCollider))]
    public class SphereSkillShoot : TargetOther
    {
        private Rigidbody _body;
        private SphereCollider _collider;
        
        private void Awake()
        {
            _body = GetComponent<Rigidbody>();
            _body.isKinematic = true;
            
            _collider = GetComponent<SphereCollider>();
            _collider.radius = spellData.radius;
            _collider.isTrigger = true;
            
            Invoke(nameof(DestroySpell), spellData.duration);
        }

        private void Update()
        {
            if(spellData.speed > 0)
                _body.AddForce(Vector3.forward * spellData.speed);
        }

        protected override void OnCollisionEnter(Collision other)
        {
            var lifeComponent = other.gameObject.GetComponent<LifeComponent>();
            lifeComponent?.TakeDamage(spellData.value);
            DestroySpell();
        }
    }
}