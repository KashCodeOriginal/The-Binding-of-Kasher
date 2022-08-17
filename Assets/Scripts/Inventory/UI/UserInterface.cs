using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class UserInterface : MonoBehaviour
{
    [SerializeField] protected InventoryObject _playerInventory;

    [SerializeField] private bool _isInventoryAbleToTakePartOfItem;

    [SerializeField] private float _holdTime;
    [SerializeField] private int _clampedValue = 0;
    [SerializeField] private float _timeBetweenValueAdd;
    [SerializeField] private float _decreaseTimeBetweenValueAdding;
    [SerializeField] private float _currentTime;

    [SerializeField] private bool _isSlotPressed = false;
    [SerializeField] private bool _isSlotClamped = false;

    private Vector3 _mousePosition;

    protected Dictionary<GameObject, InventorySlot> _slotsOnInterface = new Dictionary<GameObject, InventorySlot>();
    public InventoryObject PlayerInventory => _playerInventory;

    [SerializeField] private Transform _tempObjectsTransform;

    [SerializeField] private InteractableItems _interactableItems;

    private void Start()
    {
        for (int i = 0; i < _playerInventory.GetSlots.Length; i++)
        {
            _playerInventory.GetSlots[i].SetParent(this);
            _playerInventory.GetSlots[i].OnAfterUpdate += OnSlotUpdate;
        }
        CreateInventorySlots();
        AddEvent(gameObject, EventTriggerType.PointerEnter, delegate { OnEnterInterface(gameObject); });
        AddEvent(gameObject, EventTriggerType.PointerExit, delegate { OnExitInterface(gameObject); });
        
        _playerInventory.LoadInventory();
    }

    private void Update()
    {
        if (_isInventoryAbleToTakePartOfItem == true)
        {
            if (_isSlotPressed == true && _mousePosition == Input.mousePosition)
            {
                _currentTime += Time.deltaTime;

                if (_currentTime >= _holdTime)
                {
                    _isSlotClamped = true;
                    _currentTime = 0;
                    
                    _interactableItems.TurnOffInfoDisplay();
                }
            }

            if (_mousePosition != Input.mousePosition)
            {
                _currentTime = 0;
            }

            if (_isSlotClamped == true)
            {
                _currentTime += Time.deltaTime;

                if (_currentTime >= _timeBetweenValueAdd)
                {
                    _clampedValue++;
                    _currentTime = 0;

                    if (_timeBetweenValueAdd > 0.1)
                    {
                        _timeBetweenValueAdd -= _decreaseTimeBetweenValueAdding;
                    }
                }
            }
        }
    }

    private void OnSlotUpdate(InventorySlot _slot)
    {
        if (_slot.Item.ID >= 0)
        {
            _slot.SlotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().sprite = _slot.ItemObject.Icon;
            _slot.SlotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
            _slot.SlotDisplay.GetComponentInChildren<TextMeshProUGUI>().text = _slot.Amount == 1 ? "" : _slot.Amount.ToString("n0");
        }
        else
        {
            _slot.SlotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
            _slot.SlotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
            _slot.SlotDisplay.GetComponentInChildren<TextMeshProUGUI>().text = "";
        }
    }
    
    public abstract void CreateInventorySlots();
    
    protected void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        var eventTrigger = new EventTrigger.Entry();
        eventTrigger.eventID = type;
        
        eventTrigger.callback.AddListener(action);
        
        trigger.triggers.Add(eventTrigger);
    }
    protected void OnDown(GameObject obj)
    {
        _interactableItems.TurnOffInfoDisplay();
        
        _isSlotPressed = true;
        _mousePosition = Input.mousePosition;
        
        _interactableItems.TurnOffInfoDisplay();

        InventorySlot mouseHoverSlotData = null;

        if (MouseData.InterfaceMouseIsOver != null && MouseData.SlotHoveredOver != null && _slotsOnInterface[MouseData.SlotHoveredOver] != null)
        {
            mouseHoverSlotData = MouseData.InterfaceMouseIsOver._slotsOnInterface[MouseData.SlotHoveredOver];
        }

        if (mouseHoverSlotData != null && mouseHoverSlotData.Item.ID >= 0)
        {
            _interactableItems.DisplayInteractableItem(mouseHoverSlotData);
        }
    }
    

    protected void OnUp(GameObject obj)
    {
        if (obj != null)
        {
            if (_isSlotClamped == true)
            {
                InventorySlot mouseHoverSlotData = MouseData.InterfaceMouseIsOver._slotsOnInterface[MouseData.SlotHoveredOver];
                if(mouseHoverSlotData != null){}
                _playerInventory.TakePartOfItem(_slotsOnInterface[obj], mouseHoverSlotData, _clampedValue);
                _clampedValue = 0;
                StartCoroutine(ValueChangeStateDelay());
            }
        }

        _isSlotPressed = false;
        _currentTime = 0;
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
        MouseData.TempItemDragged = CreateTempObject(obj);
        _interactableItems.TurnOffInfoDisplay();
    }

    public GameObject CreateTempObject(GameObject obj)
    {
        GameObject tempItem = null;

        if (_slotsOnInterface[obj].Item.ID >= 0)
        {
            tempItem = new GameObject();
            var rectTransform = tempItem.AddComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(100, 100);
            tempItem.transform.SetParent(_tempObjectsTransform,false);
            var image = tempItem.AddComponent<Image>();
            image.sprite = _slotsOnInterface[obj].ItemObject.Icon;
            image.raycastTarget = false;
        }

        return tempItem;
    }
    
    protected void OnEndDrag(GameObject obj)
    {
        Destroy(MouseData.TempItemDragged);

        if (_tempObjectsTransform.childCount > 0)
        {
            for (int i = 0; i <= _tempObjectsTransform.childCount - 1; i++)
            {
                Destroy(_tempObjectsTransform.GetChild(i).gameObject);
            }
        }

        if (MouseData.InterfaceMouseIsOver == null && Input.touchCount <= 1)
        {
            _playerInventory.DropItemFromInventory(_slotsOnInterface[obj]);
            return;
        }

        if (_isSlotClamped == false)
        {
            if (MouseData.SlotHoveredOver == true)
            {
                InventorySlot mouseHoverSlotData = MouseData.InterfaceMouseIsOver._slotsOnInterface[MouseData.SlotHoveredOver];
                _playerInventory.SwapItems(_slotsOnInterface[obj], mouseHoverSlotData);
            }
        }
    }

    protected void OnDrag(GameObject obj)
    {
        if (MouseData.TempItemDragged != null)
        {
            MouseData.TempItemDragged.GetComponent<RectTransform>().position = Input.mousePosition;
        }
    } 
    public IEnumerator ValueChangeStateDelay()
    {
        yield return new WaitForSeconds(0.1f);
        _isSlotClamped = false;
    }
}

public static class MouseData
{
    public static UserInterface InterfaceMouseIsOver;
    public static GameObject TempItemDragged;
    public static GameObject SlotHoveredOver;
}


