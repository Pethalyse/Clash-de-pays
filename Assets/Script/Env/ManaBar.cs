using Gameplay;
using UnityEngine;
using UnityEngine.UI;

namespace Env
{
    [RequireComponent(typeof(Slider))]
    public class ManaBar : MonoBehaviour
    {
        private Slider _manaSlider;
        [SerializeField] private Player player;

        private void Awake()
        {
            _manaSlider = GetComponent<Slider>();
        }
        
        private void OnEnable()
        {
            //player.GetSpellComponent().OnManaChanged += UpdateManaGUI;
        }

        private void OnDisable()
        {
            //layer.GetSpellComponent().OnManaChanged -= UpdateManaGUI;
        }

        private void UpdateManaGUI(int mana)
        {
            _manaSlider.value = mana / 100f;
        }
    }
}