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
        InventorySlot slot = FindItemInInventory(item);
        
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
    
    
    public void SwapItems(InventorySlot item1, InventorySlot item2)
    {
        if (item1.Item.ID == item2.Item.ID && item1.SlotDisplay != item2.SlotDisplay)
        {
            if (item1.Amount + item2.Amount > 30)
            {
                int overItemsSumm = item1.Amount + item2.Amount - 30;

                item1.UpdateSlot(item1.Item, overItemsSumm,30 - overItemsSumm);
                item2.UpdateSlot(item2.Item, 30, 0);
                return;
            }
            item2.UpdateSlot(item1.Item,  item1.Amount + item2.Amount, item1.MaxSlotAmount - item2.Amount);
            item1.RemoveItem();
            return;
        }
        if (item2.CanPlaceInSlot(item1.ItemObject) && item1.CanPlaceInSlot(item2.ItemObject))
        {
            Debug.Log("2");
            InventorySlot temp = new InventorySlot(item2.Item, item2.Amount, item2.MaxSlotAmount);
            item2.UpdateSlot(item1.Item, item1.Amount, item1.MaxSlotAmount);
            item1.UpdateSlot(temp.Item, temp.Amount, temp.MaxSlotAmount);
        }
    }
    public void TakePartOfItem(InventorySlot item1, InventorySlot item2, int clampedValue)
    {
        if (clampedValue > 0 && item1.SlotDisplay != item2.SlotDisplay)
        {
            if (item2.CanPlaceInSlot(item1.ItemObject) && item1.CanPlaceInSlot(item2.ItemObject) && item2.Item.ID < 0)
            {
                if (item2.CanPlaceInSlot(item1.ItemObject))
                {
                    item1.UpdateSlot(item1.Item, item1.Amount - clampedValue, item1.MaxSlotAmount + clampedValue); 
                    item2.UpdateSlot(item1.Item, clampedValue, item2.MaxSlotAmount - clampedValue);
                }
            }
            else if(item2.CanPlaceInSlot(item1.ItemObject) && item1.CanPlaceInSlot(item2.ItemObject) && item1.Item.ID == item2.Item.ID)
            {
                if (item2.CanPlaceInSlot(item1.ItemObject))
                {
                    item1.UpdateSlot(item1.Item, item1.Amount - clampedValue, item1.MaxSlotAmount + clampedValue); 
                    item2.UpdateSlot(item1.Item, item2.Amount + clampedValue, item2.MaxSlotAmount - clampedValue);
                }
            }
        }
    }

    public void DropItemFromInventory(Item item)
    {
        CreateDroppingItem(item, 0);
    }
    public void DropItemFromInventory(Item item, int amount)
    {
        CreateDroppingItem(item, amount);
    }

    private void CreateDroppingItem(Item item, int amount)
    {
        for (int i = 0; i < GetSlots.Length; i++)
        {
            if (GetSlots[i].Item == item)
            {
                if (amount == 0)
                {
                    amount = GetSlots[i].Amount;
                }
                
                var prefab = GetSlots[i].ItemObject.Prefab;
           
                var player = GameObject.FindWithTag("Player");
                var obj = Instantiate(prefab, new Vector3(player.transform.position.x, player.transform.position.y + 2, player.transform.position.z + 2), Quaternion.identity);
                var component = obj.TryGetComponent(out GroundItem groundItem);
                if (component == true)
                {
                    obj.GetComponent<GroundItem>().SetAmount(amount);
                }
                
                obj.GetComponent<Rigidbody>().AddForce(Vector3.forward * 2, ForceMode.Impulse);
                obj.GetComponent<Rigidbody>().AddForce(Vector3.up * 2, ForceMode.Impulse);
                
                GetSlots[i].UpdateSlot(new Item(), 0, 30);
            }
        }
    }

    private InventorySlot FindFirstEmptySlot(Item item, int amount)
    {
        for (int i = 0; i < _itemsContainer.Slots.Length; i++)
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
