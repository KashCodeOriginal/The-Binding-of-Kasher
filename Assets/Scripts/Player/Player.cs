using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private InventoryObject _playerInventory;

    [SerializeField] private LighthouseFill _lighthouseFill;
    
    public InventoryObject PlayerInventory => _playerInventory;

    private void AddItemToInventory(ItemsData _item, int amount)
    {
        _playerInventory.AddItemToInventory(_item, amount);
    }
    private void RemoveItemTFromInventory(ItemsData _item, int amount)
    {
        _playerInventory.RemoveItemFromInventory(_item, amount);
    }

    private void OnEnable()
    {
        _lighthouseFill.DeleteWoodFromInventory += RemoveItemTFromInventory;
    }
    private void OnDisable()
    {
        _lighthouseFill.DeleteWoodFromInventory -= RemoveItemTFromInventory;
    }
}
