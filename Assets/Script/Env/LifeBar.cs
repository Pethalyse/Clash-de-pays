namespace Env
{
    public class LifeBar : Bar
    {
        private void OnEnable()
        {
            player.GetLifeComponent().OnLifeChanged += UpdateBarGUI;
        }

        private void OnDisable()
        {
            player.GetLifeComponent().OnLifeChanged -= UpdateBarGUI;
        }
    }
}
