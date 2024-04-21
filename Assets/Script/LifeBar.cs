using System;
using UnityEngine;
using UnityEngine.UI;

public class LifeBar : MonoBehaviour
{
    [SerializeField] private Slider lifeSlider;
    [SerializeField] private Player player;

    private void OnEnable()
    {
        player.OnPlayerLifeChanged += UpdateLifeGUI;
    }

    private void OnDisable()
    {
        player.OnPlayerLifeChanged -= UpdateLifeGUI;
    }

    private void UpdateLifeGUI(int life)
    {
        lifeSlider.value = life / 100f;
    }
}
