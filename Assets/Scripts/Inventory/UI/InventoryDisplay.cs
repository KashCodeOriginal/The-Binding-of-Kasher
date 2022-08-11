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
        for (int i = 0; i < _playerInventory.ItemsContainer.Items.Count; i++)
        {
            InventorySlot slot = _playerInventory.ItemsContainer.Items[i];
            
            var obj = Instantiate(_inventorySlotPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetItemPosition(i);
            obj.transform.GetChild(0).GetComponentInChildren<Image>().sprite = _playerInventory.ItemsDataBase.GetItemByID[slot.Item.ID].Icon;
            obj.GetComponentInChildren<TextMeshProUGUI>().text = slot.Amount.ToString("n0");
            _itemsDisplayed.Add(slot, obj);
        }
    }
    public void UpdateInventorySlots()
    {
        for (int i = 0; i < _playerInventory.ItemsContainer.Items.Count; i++)
        {
            InventorySlot slot = _playerInventory.ItemsContainer.Items[i];
            
            if (_itemsDisplayed.ContainsKey(slot))
            {
                _itemsDisplayed[slot].GetComponentInChildren<TextMeshProUGUI>().text = slot.Amount.ToString("n0");
            }
            else
            {
                var obj = Instantiate(_inventorySlotPrefab, Vector3.zero, Quaternion.identity, transform);
                obj.GetComponent<RectTransform>().localPosition = GetItemPosition(i);
                obj.transform.GetChild(0).GetComponentInChildren<Image>().sprite = _playerInventory.ItemsDataBase.GetItemByID[slot.Item.ID].Icon;
                obj.GetComponentInChildren<TextMeshProUGUI>().text = _playerInventory.ItemsContainer.Items[i].Amount.ToString("n0");
                _itemsDisplayed.Add(slot, obj);
            }
        }
    }
    private Vector3 GetItemPosition(int number)
    {
        return new Vector3(_xStartPositionItems + (_xSpaceBetweenItems * (number % _columns)), _yStartPositionItems + (-_ySpaceBetweenItems * (number / _columns)), 0f); 
    }
}
