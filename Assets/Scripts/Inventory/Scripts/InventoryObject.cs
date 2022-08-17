 using System;
 using System.IO;
 using System.Runtime.Serialization;
 using System.Runtime.Serialization.Formatters.Binary;
 using Unity.VisualScripting;
 using UnityEngine;

[CreateAssetMenu(fileName = "InventoryObject", menuName = "ScriptableObject/Inventory")]
public class InventoryObject : ScriptableObject
{
    [SerializeField] private ItemsDataBase _itemsDataBase;

    [SerializeField] private string _savingPath;

    [SerializeField] private Inventory _itemsContainer;
    
    public InventorySlot[] GetSlots
    {
        get
        {
            return _itemsContainer.Slots;
        }
    }

    public Inventory ItemsContainer => _itemsContainer;

    public ItemsDataBase ItemsDataBase => _itemsDataBase;

    public bool AddItemToInventory(Item item, int amount)
    {
        InventorySlot slot = FindAvailableItemInInventory(item);

        if (EmptySlotCount <= 0)
        {
            return false;
        }

        if (_itemsDataBase.ItemsData[item.ID].Stackable == false || slot == null)
        {
            FindFirstEmptySlot(item, amount);
            return true;
        }

        if (amount <= slot.MaxSlotAmount)
        {
            slot.AddItem(amount);
            return true;
        }
        if (amount > slot.MaxSlotAmount)
        {
            int maxAmount = amount - slot.MaxSlotAmount;

            //int overMaxAmount = amount - maxAmount;
                    
            slot.AddItem(slot.MaxSlotAmount);
            
            FindFirstEmptySlot(item, maxAmount);

            /*if (overMaxAmount > 0)
            {
                DropItemFromInventory(item, overMaxAmount);
            }*/
            
            return true;
        }
        
        return true;
    }

    public InventorySlot FindAvailableItemInInventory(Item item)
    {
        for (int i = 0; i < GetSlots.Length; i++)
        {
            if (GetSlots[i].Item.ID == item.ID && GetSlots[i].Amount < 30)
            {
                return GetSlots[i];
            }
        }

        return null;
    }
    public InventorySlot FindItemInInventory(Item item)
    {
        for (int i = 0; i < GetSlots.Length; i++)
        {
            if (GetSlots[i].Item.ID == item.ID)
            {
                return GetSlots[i];
            }
        }
        return null;
    }
    public bool FindItemInInventory(Item item, int requiredAmount)
    {
        for (int i = 0; i < GetSlots.Length; i++)
        {
            if (GetSlots[i].Item.ID == item.ID && GetSlots[i].Amount >= requiredAmount)
            {
                return true;
            }
        }
        return false;
    }
    
    public int EmptySlotCount
    {
        get
        {
            int counter = 0;
            for (int i = 0; i < GetSlots.Length; i++)
            {
                if (GetSlots[i].Item.ID <= -1)
                {
                    counter++;
                }
            }
            return counter;
        }
    }
    
    
    public void SwapItems(InventorySlot slotItem, InventorySlot newSlotItem)
    {
        if (slotItem.Item.ID == newSlotItem.Item.ID && slotItem.SlotDisplay != newSlotItem.SlotDisplay)
        {
            if (slotItem.Amount + newSlotItem.Amount > 30)
            {
                int overItemsSumm = slotItem.Amount + newSlotItem.Amount - 30;

                slotItem.UpdateSlot(slotItem.Item, overItemsSumm,30 - overItemsSumm);
                newSlotItem.UpdateSlot(newSlotItem.Item, 30, 0);
                return;
            }
            newSlotItem.UpdateSlot(slotItem.Item,  slotItem.Amount + newSlotItem.Amount, slotItem.MaxSlotAmount - newSlotItem.Amount);
            slotItem.RemoveItem();
            return;
        }
        if (newSlotItem.CanPlaceInSlot(slotItem.ItemObject) && slotItem.CanPlaceInSlot(newSlotItem.ItemObject))
        {
            InventorySlot temp = new InventorySlot(newSlotItem.Item, newSlotItem.Amount, newSlotItem.MaxSlotAmount);
            newSlotItem.UpdateSlot(slotItem.Item, slotItem.Amount, slotItem.MaxSlotAmount);
            slotItem.UpdateSlot(temp.Item, temp.Amount, temp.MaxSlotAmount);
        }
    }
    public void TakePartOfItem(InventorySlot slotItem, InventorySlot newSlotItem, int clampedValue)
    {
        if (clampedValue > 0 && slotItem.SlotDisplay != newSlotItem.SlotDisplay && slotItem.Amount <= 30 && newSlotItem.Amount < 30)
        {
            if (clampedValue > slotItem.Amount)
            {
                clampedValue = slotItem.Amount;
            }
            if (newSlotItem.CanPlaceInSlot(slotItem.ItemObject) && slotItem.CanPlaceInSlot(newSlotItem.ItemObject) && newSlotItem.Item.ID < 0)
            {
                if (newSlotItem.CanPlaceInSlot(slotItem.ItemObject))
                {
                    slotItem.UpdateSlot(slotItem.Item, slotItem.Amount - clampedValue, slotItem.MaxSlotAmount + clampedValue); 
                    newSlotItem.UpdateSlot(slotItem.Item, clampedValue, newSlotItem.MaxSlotAmount - clampedValue);
                }
            }
            else if(newSlotItem.CanPlaceInSlot(slotItem.ItemObject) && slotItem.CanPlaceInSlot(newSlotItem.ItemObject) && slotItem.Item.ID == newSlotItem.Item.ID)
            {
                if (newSlotItem.CanPlaceInSlot(slotItem.ItemObject) && newSlotItem.Amount + clampedValue <= 30)
                {
                    slotItem.UpdateSlot(slotItem.Item, slotItem.Amount - clampedValue, slotItem.MaxSlotAmount + clampedValue); 
                    newSlotItem.UpdateSlot(slotItem.Item, newSlotItem.Amount + clampedValue, newSlotItem.MaxSlotAmount - clampedValue);
                }
                else if (newSlotItem.CanPlaceInSlot(slotItem.ItemObject) && newSlotItem.Amount + clampedValue > 30)
                {
                    int overItemsSumm = 30 - newSlotItem.Amount;
                    if (clampedValue > overItemsSumm)
                    {
                        clampedValue = overItemsSumm;
                    }

                    newSlotItem.UpdateSlot(slotItem.Item, 30, 0); 
                    slotItem.UpdateSlot(slotItem.Item, slotItem.Amount - clampedValue, slotItem.MaxSlotAmount + clampedValue);
                }
            }

            if (slotItem.Amount <= 0)
            {
                slotItem.RemoveItem();
            }
        }
    }

    public void RemoveItemAmountFromInventory(InventorySlot slot, int amount)
    {
        if (slot.Amount - amount <= 0)
        {
            slot.RemoveItem();
            return;
        }
        
        slot.UpdateSlot(slot.Item, slot.Amount - amount, slot.MaxSlotAmount + amount);
    }

    public void DropItemFromInventory(InventorySlot item)
    {
        CreateDroppingItem(item, 0);
    }
    public void DropItemFromInventory(InventorySlot item, int amount)
    {
        CreateDroppingItem(item, amount);
    }

    private void CreateDroppingItem(InventorySlot slot, int amount)
    {
        if (amount == 0)
        {
            amount = slot.Amount;
        }
        
        var prefab = slot.ItemObject.Prefab;
           
        var player = GameObject.FindWithTag("Player");
        var obj = Instantiate(prefab, new Vector3(player.transform.position.x, player.transform.position.y + 2, player.transform.position.z + 2), Quaternion.identity);
        var component = obj.TryGetComponent(out GroundItem groundItem);
        if (component == true)
        {
            obj.GetComponent<GroundItem>().SetAmount(amount);
        }
                
        obj.GetComponent<Rigidbody>().AddForce(Vector3.forward * 2, ForceMode.Impulse);
        obj.GetComponent<Rigidbody>().AddForce(Vector3.up * 2, ForceMode.Impulse);
        slot.UpdateSlot(new Item(), 0, 30);
    }

    private InventorySlot FindFirstEmptySlot(Item item, int amount)
    {
        for (int i = 0; i < GetSlots.Length; i++)
        {
            if (GetSlots[i].Item.ID <= -1)
            {
                GetSlots[i].UpdateSlot(item, amount, GetSlots[i].MaxSlotAmount - amount);
                return GetSlots[i];
            }
        }
        return null;
    }

    public void SaveInventory()
    {
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(string.Concat(Application.persistentDataPath, _savingPath), FileMode.Create, FileAccess.Write);
        formatter.Serialize(stream, _itemsContainer);
        stream.Close();
    }
    public void LoadInventory()
    {
        if (File.Exists(String.Concat(Application.persistentDataPath, _savingPath)))
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(string.Concat(Application.persistentDataPath, _savingPath), FileMode.Open, FileAccess.Read);
            Inventory newContainer = (Inventory)formatter.Deserialize(stream);
            for (int i = 0; i < GetSlots.Length; i++)
            {
                GetSlots[i].UpdateSlot(newContainer.Slots[i].Item, newContainer.Slots[i].Amount, newContainer.Slots[i].MaxSlotAmount);
            }
            stream.Close();
        }
    }

    public void ClearInventory()
    {
        _itemsContainer.ClearItems();
    }
}
