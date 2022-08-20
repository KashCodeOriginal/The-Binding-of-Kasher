using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsDisplay : MonoBehaviour
{
    [SerializeField] private Slider _healthBar;
    [SerializeField] private Slider _waterBar;
    [SerializeField] private Slider _hungerBar;

    [SerializeField] private Player _player;

    private void ChangeHealthBarValue(int value)
    {
        _healthBar.value = value;
    }
    private void ChangeWaterBarValue(int value)
    {
        _waterBar.value = value;
    }
    private void ChangeHungerBarValue(int value)
    {
        _hungerBar.value = value;
    }

    private void OnEnable()
    {
        _player.HealthValueChanged += ChangeHealthBarValue;
        _player.HungerValueChanged += ChangeHungerBarValue;
        _player.WaterValueChanged += ChangeWaterBarValue;
    }
    private void OnDisable()
    {
        _player.HealthValueChanged -= ChangeHealthBarValue;
        _player.HungerValueChanged -= ChangeHungerBarValue;
        _player.WaterValueChanged -= ChangeWaterBarValue;
    }
}
