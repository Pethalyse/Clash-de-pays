using System;
using Gameplay;
using UnityEngine;
using UnityEngine.UI;

namespace Env
{
    public class LifeBar : MonoBehaviour
    {
        
        [SerializeField] private Slider lifeSlider;
        [SerializeField] private Player player;

        private void OnEnable()
        {
            player.GetLifeComponent().OnPlayerLifeChanged += UpdateLifeGUI;
        }

        private void OnDisable()
        {
            player.GetLifeComponent().OnPlayerLifeChanged -= UpdateLifeGUI;
        }

        private void UpdateLifeGUI(int life)
        {
            lifeSlider.value = life / 100f;
        }
    }
}
