using UnityEngine;

[System.Serializable]
public class InventorySlot
{
    [SerializeField] private ItemType[] AllowedItems = new ItemType[0];
    [System.NonSerialized] private UserInterface _parent;
    [SerializeField] private Item _item;
    [SerializeField] private int _id;
    [SerializeField] private int _amount;
    [SerializeField] private int _maxSlotAmount;
    
    public UserInterface Parent => _parent;
    public Item Item => _item;
    public int Amount => _amount;
    public int ID => _id;
    public int MaxSlotAmount => _maxSlotAmount;

    public InventorySlot()
    {
        _id = -1;
        _item = null;
        _amount = 0;
        _maxSlotAmount = 30;
    }
    public InventorySlot(int id, Item item, int amount)
    {
        _id = id;
        _item = item;
        _amount = amount;
        _maxSlotAmount -= amount;
    }
    public InventorySlot(int id, Item item, int amount, int maxAmount)
    {
        _id = id;
        _item = item;
        _amount = amount;
        _maxSlotAmount = maxAmount;
    }
    public void UpdateSlot(int id, Item item, int amount, int maxAmount)
    {
        _id = id;
        _item = item;
        _amount = amount;
        _maxSlotAmount = maxAmount;
    }


    public void AddItemAmount(int value)
    {
        _amount += value;
        _maxSlotAmount -= value;
    }

    public void RemoveItemAmount(int value)
    {
        _amount -= value;
        _maxSlotAmount += value;
    }

    public void SetItem(Item item)
    {
        _item = item;
    }

    public void SetParent(UserInterface parent)
    {
        _parent = parent;
    }

    public bool CanPlaceInSlot(ItemsData item)
    {
        if (AllowedItems.Length <= 0)
        {
            return true;
        }

        for (int i = 0; i < AllowedItems.Length; i++)
        {
            if (item.Type == AllowedItems[i])
            {
                return true;
            }
        }

        return false;
    }
}
