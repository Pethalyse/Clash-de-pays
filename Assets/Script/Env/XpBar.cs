using TMPro;
using UnityEngine;

namespace Env
{
    public class XpBar : Bar
    {
        [SerializeField] private TextMeshProUGUI textLevel;
        private void OnEnable()
        {
            player.GetXpComponent().OnLevelChanged += OnLevelChanged ;
            player.GetXpComponent().OnXpChangedText += UpdateBarGUI ;
        }
        
        private void OnDisable()
        {
            player.GetXpComponent().OnLevelChanged -= OnLevelChanged;
            player.GetXpComponent().OnXpChangedText -= UpdateBarGUI ;
        }
        
        private void OnLevelChanged(int level)
        {
            textLevel.text = $"{level}";
        }
    }
}