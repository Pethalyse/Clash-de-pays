namespace Env
{
    public class ManaBar : Bar
    {
        private void OnEnable()
        {
            player.GetSpellComponent().OnManaChanged += UpdateBarGUI;
        }

        private void OnDisable()
        {
            player.GetSpellComponent().OnManaChanged -= UpdateBarGUI;
        }
    }
}