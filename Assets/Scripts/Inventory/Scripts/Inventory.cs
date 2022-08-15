using UnityEngine;

[System.Serializable]
public class Inventory
{
    [SerializeField] private InventorySlot[] _slots = new InventorySlot[24];
    public InventorySlot[] Slots => _slots;

    public void ClearItems()
    {
        for (int i = 0; i < Slots.Length; i++)
        {
            Slots[i].RemoveItem();
        }
    }
}
