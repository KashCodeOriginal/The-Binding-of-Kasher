using UnityEngine;

public class CollectWater : MonoBehaviour
{
    [SerializeField] private CollectWaterDisplay _collectWaterDisplay;

    [SerializeField] private InventoryObject _playerInventory;
    [SerializeField] private InventoryObject _playerActivePanel;

    [SerializeField] private DropResource _dropResource;

    [SerializeField] private ItemsData _emptyCup;
    [SerializeField] private ItemsData _fullCup;
    
    public void TryCollectWater()
    {
        if (_playerInventory.FindItemInInventory(_emptyCup.Data, 1) == true || _playerActivePanel.FindItemInInventory(_emptyCup.Data, 1) == true)
        {
            _collectWaterDisplay.DisplayWaterInterface();
        }
        else
        {
            _collectWaterDisplay.HideWaterInterface();
        }
    }

    public void CollectWaterButtonClick()
    {
        if (_playerInventory.FindItemInInventory(_emptyCup.Data, 1) == true)
        {
            _playerInventory.RemoveItemAmountFromInventory(_playerInventory.FindItemInInventory(_emptyCup.Data), 1);
            _dropResource.DropItem(_fullCup.Data, 1);
        }
        else if (_playerActivePanel.FindItemInInventory(_emptyCup.Data, 1) == true)
        {
            _playerActivePanel.RemoveItemAmountFromInventory(_playerActivePanel.FindItemInInventory(_emptyCup.Data), 1);
            _dropResource.DropItem(_fullCup.Data, 1);
        }
        
    }
}
