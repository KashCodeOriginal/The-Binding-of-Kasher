using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObject/Craft")]
public class CraftRecipe : ScriptableObject, ISerializationCallbackReceiver
{
    private Item _outputItem;

    [SerializeField] private ItemsData _item;

    [SerializeField] private int _outputItemAmount;

    [SerializeField] private CraftItem[] _requiredItems;

    [SerializeField] private InventoryObject _inventory;
    [SerializeField] private InventoryObject _playerActivePanel;

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
            
            _inventory.AddItemToInventory(_outputItem, _outputItemAmount);
        }
    }
    public void OnAfterDeserialize()
    {
        _outputItem?.SetID(_item.Data.ID);
    }

    public void OnBeforeSerialize()
    {
        
    }
    
}
