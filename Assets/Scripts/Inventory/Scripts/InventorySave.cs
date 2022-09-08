using System;
using UnityEngine;

public class InventorySave : MonoBehaviour
{
    [SerializeField] private InventoryObject _playerInventory;
    [SerializeField] private InventoryObject _playerEquipment;
    [SerializeField] private InventoryObject _playerActivePanel;
    [SerializeField] private InventoryObject _lighthouse;
    [SerializeField] private InventoryObject _oven;
    [SerializeField] private InventoryObject _houseChest;
    [SerializeField] private InventoryObject _shipChest;

    private void OnApplicationQuit()
    {
        _playerInventory.ClearInventory();
        _playerEquipment.ClearInventory();
        _playerActivePanel.ClearInventory();
        _lighthouse.ClearInventory();
        _oven.ClearInventory();
        _houseChest.ClearInventory();
        _shipChest.ClearInventory();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            _playerInventory.SaveInventory();
            _playerEquipment.SaveInventory();
            _playerActivePanel.SaveInventory();
            _lighthouse.SaveInventory();
            _oven.SaveInventory();
            _houseChest.SaveInventory();
            _shipChest.SaveInventory();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            _playerInventory.LoadInventory();
            _playerEquipment.LoadInventory();
            _playerActivePanel.LoadInventory();
            _lighthouse.LoadInventory();
            _oven.LoadInventory();
            _houseChest.LoadInventory();
            _shipChest.LoadInventory();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            _playerInventory.ClearInventory();
            _playerEquipment.ClearInventory();
            _playerActivePanel.ClearInventory();
            _lighthouse.ClearInventory();
            _oven.ClearInventory();
            _houseChest.ClearInventory();
            _shipChest.ClearInventory();
        }
    }
}
