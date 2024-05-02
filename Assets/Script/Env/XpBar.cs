using TMPro;
using UnityEngine;

namespace Env
{
    public class XpBar : Bar
    {
        [SerializeField] private TextMeshProUGUI textLevel;
        private void OnEnable()
        {
            player.GetXpComponent().OnLevelChangedText += OnLevelChangedText ;
            player.GetXpComponent().OnXpChangedText += UpdateBarGUI ;
        }
        
        private void OnDisable()
        {
            player.GetXpComponent().OnLevelChangedText -= OnLevelChangedText;
            player.GetXpComponent().OnXpChangedText -= UpdateBarGUI ;
        }
        
        private void OnLevelChangedText(int level)
        {
            textLevel.text = $"{level}";
        }
    }
}