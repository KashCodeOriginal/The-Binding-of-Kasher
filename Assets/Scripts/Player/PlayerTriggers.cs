using UnityEngine;

public class PlayerTriggers : MonoBehaviour
{
    [SerializeField] private CollectWood _collectWood;
    [SerializeField] private CollectWater _collectWater;
    [SerializeField] private CollectWaterDisplay _collectWaterDisplay;
    [SerializeField] private LighthouseDisplay _lighthouseDisplay;
    [SerializeField] private CollectOreDisplay _collectOreDisplay;
    [SerializeField] private Fishing _fishing;
    [SerializeField] private FishingDisplay _fishingDisplay;
    [SerializeField] private OvenDisplay _ovenDisplay;
    [SerializeField] private CollectMeatDisplay _collectMeatDisplay;
    [SerializeField] private PlayerHouseDisplay _playerHouseDisplay;
    [SerializeField] private CollectWheatDisplay _collectWheatDisplay;
    [SerializeField] private CollectTorchDisplay _collectTorchDisplay;
    [SerializeField] private CollectTorch _collectTorch;
    
    [SerializeField] private InventoryObject _playerInventory;
    [SerializeField] private InventoryObject _playerActivePanel;
    
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Tree"))
        {
            _collectWood.CollectWoodDisplay.CollectWoodInterfaceActive(true);
            _collectWood.CollectWoodDisplay.StartCollectWoodButton(true);
            _collectWood.SetCurrentTree(collider.gameObject);
        }

        if (collider.GetComponent<GroundItem>() == true)
        {
            var item = collider.GetComponent<GroundItem>();
            if(_playerActivePanel.AddItemToInventory(item.Item.Data, item.Amount) == true)
            {
                Destroy(collider.gameObject);
                return;
            }
            if (_playerInventory.AddItemToInventory(item.Item.Data, item.Amount))
            {
                Destroy(collider.gameObject);
            }
        }
        
        if (collider.CompareTag("Water"))
        {
            _collectWater.TryCollectWater();
            _fishing.TryToCatchFish();
        }

        if (collider.CompareTag("Lighthouse"))
        {
            _lighthouseDisplay.LighthouseInterfaceDisplay();
        }
        if (collider.CompareTag("Mine"))
        {
            _collectOreDisplay.CollectOreInterfaceActive(true);
            _collectOreDisplay.StartCollectOreButton(true);
        }
        if (collider.CompareTag("Oven"))
        {
            _ovenDisplay.DisplayOvenInterface();
        }
        if (collider.CompareTag("Farm"))
        {
            _collectMeatDisplay.DisplayCollectMeatInterface();
        }
        if (collider.CompareTag("House"))
        {
            _playerHouseDisplay.DisplayHouseInterface();
        }
        if (collider.CompareTag("Wheat"))
        {
            _collectWheatDisplay.DisplayCollectWheatInterface();
        }
        if (collider.CompareTag("Torch"))
        {
            _collectTorchDisplay.DisplayTorchCollectInterface();
            _collectTorch.SetCurrentTorch(collider.gameObject);
        }
    }
    private void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Tree"))
        {
            _collectWood.CollectWoodDisplay.CollectWoodInterfaceActive(false);
            _collectWood.CollectWoodDisplay.StartCollectWoodButton(false);
        }
        if (collider.CompareTag("Water"))
        {
            _collectWaterDisplay.HideWaterInterface();
            _fishingDisplay.HideFishingInterface();
        }
        if (collider.CompareTag("Lighthouse"))
        {
            _lighthouseDisplay.LighthouseInterfaceDisplay();
        }
        if (collider.CompareTag("Mine"))
        {
            _collectOreDisplay.CollectOreInterfaceActive(false);
            _collectOreDisplay.StartCollectOreButton(false);
        }
        if (collider.CompareTag("Oven"))
        {
            _ovenDisplay.HideOvenInterface();
        }
        if (collider.CompareTag("Farm"))
        {
            _collectMeatDisplay.HideCollectMeatInterface();
        }
        if (collider.CompareTag("House"))
        {
            _playerHouseDisplay.HideHouseInterface();
        }
        if (collider.CompareTag("Wheat"))
        {
            _collectWheatDisplay.HideCollectWheatInterface();
        }
        if (collider.CompareTag("Torch"))
        {
            _collectTorchDisplay.HideTorchCollectInterface();
            _collectTorch.SetCurrentTorch(null);
        }
    }
}
