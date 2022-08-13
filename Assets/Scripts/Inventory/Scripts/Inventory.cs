using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory
{
    [SerializeField] private InventorySlot[] _items = new InventorySlot[24];
    public InventorySlot[] Items => _items;

    public void ClearItems()
    {
        for (int i = 0; i < Items.Length; i++)
        {
            Items[i].UpdateSlot(-1, new Item(), 0, 30);
        }
    }
}
