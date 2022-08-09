using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDisplay : MonoBehaviour
{
    [SerializeField] private GameObject _inventorySlotPrefab;
    
    [SerializeField] private InventoryObject _playerInventory;

    [SerializeField] private int _xStartPositionItems;
    [SerializeField] private int _yStartPositionItems;
    
    [SerializeField] private int _xSpaceBetweenItems;
    [SerializeField] private int _ySpaceBetweenItems;
    
    [SerializeField] private int _columns;

    private Dictionary<InventorySlot, GameObject> _itemsDisplayed = new Dictionary<InventorySlot, GameObject>();

    private void Start()
    {
        CreateInventorySlots();
    }

    private void Update()
    {
        UpdateInventorySlots();
    }

    public void CreateInventorySlots()
    {
        for (int i = 0; i < _playerInventory.ItemContainer.Count; i++)
        {
            var obj = Instantiate(_inventorySlotPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetItemPosition(i);
            obj.transform.GetChild(0).GetComponentInChildren<Image>().sprite = _playerInventory.ItemContainer[i].Item.Icon;
            obj.GetComponentInChildren<TextMeshProUGUI>().text = _playerInventory.ItemContainer[i].Amount.ToString("n0");
            _itemsDisplayed.Add(_playerInventory.ItemContainer[i], obj);
        }
    }
    public void UpdateInventorySlots()
    {
        for (int i = 0; i < _playerInventory.ItemContainer.Count; i++)
        {
            if (_itemsDisplayed.ContainsKey(_playerInventory.ItemContainer[i]))
            {
                _itemsDisplayed[_playerInventory.ItemContainer[i]].GetComponentInChildren<TextMeshProUGUI>().text =
                    _playerInventory.ItemContainer[i].Amount.ToString("n0");
            }
            else
            {
                var obj = Instantiate(_playerInventory.ItemContainer[i].Item.Prefab, Vector3.zero, Quaternion.identity, transform);
                obj.GetComponent<RectTransform>().localPosition = GetItemPosition(i);
                obj.GetComponentInChildren<Image>().sprite = _playerInventory.ItemContainer[i].Item.Icon;
                obj.GetComponentInChildren<TextMeshProUGUI>().text = _playerInventory.ItemContainer[i].Amount.ToString("n0");
                _itemsDisplayed.Add(_playerInventory.ItemContainer[i], obj);
            }
        }
    }
    private Vector3 GetItemPosition(int number)
    {
        return new Vector3(_xStartPositionItems + (_xSpaceBetweenItems * (number % _columns)), _yStartPositionItems + (-_ySpaceBetweenItems * (number / _columns)), 0f); 
    }
}
