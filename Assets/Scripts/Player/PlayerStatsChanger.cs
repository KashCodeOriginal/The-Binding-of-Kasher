using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStatsChanger : MonoBehaviour
{
    [SerializeField] private Player _player;

    [SerializeField] private int _healthDecreaseStep;
    [SerializeField] private int _healthIncreaseStep;
    [SerializeField] private float _timeBetweenHealthIncreaseSteps;
    [SerializeField] private float _timeBetweenHealthDecreaseSteps;

    [SerializeField] private int _hungerDecreaseStep;
    [SerializeField] private float _timeBetweenHungerSteps;

    [SerializeField] private int _waterDecreaseStep;
    [SerializeField] private float _timeBetweenWaterSteps;

    [SerializeField] private int _energyDecreaseStep;
    [SerializeField] private float _timeBetweenEnergySteps;

    [SerializeField] private PlayerDeath _playerDeath;

    [SerializeField] private PlayerMovement _playerMovement;

    private Dictionary<(int, int), int> _energyForSpeed = new Dictionary<(int, int), int>()
    {
        {(100, 80), 10},
        {(79, 70), 9},
        {(69, 60), 8},
        {(59, 50), 7},
        {(49, 40), 6},
        {(39, 30), 5},
        {(29, 20), 4},
    };
    

    public event UnityAction<int> HealthIsIncreased;
    public event UnityAction<int> HealthIsDecreased;
    
    public event UnityAction<int> HungerIsIncreased;
    public event UnityAction<int> HungerIsDecreased;

    public event UnityAction<int> WaterIsIncreased;
    public event UnityAction<int> WaterIsDecreased;

    public event UnityAction<int> EnergyIsIncreased;
    public event UnityAction<int> EnergyIsDecreased;

    private void Start()
    {
        StartCoroutines();
    }

    private IEnumerator DecreaseHunger()
    {
        while(_player.HungerPoint > 0)
        {
            HungerIsDecreased?.Invoke(_hungerDecreaseStep);
            yield return new WaitForSeconds(_timeBetweenHungerSteps);
        }
    }
    private IEnumerator DecreaseWater()
    {
        while(_player.WaterPoint > 0)
        {
            WaterIsDecreased?.Invoke(_waterDecreaseStep);
            yield return new WaitForSeconds(_timeBetweenWaterSteps);
        }
    }
    private IEnumerator DecreaseEnergy()
    {
        while(_player.EnergyPoint > 0)
        {
            EnergyIsDecreased?.Invoke(_energyDecreaseStep);
            yield return new WaitForSeconds(_timeBetweenEnergySteps);

            foreach (var energy in _energyForSpeed)
            {
                if (_player.EnergyPoint <= energy.Key.Item1 && _player.EnergyPoint > energy.Key.Item2)
                {
                    _playerMovement.DecreaseSpeed(energy.Value); 
                }
            }
        }
    }

    private IEnumerator CheckStats()
    {
        while (_player.HealthPoint > 0)
        {
            while ((_player.WaterPoint <= 0 || _player.HungerPoint <= 0) && _player.HealthPoint > 0)
            {
                HealthIsDecreased?.Invoke(_healthDecreaseStep);
                yield return new WaitForSeconds(_timeBetweenHealthDecreaseSteps);
            }
            
            while (_player.WaterPoint >= 70 && _player.HungerPoint >= 70 && _player.HealthPoint < 100)
            {
                HealthIsIncreased?.Invoke(_healthIncreaseStep);
                yield return new WaitForSeconds(_timeBetweenHealthIncreaseSteps);
            }
            yield return new WaitForSeconds(3f);
        }
    }

    public void IncreaseHunger(int value)
    {
        HungerIsIncreased?.Invoke(value);
        IncreaseEnergy(value * 2);
    }
    public void IncreaseWater(int value)
    {
        WaterIsIncreased?.Invoke(value);
    }
    public void IncreaseHealth(int value)
    {
        HealthIsIncreased?.Invoke(value);
    }

    public void IncreaseEnergy(int value)
    {
        EnergyIsIncreased?.Invoke(value);
        
        foreach (var energy in _energyForSpeed)
        {
            if (_player.EnergyPoint <= energy.Key.Item1 && _player.EnergyPoint > energy.Key.Item2)
            {
                _playerMovement.IncreaseSpeed(energy.Value); 
            }
        }
    }

    public void DecreaseEnergyByAction(int value)
    {
        EnergyIsDecreased?.Invoke(value);
    }

    private void StartCoroutines()
    {
        StopCoroutines();
        
        StartCoroutine(DecreaseHunger());
        StartCoroutine(DecreaseWater());
        StartCoroutine(DecreaseEnergy());
        StartCoroutine(CheckStats());
    }

    private void OnEnable()
    {
        _playerDeath.PlayerIsRespawned += StartCoroutines;
    }
    private void OnDisable()
    {
        _playerDeath.PlayerIsRespawned -= StartCoroutines;
    }

    private void StopCoroutines()
    {
        StopCoroutine(DecreaseHunger());
        StopCoroutine(DecreaseWater());
        StopCoroutine(DecreaseEnergy());
        StopCoroutine(CheckStats());
    }
}
