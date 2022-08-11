using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory
{
    [SerializeField] private List<InventorySlot> _items = new List<InventorySlot>();
    public List<InventorySlot> Items => _items;
}
