using UnityEngine;

[System.Serializable]
public class MeltItem
{
    [SerializeField] private ItemsData _itemData;

    [SerializeField] private int _amount;

    public ItemsData Item => _itemData;

    public int Amount => _amount;

    public MeltItem(ItemsData item)
    {
        _itemData = item;
    }
}
