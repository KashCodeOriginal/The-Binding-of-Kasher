using UnityEngine;

public class PlayerTriggers : MonoBehaviour
{
    [SerializeField] private CollectWood _collectWood;
    [SerializeField] private CollectWater _collectWater;
    [SerializeField] private LighthouseDisplay _lighthouseDisplay;
    [SerializeField] private CollectOreDisplay _collectOreDisplay;
    [SerializeField] private Fishing _fishing;
    [SerializeField] private OvenDisplay _ovenDisplay;
    [SerializeField] private CollectMeat _collectMeat;
    [SerializeField] private PlayerHouse _playerHouse;
    //[SerializeField] private 
    
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
            _collectMeat.DisplayFarmInterface();
        }
        if (collider.CompareTag("House"))
        {
            _playerHouse.DisplayHouseInterface();
        }
        if (collider.CompareTag("Wheat"))
        {
            _playerHouse.DisplayHouseInterface();
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
            _collectWater.TryCollectWater();
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
            _collectMeat.HideFarmInterface();
        }
        if (collider.CompareTag("House"))
        {
            _playerHouse.HideHouseInterface();
        }
    }
}
