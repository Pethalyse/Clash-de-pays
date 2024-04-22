namespace Gameplay
{
    public class LifeComponent
    {
        private int _life = 100;
        public int Life
        {
            get => _life;
            set
            {
                _life = value;
                OnPlayerLifeChanged?.Invoke(_life);
            }
        }

        public delegate void OnPlayerLifeChangedDelegate(int life);
        public event OnPlayerLifeChangedDelegate OnPlayerLifeChanged;
    }
}