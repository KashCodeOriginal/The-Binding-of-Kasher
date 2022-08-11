using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryObject", menuName = "ScriptableObject/Inventory")]
public class InventoryObject : ScriptableObject, ISerializationCallbackReceiver
{
    [SerializeField] private List<InventorySlot> _itemsContainer = new List<InventorySlot>();

    [SerializeField] private int _maxSlotsAmount;

    private DataBase _dataBase;

    [SerializeField] private string _savingPath;

    public List<InventorySlot> ItemContainer => _itemsContainer;

    public void AddItemToInventory(ItemsData item, int amount)
    {
        bool _hasItemInInventory = false;

        for(int i = 0; i < _itemsContainer.Count; i++)
        {
            if (_itemsContainer[i].Item == item && _itemsContainer[i].MaxSlotAmount != 0)
            {
                if (amount <= _itemsContainer[i].MaxSlotAmount)
                {
                    _itemsContainer[i].AddItemAmount(amount);
                    _hasItemInInventory = true;
                    break;
                }
                if (amount > _itemsContainer[i].MaxSlotAmount)
                {
                    int maxAmount = amount - _itemsContainer[i].MaxSlotAmount;

                    int overMaxAmount = amount - maxAmount;
                    
                    _itemsContainer[i].AddItemAmount(_itemsContainer[i].MaxSlotAmount);
                    
                    if (_itemsContainer.Count < _maxSlotsAmount)
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
        if (_hasItemInInventory == false && _itemsContainer.Count < _maxSlotsAmount)
        {
            AddNewItem(item, amount);
        }
    }
    

    public void RemoveItemFromInventory(ItemsData item, int amount)
    {
        for(int i = 0; i < _itemsContainer.Count; i++)
        {
            if (_itemsContainer[i].Item == item)
            {
                _itemsContainer[i].RemoveItemAmount(amount);
                if (_itemsContainer[i].Amount <= 0)
                {
                    _itemsContainer.RemoveAt(i);
                }
                break;
            }
        }
    }

    private void AddNewItem(ItemsData item, int amount)
    {
        _itemsContainer.Add(new InventorySlot(_dataBase.GetID[item], item, amount));
    }

    public void SaveInventory()
    {
        string saveData = JsonUtility.ToJson(this, true);
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = File.Create(String.Concat(Application.persistentDataPath, _savingPath));
        binaryFormatter.Serialize(fileStream, saveData);
        fileStream.Close();
    }
    public void LoadInventory()
    {
        if (File.Exists(String.Concat(Application.persistentDataPath, _savingPath)))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = File.Open(String.Concat(Application.persistentDataPath, _savingPath), FileMode.Open);
            JsonUtility.FromJsonOverwrite(binaryFormatter.Deserialize(fileStream).ToString(), this);
            fileStream.Close();
        }
    }
    
    public void OnAfterDeserialize()
    {
        for (int i = 0; i < _itemsContainer.Count; i++)
        {
            _itemsContainer[i].SetItem(_dataBase.GetItemByID[_itemsContainer[i].ID]);
        }
    }

    public void OnBeforeSerialize()
    {
    }

    private void OnEnable()
    {
    #if UNITY_EDITOR
        _dataBase = (DataBase)AssetDatabase.LoadAssetAtPath("Assets/Resources/ItemsDatabase.asset", typeof(DataBase));
    #else 
    _dataBase = Resources.Load<DataBase>("ItemsDatabase");
    #endif
    }
}
