using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObject/Craft")]
public class CraftRecipe : ScriptableObject, ISerializationCallbackReceiver
{
    [SerializeField] private Item _item;

    [SerializeField] private ItemsData _outputItem;

    [SerializeField] private int _outputItemAmount;

    [SerializeField] private CraftItem[] _requiredItems;

    [SerializeField] private InventoryObject _inventory;
    [SerializeField] private InventoryObject _playerActivePanel;

    public CraftRecipe()
    {
        
    }

    public bool CanCraftItem()
    {
        foreach (var item in _requiredItems)
        {
            if (_inventory.FindItemInInventory(item.Item, item.Amount) == false && _playerActivePanel.FindItemInInventory(item.Item, item.Amount) == false)
            {
                return false;
            }
        }
        return true;
    }

    public void Craft()
    {
        if (CanCraftItem() == true)
        {
            bool canCraft = _playerActivePanel.AddItemToInventory(_item, _outputItemAmount) == true || _inventory.AddItemToInventory(_item, _outputItemAmount) == true;

            if (canCraft == true)
            {
                foreach (var item in _requiredItems)
                {
                    if(_inventory.FindItemInInventory(item.Item, item.Amount) == true)
                    {
                        _inventory.RemoveItemAmountFromInventory(_inventory.FindItemInInventory(item.Item), item.Amount);
                    }
                    else if (_playerActivePanel.FindItemInInventory(item.Item, item.Amount) == true)
                    {
                        _playerActivePanel.RemoveItemAmountFromInventory(_playerActivePanel.FindItemInInventory(item.Item), item.Amount);
                    }
                }
            }
        }
    }
    public void OnAfterDeserialize()
    {
        _item?.SetID(_outputItem.Data.ID);
    }

    public void OnBeforeSerialize()
    {
        _item?.SetID(_outputItem.Data.ID);
    }
    
}
