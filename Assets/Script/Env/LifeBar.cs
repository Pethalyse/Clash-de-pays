using System;
using Gameplay;
using UnityEngine;
using UnityEngine.UI;

namespace Env
{
    [RequireComponent(typeof(Slider))]
    public class LifeBar : MonoBehaviour
    {
        private Slider _lifeSlider;
        [SerializeField] private Player player;

        private void Awake()
        {
            _lifeSlider = GetComponent<Slider>();
        }

        private void OnEnable()
        {
            player.GetLifeComponent().OnLifeChanged += UpdateLifeGUI;
        }

        private void OnDisable()
        {
            player.GetLifeComponent().OnLifeChanged -= UpdateLifeGUI;
        }

        private void UpdateLifeGUI(int life)
        {
            _lifeSlider.value = life / 100f;
        }
    }
}
