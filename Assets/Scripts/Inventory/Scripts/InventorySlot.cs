using UnityEditor;
using UnityEngine;

[System.Serializable]
public class InventorySlot
{
    [SerializeField] private Item _item;
    [SerializeField] private int _id;
    [SerializeField] private int _amount;
    [SerializeField] private int _maxSlotAmount = 30;

    public Item Item => _item;
    public int Amount => _amount;
    public int ID => _id;

    public int MaxSlotAmount => _maxSlotAmount;

    public InventorySlot(int id, Item item, int amount)
    {
        _id = id;
        _item = item;
        _amount = amount;
        _maxSlotAmount -= amount;
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
}
