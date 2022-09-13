using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour, IDamagable
{
    [SerializeField] private int _maxPointsValue;

    [SerializeField] private int _healthPoint;
    [SerializeField] private int _hungerPoint;
    [SerializeField] private int _waterPoint;
    [SerializeField] private int _energyPoint;

    [SerializeField] private PlayerStatsChanger _playerStatsChanger;
    
    [SerializeField] private GameObject _inventory;

    [SerializeField] private Vector3 _spawnPlace;

    [SerializeField] private PlayerSleep _playerSleep;

    public event UnityAction<int> HealthValueChanged;
    public event UnityAction<int> WaterValueChanged;
    public event UnityAction<int> HungerValueChanged;
    public event UnityAction<int> EnergyValueChanged;
    public event UnityAction PlayerDied;

    public int HealthPoint => _healthPoint;
    public int HungerPoint => _hungerPoint;
    public int WaterPoint => _waterPoint;
    public int EnergyPoint => _energyPoint;

    private void Start()
    {
        Application.targetFrameRate = 75;
        _inventory.SetActive(false);
        SpawnPlayer();

        LoadPlayerInfo();
        
        HealthValueChanged?.Invoke(_healthPoint);
        HungerValueChanged?.Invoke(_hungerPoint);
        WaterValueChanged?.Invoke(_waterPoint);
        EnergyValueChanged?.Invoke(_energyPoint);

        if (_healthPoint <= 0)
        {
            Die();
        }
    }
    private void IncreaseHeath(int value)
    {
        _healthPoint += TryIncreaseValue(_healthPoint, value, _maxPointsValue);
        HealthValueChanged?.Invoke(_healthPoint);
    }
    private void DecreaseHealth(int value)
    {
        _healthPoint -= value;
        
        HealthValueChanged?.Invoke(_healthPoint);

        if (_healthPoint <= 0)
        {
            Die();
        }
    }
    private void IncreaseHunger(int value)
    {
        _hungerPoint += TryIncreaseValue(_hungerPoint, value, _maxPointsValue);
        HungerValueChanged?.Invoke(_hungerPoint);
    }
    private void DecreaseHunger(int value)
    {
        _hungerPoint -= value;
        HungerValueChanged?.Invoke(_hungerPoint);
    }
    private void IncreaseWater(int value)
    {
        _waterPoint += TryIncreaseValue(_waterPoint, value, _maxPointsValue);
        WaterValueChanged?.Invoke(_waterPoint);
    }
    private void DecreaseWater(int value)
    {
        _waterPoint -= value;
        WaterValueChanged?.Invoke(_waterPoint);
    }

    private void IncreaseEnergy(int value)
    {
        _energyPoint += TryIncreaseValue(_energyPoint, value, _maxPointsValue);
        EnergyValueChanged?.Invoke(_energyPoint);
    }
    private void DecreaseEnergy(int value)
    {
        _energyPoint -= value;
        EnergyValueChanged?.Invoke(_energyPoint);
    }

    public void SpawnPlayer()
    {
        gameObject.GetComponent<CapsuleCollider>().enabled = true;
        gameObject.GetComponent<Rigidbody>().useGravity = true;
        gameObject.transform.position = _spawnPlace;

        _healthPoint = _maxPointsValue;
        _waterPoint = _maxPointsValue;
        _hungerPoint = _maxPointsValue;
        _energyPoint = _maxPointsValue;
        
        HungerValueChanged?.Invoke(_hungerPoint);
        WaterValueChanged?.Invoke(_waterPoint);
        HealthValueChanged?.Invoke(_healthPoint);
        EnergyValueChanged?.Invoke(_energyPoint);
    }

    private void Die()
    {
        PlayerDied?.Invoke();
    }

    private void OnEnable()
    {
        _playerStatsChanger.HungerIsDecreased += DecreaseHunger;
        _playerStatsChanger.WaterIsDecreased += DecreaseWater;
        _playerStatsChanger.HealthIsDecreased += DecreaseHealth;
        _playerStatsChanger.EnergyIsDecreased += DecreaseEnergy;
        _playerStatsChanger.HungerIsIncreased += IncreaseHunger;
        _playerStatsChanger.WaterIsIncreased += IncreaseWater;
        _playerStatsChanger.HealthIsIncreased += IncreaseHeath;
        _playerStatsChanger.EnergyIsIncreased += IncreaseEnergy;
        _playerSleep.PlayerStartsSleep += SetEnergyToFull;
    }
    private void OnDisable()
    {
        _playerStatsChanger.HungerIsDecreased -= DecreaseHunger;
        _playerStatsChanger.WaterIsDecreased -= DecreaseWater;
        _playerStatsChanger.HealthIsDecreased -= DecreaseHealth;
        _playerStatsChanger.EnergyIsDecreased -= DecreaseEnergy;
        _playerStatsChanger.HungerIsIncreased -= IncreaseHunger;
        _playerStatsChanger.WaterIsIncreased -= IncreaseWater;
        _playerStatsChanger.HealthIsIncreased -= IncreaseHeath;
        _playerStatsChanger.EnergyIsIncreased -= IncreaseEnergy;
        _playerSleep.PlayerStartsSleep += SetEnergyToFull;
    }

    private int TryIncreaseValue(int currentAddingValue, int valueToAdd, int maxValue)
    {
        int placeableValue = valueToAdd;
        
        if (placeableValue + currentAddingValue > maxValue)
        {
            placeableValue = maxValue - currentAddingValue;
            return placeableValue;
        }
        
        return placeableValue;
    }

    private void SetEnergyToFull()
    {
        _energyPoint = _maxPointsValue;
    }

    public void TryApplyDamage(int value)
    {
        DecreaseHealth(value);
    }

#if UNITY_ANDROID && !UNITY_EDITOR
    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            SavePlayerInfo();
        } 
}
#endif
    private void OnApplicationQuit()
    {
        SavePlayerInfo();
    }

    private void SavePlayerInfo()
    {
        PlayerPrefs.SetInt("Health", _healthPoint);
        PlayerPrefs.SetInt("Hunger", _hungerPoint);
        PlayerPrefs.SetInt("Water", _waterPoint);
        PlayerPrefs.SetInt("Energy", _energyPoint);

        var position = gameObject.transform.position;

        PlayerPrefs.SetFloat("xPosition", position.x);
        PlayerPrefs.SetFloat("yPosition", position.y);
        PlayerPrefs.SetFloat("zPosition", position.z);
    }
    private void LoadPlayerInfo()
    {
        _healthPoint = PlayerPrefs.GetInt("Health", _maxPointsValue);
        _hungerPoint = PlayerPrefs.GetInt("Hunger", _maxPointsValue);
        _waterPoint = PlayerPrefs.GetInt("Water", _maxPointsValue);
        _energyPoint = PlayerPrefs.GetInt("Energy", _maxPointsValue);

        var position = new Vector3(PlayerPrefs.GetFloat("xPosition", _spawnPlace.x), PlayerPrefs.GetFloat("yPosition", _spawnPlace.y), PlayerPrefs.GetFloat("zPosition", _spawnPlace.z));

        gameObject.transform.position = position;
    }
}
