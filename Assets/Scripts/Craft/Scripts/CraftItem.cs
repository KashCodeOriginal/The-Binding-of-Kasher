using UnityEngine;

[System.Serializable]
public class CraftItem : ISerializationCallbackReceiver
{
    [SerializeField] private Item _item;

    [SerializeField] private ItemsData _itemData;

    [SerializeField] private int _amount;

    public Item Item => _item;

    public int Amount => _amount;

    public CraftItem (Item item)
    {
        _item = item;
    }
    
    public void OnAfterDeserialize()
    {
        _item?.SetID(_itemData.Data.ID);
    }

    public void OnBeforeSerialize()
    {
        _item?.SetID(_itemData.Data.ID);
    }

    
}
