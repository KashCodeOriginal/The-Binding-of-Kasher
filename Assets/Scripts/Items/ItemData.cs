using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObject/Item")]
public class ItemsData : ScriptableObject
{
    [SerializeField] private string _id;
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private ItemType _type;
    [SerializeField] private Sprite _icon;
    [SerializeField] private GameObject _prefab;

    public string ID => _id;
    public string Name => _name;
    public string Description => _description;
    public ItemType Type => _type;
    public Sprite Icon => _icon;
    public GameObject Prefab => _prefab;
}

public enum ItemType
{ 
    Food,
    Resource
}

