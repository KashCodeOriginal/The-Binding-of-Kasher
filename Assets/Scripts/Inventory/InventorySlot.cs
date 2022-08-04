using UnityEngine;

[System.Serializable]
public class InventorySlot
{
    [SerializeField] private ItemsData _item;
    [SerializeField] private int _amount;
    
    public ItemsData Item => _item;
    public int Amount => _amount;
    
    public InventorySlot(ItemsData item, int amount)
    {
        _item = item;
        _amount = amount;
    }

    public void AddItemAmount(int value)
    {
        _amount += value;
    }

    public void RemoveItemAmount(int value)
    {
        _amount -= value;
    }
}
