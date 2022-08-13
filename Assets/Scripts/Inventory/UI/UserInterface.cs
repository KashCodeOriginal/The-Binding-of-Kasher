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

    protected Dictionary<GameObject, InventorySlot> _itemsDisplayed = new Dictionary<GameObject, InventorySlot>();
    
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
        _player.MouseItem.UI = obj.GetComponent<UserInterface>();
    }
    protected void OnExitInterface(GameObject obj)
    {
        _player.MouseItem.UI = null;
    }
    protected void OnEnter(GameObject obj)
    {
        _player.MouseItem.HoverObj = obj;
        _player.MouseItem.UI = obj.gameObject.GetComponentInParent<UserInterface>();
        if (_itemsDisplayed.ContainsKey(obj))
        {
            _player.MouseItem.HoverItem = _itemsDisplayed[obj];
        }
    }
    protected void OnExit(GameObject obj)
    {
        _player.MouseItem.HoverObj = null;
        _player.MouseItem.HoverItem = null;
    }
    protected void OnStartDrag(GameObject obj)
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

        _player.MouseItem.Obj = mouseObject;
        _player.MouseItem.Item = _itemsDisplayed[obj];
    }
    protected void OnEndDrag(GameObject obj)
    {
        var itemOnMouse = _player.MouseItem;
        var mouseHoverItem = itemOnMouse.HoverItem;
        var mouseHoverObj = itemOnMouse.HoverObj;
        var GetItemObject = _playerInventory.ItemsDataBase.GetItemByID;

        if (itemOnMouse.UI != null)
        {
            if (mouseHoverObj == true)
            {
                if (mouseHoverItem.CanPlaceInSlot(GetItemObject[_itemsDisplayed[obj].ID]) && (mouseHoverItem.Item.ID <= -1 || (mouseHoverItem.Item.ID >= 0 && _itemsDisplayed[obj].CanPlaceInSlot(GetItemObject[mouseHoverItem.Item.ID]))))
                {
                    _playerInventory.MoveItem(_itemsDisplayed[obj],mouseHoverItem.Parent._itemsDisplayed[itemOnMouse.HoverObj]);
                }
            }
        }
        else
        {
            _playerInventory.DropItem(_itemsDisplayed[obj].Item);
        }
        Destroy(itemOnMouse.Obj);
        itemOnMouse.Item = null;
    }

    protected void OnDrag(GameObject obj)
    {
        if (_player.MouseItem.Obj != null)
        {
            _player.MouseItem.Obj.GetComponent<RectTransform>().position = Input.mousePosition;
        }
    }
}

public class MouseItem
{
    public UserInterface UI;
    public GameObject Obj;
    public InventorySlot Item;
    public InventorySlot HoverItem;
    public GameObject HoverObj;
}
