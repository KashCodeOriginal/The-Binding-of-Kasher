using UnityEngine;

[System.Serializable]
public class InventorySlot
{
    [SerializeField] private ItemType[] AllowedItems = new ItemType[0];
    [System.NonSerialized] private UserInterface _parent;
    [System.NonSerialized] private GameObject _slotDisplay;

    [SerializeField] private Item _item;
    [SerializeField] private int _amount;
    [SerializeField] private int _maxSlotAmount;
    
    [System.NonSerialized] public SlotUpdated OnAfterUpdate;
    [System.NonSerialized] public SlotUpdated OnBeforeUpdate;
    
    public Item Item => _item;
    public int Amount => _amount;
    public int MaxSlotAmount => _maxSlotAmount;
    public GameObject SlotDisplay => _slotDisplay;

    public ItemsData ItemObject
    {
        get
        {
            if (_item.ID >= 0)
            {
                return _parent.PlayerInventory.ItemsDataBase.ItemsData[_item.ID];
            }

            return null;
        }
    }

    public InventorySlot()
    {
        UpdateSlot(new Item(), 0, 30);
    }
    public InventorySlot(Item item, int amount)
    {
        UpdateSlot(item, amount, _maxSlotAmount -= amount);
    }
    public InventorySlot(Item item, int amount, int maxAmount)
    {
        UpdateSlot(item, amount, maxAmount);
    }
    public void AddItem(int value)
    {
        UpdateSlot(_item, _amount += value, _maxSlotAmount -= value);
    }

    public void RemoveItem()
    {
        UpdateSlot(new Item(), 0, 30);
    }
    
    public void UpdateSlot(Item item, int amount, int maxAmount)
    {
        OnBeforeUpdate?.Invoke(this);
        _item = item;
        _amount = amount;
        _maxSlotAmount = maxAmount;
        OnAfterUpdate?.Invoke(this);
    }


    public void SetItem(Item item)
    {
        _item = item;
    }

    public void SetParent(UserInterface parent)
    {
        _parent = parent;
    }

    public void SetSlot(GameObject slot)
    {
        _slotDisplay = slot;
    }

    public bool CanPlaceInSlot(ItemsData itemData)
    {
        if (AllowedItems.Length <= 0 || itemData == null || itemData.Data.ID < 0)
        {
            return true;
        }

        for (int i = 0; i < AllowedItems.Length; i++)
        {
            if (itemData.Type == AllowedItems[i])
            {
                return true;
            }
        }

        return false;
    }
}

public delegate void SlotUpdated(InventorySlot _inventorySlot);
