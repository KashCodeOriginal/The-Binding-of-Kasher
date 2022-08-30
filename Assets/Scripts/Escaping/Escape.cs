using UnityEngine;

public class Escape : MonoBehaviour
{
    [SerializeField] private InventoryObject _playerEquipment;

    [SerializeField] private ItemsData _woodenBoat;
    
    [SerializeField] private ItemsData _powerBoat;

    [SerializeField] private EscapeDisplay _escapeDisplay;

    [SerializeField] private float _woodBoatEscapeChance;
    
    public void CheckForItemsInHand()
    {
        if (_playerEquipment.ItemsContainer.Slots[^1].ItemObject == _woodenBoat)
        {
            _escapeDisplay.DisplayWoodenBoatEscapingInterface();
        }
        else
        {
            _escapeDisplay.HideWoodenBoatEscapingInterface();
        }
        if(_playerEquipment.ItemsContainer.Slots[^1].ItemObject == _powerBoat)
        {
            _escapeDisplay.DisplayPowerBoatEscapingInterface();
        }
        else
        {
            _escapeDisplay.HidePowerBoatEscapingInterface();
        }
    }

    public void TryEscapeByWoodenBoat()
    {
        var value = Random.Range(0, 100);

        if (value <= _woodBoatEscapeChance)
        {
            Debug.Log("Сбежал");
        }
    }
    public void EscapeByPowerBoat()
    {
        Debug.Log("Сбежал");
    }
}
    

