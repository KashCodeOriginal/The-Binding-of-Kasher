using UnityEngine;
using UnityEngine.Events;

public class LighthouseFill : MonoBehaviour
{
    [SerializeField] private Item _wood;

    [SerializeField] private InventoryObject _playerInventory;

    public event UnityAction<int> FillLightHouse;
    
    

    public void TryToFillLightHouse()
    {
        for (int i = 0; i < _playerInventory.ItemsContainer.Items.Count; i++)
        {
            if (_playerInventory.ItemsContainer.Items[i].Item == _wood && _playerInventory.ItemsContainer.Items[i].Amount > 0)
            {
                FillLightHouse?.Invoke(_playerInventory.ItemsContainer.Items[i].Amount);
                _playerInventory.RemoveItemFromInventory(_wood, _playerInventory.ItemsContainer.Items[i].Amount);
            }
        }
    }
}
