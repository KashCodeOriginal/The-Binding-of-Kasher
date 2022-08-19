using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject _woodInterface;

    [SerializeField] private int _healthPoint;
    [SerializeField] private int _hungerPoint;
    [SerializeField] private int _waterPoint;

    [SerializeField] private PlayerStatsChanger _playerStatsChanger;

    [SerializeField] private InventoryObject _playerInventory;
    [SerializeField] private InventoryObject _playerEquipment;
    [SerializeField] private InventoryObject _playerActivePanel;

    [SerializeField] private GameObject _inventory;

    [SerializeField] private CollectWood _collectWood;

    public int HealthPoint => _healthPoint;
    public int HungerPoint => _hungerPoint;
    public int WaterPoint => _waterPoint;

    private void Start()
    {
        Application.targetFrameRate = 60;
        _inventory.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            _playerInventory.SaveInventory();
            _playerEquipment.SaveInventory();
            _playerActivePanel.SaveInventory();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            _playerInventory.LoadInventory();
            _playerEquipment.LoadInventory();
            _playerActivePanel.LoadInventory();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            _playerInventory.ClearInventory();
            _playerEquipment.ClearInventory();
            _playerActivePanel.ClearInventory();
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Tree"))
        {
            _woodInterface.SetActive(true);
            _woodInterface.GetComponent<CollectWoodDisplay>().StartCollectWoodButton(true);
            _collectWood.SetCurrentTree(collider.gameObject);
        }

        if (collider.GetComponent<GroundItem>() == true)
        {
            var item = collider.GetComponent<GroundItem>();
            if(_playerActivePanel.AddItemToInventory(item.Item.Data, item.Amount))
            {
                Destroy(collider.gameObject);
            }
            else if (_playerInventory.AddItemToInventory(item.Item.Data, item.Amount))
            {
                Destroy(collider.gameObject);
            }
        }
    }
    private void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Tree")
        {
            _woodInterface.SetActive(false);
            _woodInterface.GetComponent<CollectWoodDisplay>().StartCollectWoodButton(false);
        }
    }
    private void IncreaseHeath(int value)
    {
        _healthPoint += TryIncreaseValue(_healthPoint, value, 100);
    }
    private void DecreaseHealth(int value)
    {
        _healthPoint -= value;

        if (_healthPoint <= 0)
        {
            Die();
        }
    }
    private void IncreaseHunger(int value)
    {
        _hungerPoint += TryIncreaseValue(_hungerPoint, value, 100);
    }
    private void DecreaseHunger(int value)
    {
        _hungerPoint -= value;
    }
    private void IncreaseWater(int value)
    {
        _waterPoint += TryIncreaseValue(_waterPoint, value, 100);
    }
    private void DecreaseWater(int value)
    {
        _waterPoint -= value;
    }

    private void Die()
    {
        Debug.Log("Умер");
    }

    private void OnEnable()
    {
        _playerStatsChanger.HungerIsDecreased += DecreaseHunger;
        _playerStatsChanger.WaterIsDecreased += DecreaseWater;
        _playerStatsChanger.HealthIsDecreased += DecreaseHealth;
        _playerStatsChanger.HungerIsIncreased += IncreaseHunger;
        _playerStatsChanger.WaterIsIncreased += IncreaseWater;
        _playerStatsChanger.HealthIsIncreased += IncreaseHeath;
    }
    private void OnDisable()
    {
        _playerStatsChanger.HungerIsDecreased -= DecreaseHunger;
        _playerStatsChanger.WaterIsDecreased -= DecreaseWater;
        _playerStatsChanger.HealthIsDecreased -= DecreaseHealth;
        _playerStatsChanger.HungerIsIncreased -= IncreaseHunger;
        _playerStatsChanger.WaterIsIncreased -= IncreaseWater;
        _playerStatsChanger.HealthIsIncreased -= IncreaseHeath;
    }

    private void OnApplicationQuit()
    {
        // _playerInventory.SaveInventory();
        // _playerEquipment.SaveInventory();
        
        _playerInventory.ItemsContainer.ClearItems();
        _playerEquipment.ItemsContainer.ClearItems();
        _playerActivePanel.ItemsContainer.ClearItems();
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
}
