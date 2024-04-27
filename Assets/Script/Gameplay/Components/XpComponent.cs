using UnityEngine;

namespace Gameplay.Components
{
    public class XpComponent : MonoBehaviour
    {
        private int _level = 1;

        public int GetLevel()
        {
            return _level;
        }

        private void LevelUp()
        {
            _level++;
        }
    }
}