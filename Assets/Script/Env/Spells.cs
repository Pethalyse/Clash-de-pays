using System;
using Gameplay;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Env
{
    public abstract class Spells : MonoBehaviour
    {
        [SerializeField] protected Image image;
        [SerializeField] protected TextMeshProUGUI text;
        
        protected void OnTimerSpellCooldownChanged(float maxTimer,float timer)
        {
            image.fillAmount = timer / maxTimer;
            text.text = $"{Math.Ceiling(timer)}";

            if (timer <= 0)
            {
                text.text = "";
            }
        }
    }
}