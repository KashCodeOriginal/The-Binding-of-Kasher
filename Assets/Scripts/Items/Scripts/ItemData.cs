using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObject/Item")]
public class ItemsData : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private ItemType _type;
    [SerializeField] private Sprite _icon;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private Item _data = new Item();
    [SerializeField] private bool _stackable;

    public Item CreateItem()
    {
        Item newItem = new Item(this);
        return newItem;
    }
    
    public string Name => _name;
    public string Description => _description;
    public ItemType Type => _type;
    public Sprite Icon => _icon;
    public GameObject Prefab => _prefab;
    public Item Data => _data;
    public bool Stackable => _stackable;
}

public enum ItemType
{ 
    Food,
    Drinks,
    Aid,
    Resource,
    Building,
    HandEquipment,
    Other,
    Helmet,
    ChestPlate,
    Pants,
    Shoes,
}

public enum Attributes
{ 
    RecoveryValue,
    Usages
}


