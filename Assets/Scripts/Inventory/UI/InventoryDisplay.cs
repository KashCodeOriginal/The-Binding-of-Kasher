using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryDisplay : MonoBehaviour
{
    [SerializeField] private MouseItem _mouseItem = new MouseItem();
    
    [SerializeField] private GameObject _inventorySlotPrefab;
    
    [SerializeField] private InventoryObject _playerInventory;
    
    private Dictionary<GameObject, InventorySlot> _itemsDisplayed = new Dictionary<GameObject, InventorySlot>();
    
    private void Start()
    {
        CreateInventorySlots();
    }

    private void Update()
    {
        UpdateInventorySlots();
    }
    
    private void CreateInventorySlots()
    {
        _itemsDisplayed = new Dictionary<GameObject, InventorySlot>();

        for (int i = 0; i < _playerInventory.ItemsContainer.Items.Length; i++)
        {
            var obj = Instantiate(_inventorySlotPrefab, Vector3.zero, Quaternion.identity, transform);
            _itemsDisplayed.Add(obj, _playerInventory.ItemsContainer.Items[i]);
            
            AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
            AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
            AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnStartDrag(obj); });
            AddEvent(obj, EventTriggerType.EndDrag, delegate { OnEndDrag(obj); });
            AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });
        }
    }

    private void UpdateInventorySlots()
    {
        foreach (KeyValuePair<GameObject, InventorySlot> _slot in _itemsDisplayed)
        {
            if (_slot.Value.ID >= 0)
            {
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = _playerInventory.ItemsDataBase.GetItemByID[_slot.Value.Item.ID].Icon;
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
                _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = _slot.Value.Amount == 1 ? "" : _slot.Value.Amount.ToString("n0");
            }
            else
            {
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
                _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = "";
            }
        }
    }

    private void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        var eventTrigger = new EventTrigger.Entry();
        eventTrigger.eventID = type;
        
        eventTrigger.callback.AddListener(action);
        
        trigger.triggers.Add(eventTrigger);
    }

    public void OnEnter(GameObject obj)
    {
        _mouseItem.HoverObj = obj;
        if (_itemsDisplayed.ContainsKey(obj))
        {
            _mouseItem.HoverItem = _itemsDisplayed[obj];
        }
    }
    public void OnExit(GameObject obj)
    {
        _mouseItem.HoverObj = null;
        _mouseItem.HoverItem = null;
    }
    public void OnStartDrag(GameObject obj)
    {
        var mouseObject = new GameObject();
        var rectTransform = mouseObject.AddComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(100, 100);
        mouseObject.transform.SetParent(transform.parent);

        if (_itemsDisplayed[obj].ID >= 0)
        {
            var image = mouseObject.AddComponent<Image>();
            image.sprite = _playerInventory.ItemsDataBase.GetItemByID[_itemsDisplayed[obj].ID].Icon;
            image.raycastTarget = false;
        }

        _mouseItem.Obj = mouseObject;
        _mouseItem.Item = _itemsDisplayed[obj];
    }
    public void OnEndDrag(GameObject obj)
    {
        if (_mouseItem.HoverObj == true)
        {
            _playerInventory.MoveItem(_itemsDisplayed[obj], _itemsDisplayed[_mouseItem.HoverObj]);
        }
        else
        {
            _playerInventory.DropItem(_itemsDisplayed[obj].Item);
        }
        Destroy(_mouseItem.Obj);
        _mouseItem.Item = null;
    }
    public void OnDrag(GameObject obj)
    {
        if (_mouseItem.Obj != null)
        {
            _mouseItem.Obj.GetComponent<RectTransform>().position = Input.mousePosition;
        }
    }
}
