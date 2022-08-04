using UnityEngine;
using UnityEngine.Events;

public class LighthouseFill : MonoBehaviour
{
    [SerializeField] private ItemsData _wood;

    [SerializeField] private Player _player;
    public event UnityAction<int> FillLightHouse;

    public event UnityAction<ItemsData, int> DeleteWoodFromInventory;

    public void TryToFillLightHouse()
    {
        for (int i = 0; i < _player.PlayerInventory.ItemContainer.Count; i++)
        {
            if (_player.PlayerInventory.ItemContainer[i].Item == _wood && _player.PlayerInventory.ItemContainer[i].Amount > 0)
            {
                FillLightHouse?.Invoke(_player.PlayerInventory.ItemContainer[i].Amount);
                DeleteWoodFromInventory?.Invoke(_wood, _player.PlayerInventory.ItemContainer[i].Amount);
            }
        }
    }
}
