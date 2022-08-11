using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObject/Item")]
public class ItemsData : ScriptableObject
{
    [SerializeField] private int _id;
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private ItemType _type;
    [SerializeField] private Sprite _icon;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private ItemBuff[] _itemBuffs;

    public Item CreateItem()
    {
        Item newItem = new Item(this);
        return newItem;
    }

    public int ID => _id;
    public string Name => _name;
    public string Description => _description;
    public ItemType Type => _type;
    public Sprite Icon => _icon;
    public GameObject Prefab => _prefab;
    public ItemBuff[] ItemBuffs => _itemBuffs;
    
    public void SetId(int id)
    {
        _id = id;
    }
}

public enum ItemType
{ 
    Food,
    Resource,
    Building,
    Tools,
    Equipment,
    Other
}
public enum Attributes
{ 
    RecoveryValue
}


