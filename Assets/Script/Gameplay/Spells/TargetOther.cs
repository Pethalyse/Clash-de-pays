using System;
using UnityEditor.UIElements;
using UnityEngine;

namespace Gameplay.Spells
{
    public abstract class TargetOther : Spell
    {
        protected abstract void OnTriggerEnter(Collider other);
    }
}