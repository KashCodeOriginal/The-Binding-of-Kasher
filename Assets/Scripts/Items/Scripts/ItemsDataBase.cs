using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemsDatabase", menuName = "ScriptableObject/Inventory/ItemsDataBase")]
public class ItemsDataBase : ScriptableObject, ISerializationCallbackReceiver
{
    public ItemsData[] ItemsData;
    
    [ContextMenu("Update ID's")]
    private void UpdateID()
    {
        for (int i = 0; i < ItemsData.Length; i++)
        {
            if(ItemsData[i].Data.ID != i)
            {
                ItemsData[i].Data.SetID(i);
            }
        }
    }
    public void OnAfterDeserialize()
    {
        UpdateID();
    }
    
    public void OnBeforeSerialize()
    {
    }
}
