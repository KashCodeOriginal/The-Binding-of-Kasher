using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryObject", menuName = "ScriptableObject/Inventory")]
public class InventoryObject : ScriptableObject
{
    [SerializeField] private List<InventorySlot> _itemsContainer = new List<InventorySlot>();

    [SerializeField] private int _maxSlotsAmount;

    public List<InventorySlot> ItemContainer => _itemsContainer;

    public void AddItemToInventory(ItemsData item, int amount)
    {
        bool _hasItemInInventory = false;

        for(int i = 0; i < _itemsContainer.Count; i++)
        {
            if (_itemsContainer[i].Item == item && _itemsContainer[i].MaxSlotAmount != 0)
            {
                if (amount <= _itemsContainer[i].MaxSlotAmount)
                {
                    _itemsContainer[i].AddItemAmount(amount);
                    _hasItemInInventory = true;
                    break;
                }
                if (amount > _itemsContainer[i].MaxSlotAmount)
                {
                    int maxAmount = amount - _itemsContainer[i].MaxSlotAmount;

                    int overMaxAmount = amount - maxAmount;
                    
                    _itemsContainer[i].AddItemAmount(_itemsContainer[i].MaxSlotAmount);
                    
                    if (_itemsContainer.Count < _maxSlotsAmount)
                    {
                        AddNewItem(item, maxAmount);
                    }
                    else
                    {
                        Debug.Log($"Выбросили: {overMaxAmount} {item.Name}");
                    }
                    
                    
                    _hasItemInInventory = true;
                    break;
                }
            }   
        }
        if (_hasItemInInventory == false && _itemsContainer.Count < _maxSlotsAmount)
        {
            AddNewItem(item, amount);
        }
    }
    

    public void RemoveItemFromInventory(ItemsData item, int amount)
    {
        for(int i = 0; i < _itemsContainer.Count; i++)
        {
            if (_itemsContainer[i].Item == item)
            {
                _itemsContainer[i].RemoveItemAmount(amount);
                if (_itemsContainer[i].Amount <= 0)
                {
                    _itemsContainer.RemoveAt(i);
                }
                break;
            }
        }
    }

    private void AddNewItem(ItemsData item, int amount)
    {
        _itemsContainer.Add(new InventorySlot(item, amount));
    }
    
}
