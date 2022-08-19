using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObject/Craft")]
[System.Serializable]
public class CraftRecipe : ScriptableObject
{
    [SerializeField] private ItemsData _outputItem;

    [SerializeField] private int _outputItemAmount;

    [SerializeField] private CraftItem[] _requiredItems;

    [SerializeField] private InventoryObject _inventory;
    [SerializeField] private InventoryObject _playerActivePanel;

    public bool CanCraftItem()
    {
        foreach (var item in _requiredItems)
        {
            if (_inventory.FindItemInInventory(item.Item.Data, item.Amount) == false && _playerActivePanel.FindItemInInventory(item.Item.Data, item.Amount) == false)
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
            bool canCraft = _playerActivePanel.AddItemToInventory(_outputItem.Data, _outputItemAmount) == true || _inventory.AddItemToInventory(_outputItem.Data, _outputItemAmount) == true;
            
            if (canCraft == true)
            {
                foreach (var item in _requiredItems)
                {
                    if(_inventory.FindItemInInventory(item.Item.Data, item.Amount) == true)
                    {
                        _inventory.RemoveItemAmountFromInventory(_inventory.FindItemInInventory(item.Item.Data), item.Amount);
                    }
                    else if (_playerActivePanel.FindItemInInventory(item.Item.Data, item.Amount) == true)
                    {
                        _playerActivePanel.RemoveItemAmountFromInventory(_playerActivePanel.FindItemInInventory(item.Item.Data), item.Amount);
                    }
                }
            }
        }
    }
}
