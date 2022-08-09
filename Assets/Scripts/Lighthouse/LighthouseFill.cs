using UnityEngine;
using UnityEngine.Events;

public class LighthouseFill : MonoBehaviour
{
    [SerializeField] private ItemsData _wood;

    [SerializeField] private InventoryObject _playerInventory;

    public event UnityAction<int> FillLightHouse;
    
    

    public void TryToFillLightHouse()
    {
        for (int i = 0; i < _playerInventory.ItemContainer.Count; i++)
        {
            if (_playerInventory.ItemContainer[i].Item == _wood && _playerInventory.ItemContainer[i].Amount > 0)
            {
                FillLightHouse?.Invoke(_playerInventory.ItemContainer[i].Amount);
                _playerInventory.RemoveItemFromInventory(_wood, _playerInventory.ItemContainer[i].Amount);
            }
        }
    }
}
