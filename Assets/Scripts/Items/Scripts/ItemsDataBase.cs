using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemsDatabase", menuName = "ScriptableObject/Inventory/ItemsDataBase")]
public class ItemsDataBase : ScriptableObject, ISerializationCallbackReceiver
{
    public ItemsData[] Items;

    public Dictionary<int, ItemsData> GetItemByID = new Dictionary<int, ItemsData>();
    
    public void OnAfterDeserialize()
    {
        for (int i = 0; i < Items.Length; i++)
        {
            Items[i].SetId(i);
            GetItemByID.Add(i,Items[i]);
        }
    }
    
    public void OnBeforeSerialize()
    {
        GetItemByID = new Dictionary<int, ItemsData>();
    }
}
