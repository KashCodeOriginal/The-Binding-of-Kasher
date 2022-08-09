using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemsDatabase", menuName = "ScriptableObject/Inventory/DataBase")]
public class DataBase : ScriptableObject, ISerializationCallbackReceiver
{
    public ItemsData[] Items;

    public Dictionary<ItemsData, int> GetID = new Dictionary<ItemsData, int>();
    
    public void OnAfterDeserialize()
    {
        GetID = new Dictionary<ItemsData, int>();
        for (int i = 0; i < Items.Length; i++)
        {
            GetID.Add(Items[i], i);
        }
    }
    
    public void OnBeforeSerialize()
    {
        throw new System.NotImplementedException();
    }
}
