using UnityEngine;
using UnityEngine.Events;

public class LighthouseFill : MonoBehaviour
{
    [SerializeField] private ItemsData _wood;

    [SerializeField] private InventoryObject _playerInventory;
    public event UnityAction<int> FillLightHouse;
    

    public void TryToFillLightHouse()
    {
        // for (int i = 0; i < _playerInventory.ItemsContainer.Slots.Length; i++)
        // {
        //     if (_playerInventory.ItemsContainer.Slots[i].Item == new Item(_wood) && _playerInventory.ItemsContainer.Slots[i].Amount > 0)
        //     {
        //         FillLightHouse?.Invoke(_playerInventory.ItemsContainer.Slots[i].Amount);
        //     }
        // }
    }
}
