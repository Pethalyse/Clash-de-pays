using Gameplay;
using UnityEngine;
using UnityEngine.UI;

namespace Env
{
    public class ManaBar : MonoBehaviour
    {
        [SerializeField] private Slider manaSlider;
        [SerializeField] private Player player;

        private void OnEnable()
        {
            player.GetSpellComponent().OnManaChanged += UpdateManaGUI;
        }

        private void OnDisable()
        {
            player.GetSpellComponent().OnManaChanged -= UpdateManaGUI;
        }

        private void UpdateManaGUI(int mana)
        {
            manaSlider.value = mana / 100f;
        }
    }
}