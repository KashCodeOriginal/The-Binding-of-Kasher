 using System;
 using System.IO;
 using System.Runtime.Serialization;
 using System.Runtime.Serialization.Formatters.Binary;
 using UnityEngine;

[CreateAssetMenu(fileName = "InventoryObject", menuName = "ScriptableObject/Inventory")]
public class InventoryObject : ScriptableObject
{
    [SerializeField] private ItemsDataBase _itemsDataBase;

    [SerializeField] private string _savingPath;

    [SerializeField] private Inventory _itemsContainer;

    public Inventory ItemsContainer => _itemsContainer;

    public ItemsDataBase ItemsDataBase => _itemsDataBase;

    public void AddItemToInventory(Item item, int amount)
    {
        bool _hasItemInInventory = false;

        for(int i = 0; i < _itemsContainer.Items.Length; i++)
        {
            if (_itemsContainer.Items[i].Item.ID == item.ID && _itemsContainer.Items[i].MaxSlotAmount != 0)
            {
                if (amount <= _itemsContainer.Items[i].MaxSlotAmount)
                {
                    _itemsContainer.Items[i].AddItem(amount);
                    _hasItemInInventory = true;
                    break;
                }
                
                if (amount > _itemsContainer.Items[i].MaxSlotAmount)
                {
                    int maxAmount = amount - _itemsContainer.Items[i].MaxSlotAmount;
                
                    //int overMaxAmount = amount - maxAmount;
                    
                    _itemsContainer.Items[i].AddItem(_itemsContainer.Items[i].MaxSlotAmount);
                    
                    FindFirstEmptySlot(item, maxAmount);
                    
                    _hasItemInInventory = true;
                    break;
                }
            }   
 
        }
        if (_hasItemInInventory == false)
        {
            FindFirstEmptySlot(item, amount);
        }
    }
    
    
    public void RemoveItemFromInventory(Item item, int amount)
    {
        for(int i = 0; i < _itemsContainer.Items.Length; i++)
        {
            if (_itemsContainer.Items[i].Item.ID == item.ID)
            {
                break;
            }
        }
    }

    public void SwapItems(InventorySlot item1, InventorySlot item2)
    {
        if(item2.CanPlaceInSlot())
        InventorySlot temp = new InventorySlot(item2.ID, item2.Item, item2.Amount, item2.MaxSlotAmount);
        item2.UpdateSlot(item1.ID, item1.Item, item1.Amount, item1.MaxSlotAmount);
        item1.UpdateSlot(temp.ID, temp.Item, temp.Amount, temp.MaxSlotAmount);
    }

    public void DropItem(Item item)
    {
        for (int i = 0; i < _itemsContainer.Items.Length; i++)
        {
            if (_itemsContainer.Items[i].Item == item)
            {
                
                var prefab = _itemsDataBase.GetItemByID[_itemsContainer.Items[i].Item.ID].Prefab;
           
                var player = GameObject.FindWithTag("Player");
                var obj = Instantiate(prefab, new Vector3(player.transform.position.x, player.transform.position.y + 2, player.transform.position.z + 2), Quaternion.identity);
                var component = obj.TryGetComponent(out GroundItem groundItem);
                if (component == true)
                {
                    obj.GetComponent<GroundItem>().SetAmount(_itemsContainer.Items[i].Amount);
                }
                _itemsContainer.Items[i].UpdateSlot( null, 0, 30);
            }
        }
    }
    

    private InventorySlot FindFirstEmptySlot(Item item, int amount)
    {
        for (int i = 0; i < _itemsContainer.Items.Length; i++)
        {
            if (_itemsContainer.Items[i].Item.ID <= -1)
            {
                _itemsContainer.Items[i].UpdateSlot(item, amount, _itemsContainer.Items[i].MaxSlotAmount - amount);
                return _itemsContainer.Items[i];
            }
        }
        // if (_itemsContainer.Items.Count < _maxSlotsAmount)
        // {
        //     FindFirstEmptySlot(item, maxAmount);
        // }
        // else
        // {
        //     Debug.Log($"Выбросили: {overMaxAmount} {item.Name}");
        // }
        return null;
    }

    public void SaveInventory()
    {
        /*
        string saveData = JsonUtility.ToJson(this, true);
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = File.Create(string.Concat(Application.persistentDataPath, _savingPath));
        binaryFormatter.Serialize(fileStream, saveData);
        fileStream.Close();
        */

        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(string.Concat(Application.persistentDataPath, _savingPath), FileMode.Create, FileAccess.Write);
        formatter.Serialize(stream, _itemsContainer);
        stream.Close();
    }
    public void LoadInventory()
    {
        if (File.Exists(String.Concat(Application.persistentDataPath, _savingPath)))
        {
            /*
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = File.Open(String.Concat(Application.persistentDataPath, _savingPath), FileMode.Open);
            JsonUtility.FromJsonOverwrite(binaryFormatter.Deserialize(fileStream).ToString(), this);
            fileStream.Close();
            */
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(string.Concat(Application.persistentDataPath, _savingPath), FileMode.Open, FileAccess.Read);
            Inventory newContainer = (Inventory)formatter.Deserialize(stream);
            for (int i = 0; i < _itemsContainer.Items.Length; i++)
            {
                _itemsContainer.Items[i].UpdateSlot(newContainer.Items[i].Item, newContainer.Items[i].Amount, newContainer.Items[i].MaxSlotAmount);
            }
            stream.Close();
        }
    }

    public void ClearInventory()
    {
        _itemsContainer.ClearItems();
    }
}
