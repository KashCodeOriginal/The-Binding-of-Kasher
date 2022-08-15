using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StaticInterface : UserInterface
{
    [SerializeField] private GameObject[] _slots;

    public GameObject[] Slots => _slots;
    public override void CreateInventorySlots()
    {
        _slotsOnInterface = new Dictionary<GameObject, InventorySlot>();

        for (int i = 0; i < _playerInventory.GetSlots.Length; i++)
        {
            var obj = _slots[i];
            
            AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
            AddEvent(obj, EventTriggerType.PointerDown, delegate { OnDown(obj); });
            AddEvent(obj, EventTriggerType.PointerUp, delegate { OnUp(obj); });
            AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
            AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnStartDrag(obj); });
            AddEvent(obj, EventTriggerType.EndDrag, delegate { OnEndDrag(obj); });
            AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });

            _playerInventory.GetSlots[i].SetSlot(obj);
            
            _slotsOnInterface.Add(obj, _playerInventory.GetSlots[i]);
        }
    }
}
