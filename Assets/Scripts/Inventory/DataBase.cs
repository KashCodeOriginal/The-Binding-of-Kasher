using UnityEngine;

[CreateAssetMenu(fileName = "ItemsDatabase", menuName = "ScriptableObject/Inventory/DataBase")]
public class DataBase : ScriptableObject
{
    public ItemsData[] Items;
}
