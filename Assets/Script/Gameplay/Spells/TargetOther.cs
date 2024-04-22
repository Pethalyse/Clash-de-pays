using System;
using UnityEngine;

namespace Gameplay.Spells
{
    public abstract class TargetOther : Spell
    {
        protected abstract void OnCollisionEnter(Collision other);
    }
}