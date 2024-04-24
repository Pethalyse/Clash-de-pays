using System;
using Manager;
using UnityEngine;

namespace Env
{
    public class SpellDash : Spells
    {
        private void Start()
        {
            GameManager.Instance.GetMainPlayer().GetMovementComponent().OnTimerDashCooldownChanged += OnTimerSpellCooldownChanged;
        }
        
        // private void OnDisable()
        // {
        //     GameManager.Instance.GetMainPlayer().GetMovementComponent().OnTimerDashCooldownChanged -= OnTimerSpellCooldownChanged;
        // }
    }
}