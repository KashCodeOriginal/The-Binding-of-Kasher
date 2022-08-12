using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory
{
    [SerializeField] private InventorySlot[] _items = new InventorySlot[16];
    public InventorySlot[] Items => _items;

    public void ClearItems()
    {
        _items = new InventorySlot[16];
    }
}
