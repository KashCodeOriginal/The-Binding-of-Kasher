using UnityEngine;

[System.Serializable]
public class CraftItem
{
    [SerializeField] private ItemsData _itemData;

    [SerializeField] private int _amount;

    public ItemsData Item => _itemData;

    public int Amount => _amount;

    public CraftItem(ItemsData item)
    {
        _itemData = item;
    }
}
