using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DynamicInterface : UserInterface
{
    [SerializeField] private GameObject _inventorySlotPrefab;
    
    public override void CreateInventorySlots()
    {
        _slotsOnInterface = new Dictionary<GameObject, InventorySlot>();

        for (int i = 0; i < _playerInventory.GetSlots.Length; i++)
        {
            var obj = Instantiate(_inventorySlotPrefab, Vector3.zero, Quaternion.identity, transform);
            _slotsOnInterface.Add(obj, _playerInventory.GetSlots[i]);
            
            _playerInventory.GetSlots[i].SetSlot(obj);
            
            AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
            AddEvent(obj, EventTriggerType.PointerDown, delegate { OnDown(obj); });
            AddEvent(obj, EventTriggerType.PointerUp, delegate { OnUp(obj); });
            AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
            AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnStartDrag(obj); });
            AddEvent(obj, EventTriggerType.EndDrag, delegate { OnEndDrag(obj); });
            AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });
        }
    }
}
