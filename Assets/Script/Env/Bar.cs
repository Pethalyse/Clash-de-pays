using Gameplay;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Env
{
    [RequireComponent(typeof(Slider))]
    public abstract class Bar : MonoBehaviour
    {
        private Slider _slider;
        [SerializeField] private TextMeshProUGUI textBar;
        [SerializeField] protected Player player;
        
        private void Awake()
        {
            _slider = GetComponent<Slider>();
        }
        
        protected void UpdateBarGUI(int max,int value)
        {
            _slider.maxValue = max;
            _slider.value = value;
            if(textBar)
                textBar.text = $"{value}/{max}";
        }
    }
}