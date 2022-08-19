using UnityEditor;
using UnityEngine;

[System.Serializable]
public class Item
{
    [SerializeField] private string _name;
    [SerializeField] private int _id = -1;
    [SerializeField] private ItemBuff[] _itemBuffs;

    public string Name => _name;
    public int ID => _id;

    public ItemBuff[] ItemBuffs => _itemBuffs;

    public Item()
    {
        _name = "";
        _id = -1;
    }

    public Item(ItemsData item)
    {
        _name = item.Name;
        _id = item.Data.ID;
        _itemBuffs = new ItemBuff[item.Data.ItemBuffs.Length];

        for (int i = 0; i < _itemBuffs.Length; i++)
        {
            _itemBuffs[i] = new ItemBuff(item.Data.ItemBuffs[i].Value);
            _itemBuffs[i].SetAttribute(item.Data.ItemBuffs[i].Attribute);
        }
    }

    public void SetID(int id)
    {
        _id = id;
    }
}
