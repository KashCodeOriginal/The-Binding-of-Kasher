using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class UserInterface : MonoBehaviour
{
    [SerializeField] private Player _player;
    
    [SerializeField] protected InventoryObject _playerInventory;

    protected Dictionary<GameObject, InventorySlot> _slorsOnInterface = new Dictionary<GameObject, InventorySlot>();
    
    private void Start()
    {
        for (int i = 0; i < _playerInventory.ItemsContainer.Items.Length; i++)
        {
            _playerInventory.ItemsContainer.Items[i].SetParent(this);
        }
        CreateInventorySlots();
        AddEvent(gameObject, EventTriggerType.PointerEnter, delegate { OnEnterInterface(gameObject); });
        AddEvent(gameObject, EventTriggerType.PointerExit, delegate { OnExitInterface(gameObject); });
    }

    private void Update()
    {
        UpdateInventorySlots();
    }

    public abstract void CreateInventorySlots();
    
    private void UpdateInventorySlots()
    {
        foreach (KeyValuePair<GameObject, InventorySlot> _slot in _slorsOnInterface)
        {
            if (_slot.Value.Item.ID >= 0)
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

    protected void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        var eventTrigger = new EventTrigger.Entry();
        eventTrigger.eventID = type;
        
        eventTrigger.callback.AddListener(action);
        
        trigger.triggers.Add(eventTrigger);
    }
    protected void OnEnterInterface(GameObject obj)
    {
        MouseData.InterfaceMouseIsOver = obj.GetComponent<UserInterface>();
    }
    protected void OnExitInterface(GameObject obj)
    {
        MouseData.InterfaceMouseIsOver = null;
    }
    protected void OnEnter(GameObject obj)
    {
        MouseData.SlotHoveredOver = obj;
        MouseData.InterfaceMouseIsOver = obj.gameObject.GetComponentInParent<UserInterface>();
    }
    protected void OnExit(GameObject obj)
    {
        MouseData.SlotHoveredOver = null;
    }
    protected void OnStartDrag(GameObject obj)
    {
        var mouseObject = new GameObject();
        var rectTransform = mouseObject.AddComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(100, 100);
        mouseObject.transform.SetParent(transform.parent);

        if (_slorsOnInterface[obj].Item.ID >= 0)
        {
            var image = mouseObject.AddComponent<Image>();
            image.sprite = _playerInventory.ItemsDataBase.GetItemByID[_slorsOnInterface[obj].Item.ID].Icon;
            image.raycastTarget = false;
        }

        MouseData.TempItemDragged = mouseObject;
    }
    protected void OnEndDrag(GameObject obj)
    {
        Destroy(MouseData.TempItemDragged);

        if (MouseData.InterfaceMouseIsOver == null)
        {
            _playerInventory.DropItem(_slorsOnInterface[obj].Item);
            return;
        }

        if (MouseData.SlotHoveredOver == true)
        {
            InventorySlot mouseHoverSlotData = MouseData.InterfaceMouseIsOver._slorsOnInterface[MouseData.SlotHoveredOver];
            //_playerInventory.SwapItems();
        }
    }

    protected void OnDrag(GameObject obj)
    {
        if (MouseData.TempItemDragged != null)
        {
            MouseData.TempItemDragged.GetComponent<RectTransform>().position = Input.mousePosition;
        }
    }
}

public static class MouseData
{
    public static UserInterface InterfaceMouseIsOver;
    public static GameObject TempItemDragged;
    public static GameObject SlotHoveredOver;
}
