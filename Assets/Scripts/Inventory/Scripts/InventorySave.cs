using UnityEngine;

public class InventorySave : MonoBehaviour
{
    [SerializeField] private InventoryObject _playerInventory;
    [SerializeField] private InventoryObject _playerEquipment;
    [SerializeField] private InventoryObject _playerActivePanel;
    [SerializeField] private InventoryObject _lighthouse;
    [SerializeField] private InventoryObject _oven;
    
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
    private void OnApplicationQuit()
    {
        // _playerInventory.SaveInventory();
        // _playerEquipment.SaveInventory();
        
        _playerInventory.ItemsContainer.ClearItems();
        _playerEquipment.ItemsContainer.ClearItems();
        _playerActivePanel.ItemsContainer.ClearItems();
        _lighthouse.ItemsContainer.ClearItems();
        _oven.ItemsContainer.ClearItems();
    }
}
