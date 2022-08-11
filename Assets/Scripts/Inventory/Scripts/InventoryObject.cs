 using System;
 using System.IO;
 using System.Runtime.Serialization;
 using System.Runtime.Serialization.Formatters.Binary;
 using Unity.VisualScripting;
 using UnityEngine;

[CreateAssetMenu(fileName = "InventoryObject", menuName = "ScriptableObject/Inventory")]
public class InventoryObject : ScriptableObject
{
    [SerializeField] private int _maxSlotsAmount;

    [SerializeField] private ItemsDataBase _itemsDataBase;

    [SerializeField] private string _savingPath;

    [SerializeField] private Inventory _itemsContainer;

    public Inventory ItemsContainer => _itemsContainer;

    public ItemsDataBase ItemsDataBase => _itemsDataBase;

    public void AddItemToInventory(Item item, int amount)
    {
        bool _hasItemInInventory = false;

        for(int i = 0; i < _itemsContainer.Items.Count; i++)
        {
            if (_itemsContainer.Items[i].Item.ID == item.ID && _itemsContainer.Items[i].MaxSlotAmount != 0)
            {
                if (amount <= _itemsContainer.Items[i].MaxSlotAmount)
                {
                    _itemsContainer.Items[i].AddItemAmount(amount);
                    _hasItemInInventory = true;
                    break;
                }
                if (amount > _itemsContainer.Items[i].MaxSlotAmount)
                {
                    int maxAmount = amount - _itemsContainer.Items[i].MaxSlotAmount;

                    int overMaxAmount = amount - maxAmount;
                    
                    _itemsContainer.Items[i].AddItemAmount(_itemsContainer.Items[i].MaxSlotAmount);
                    
                    if (_itemsContainer.Items.Count < _maxSlotsAmount)
                    {
                        AddNewItem(item, maxAmount);
                    }
                    else
                    {
                        Debug.Log($"Выбросили: {overMaxAmount} {item.Name}");
                    }
                    _hasItemInInventory = true;
                    break;
                }
            }   
        }
        if (_hasItemInInventory == false && _itemsContainer.Items.Count < _maxSlotsAmount)
        {
            AddNewItem(item, amount);
        }
    }
    

    public void RemoveItemFromInventory(Item item, int amount)
    {
        for(int i = 0; i < _itemsContainer.Items.Count; i++)
        {
            if (_itemsContainer.Items[i].Item.ID == item.ID)
            {
                _itemsContainer.Items[i].RemoveItemAmount(amount);
                if (_itemsContainer.Items[i].Amount <= 0)
                {
                    _itemsContainer.Items.RemoveAt(i);
                }
                break;
            }
        }
    }

    private void AddNewItem(Item item, int amount)
    {
        _itemsContainer.Items.Add(new InventorySlot(item.ID, item, amount));
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
            _itemsContainer = (Inventory) formatter.Deserialize(stream);
            stream.Close();
        }
    }

    public void ClearInventory()
    {
        _itemsContainer = new Inventory();
    }
}
