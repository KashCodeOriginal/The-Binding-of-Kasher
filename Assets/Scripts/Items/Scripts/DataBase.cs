using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemsDatabase", menuName = "ScriptableObject/Inventory/DataBase")]
public class DataBase : ScriptableObject, ISerializationCallbackReceiver
{
    public ItemsData[] Items;

    public Dictionary<ItemsData, int> GetID = new Dictionary<ItemsData, int>();

    public Dictionary<int, ItemsData> GetItemByID = new Dictionary<int, ItemsData>();
    
    public void OnAfterDeserialize()
    {
        GetID = new Dictionary<ItemsData, int>();
        GetItemByID = new Dictionary<int, ItemsData>();
        for (int i = 0; i < Items.Length; i++)
        {
            GetID.Add(Items[i], i);
            GetItemByID.Add(i,Items[i]);
        }
    }
    
    public void OnBeforeSerialize()
    {
    }
}
