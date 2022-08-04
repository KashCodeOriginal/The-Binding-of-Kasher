using UnityEngine;

public class LighthouseFill : MonoBehaviour
{
    [SerializeField] private ItemsData _wood;

    [SerializeField] private Player _player;
    
    private void TryToFillLightHouse()
    {
        for (int i = 0; i < _player.PlayerInventory.ItemContainer.Count; i++)
        {
            if (_player.PlayerInventory.ItemContainer[i].Item == _wood)
            {
               
            }
        }
    }
    
}
