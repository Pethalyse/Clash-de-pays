using System;
using UnityEngine;

namespace Gameplay.Components
{
    public class LifeComponent : MonoBehaviour
    {
        private int _lifeMax = 100;
        private const float GrowValue = 11f;
        private int _life;
        private int Life
        {
            get => _life;
            set
            {
                _life = value;
                OnLifeChanged?.Invoke(_lifeMax,_life);
            }
        }

        public delegate void OnLifeChangedDelegate(int lifeMax,int life);
        public event OnLifeChangedDelegate OnLifeChanged;

        private void Start()
        {
            Life = _lifeMax;
        }

        public void TakeDamage(int damage)
        {
            Life -= damage;
            if (Life <= 0)
                Die();
        }
        public void Heal(int heal)
        {
            Life += heal;
            if (Life >= _lifeMax)
                Life = _lifeMax;
        }

        private void Die()
        {
            Destroy(gameObject);
        }

        public void LevelUp()
        {
            var add = Mathf.RoundToInt(GrowValue * _lifeMax / 100);
            _lifeMax += add;
            Life += add;
        }
    }
}