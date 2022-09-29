using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractableItems : MonoBehaviour
{
    [SerializeField] private GameObject _interactivePanel;

    [SerializeField] private TextMeshProUGUI _buttonNameText;
    [SerializeField] private TextMeshProUGUI _buttonDescriptionText;
    [SerializeField] private TextMeshProUGUI _buttonUseText;

    [SerializeField] private GameObject _interactaleUseItemButton;

    [SerializeField] private PlayerStatsChanger _playerStatsChanger;

    [SerializeField] private InventoryObject _playerActivePanel;

    [SerializeField] private DropResource _dropResource;

    [SerializeField] private ItemsData _emptyCup;

    [SerializeField] private PlantItem _plantItem;

    [SerializeField] private PlaceItem _placeItem;

    [SerializeField] private GameObject _closeInteractivePanelButton;

    public void DisplayInteractableItem(InventorySlot mouseHoverSlotData)
    {
        _closeInteractivePanelButton.SetActive(true);
        
        if (mouseHoverSlotData.ItemObject.Type == ItemType.Food)
        {
            int recoveryValue = 0;
            
            DisplayItem(mouseHoverSlotData);
            if (mouseHoverSlotData.ItemObject.Data.ItemBuffs[0].Attribute == Attributes.RecoveryValue)
            {
                recoveryValue = mouseHoverSlotData.ItemObject.Data.ItemBuffs[0].Value;
            }

            _buttonUseText.text = "Eat!";
        
            _interactaleUseItemButton.GetComponent<Button>().onClick.AddListener(() => IncreaseHunger(recoveryValue, mouseHoverSlotData));
        }
        else if (mouseHoverSlotData.ItemObject.Type == ItemType.Drinks)
        {
            int recoveryValue = 0;
            
            DisplayItem(mouseHoverSlotData);

            if (mouseHoverSlotData.ItemObject.Data.ItemBuffs.Length > 0)
            {
                if (mouseHoverSlotData.ItemObject.Data.ItemBuffs[0].Attribute == Attributes.RecoveryValue)
                {
                    recoveryValue = mouseHoverSlotData.ItemObject.Data.ItemBuffs[0].Value;
                }
            
                _buttonUseText.text = "Drink!";
        
                _interactaleUseItemButton.GetComponent<Button>().onClick.AddListener(() => IncreaseWater(recoveryValue, mouseHoverSlotData, _emptyCup));
                return;
            }
            _interactaleUseItemButton.SetActive(false);
        }
        else if (mouseHoverSlotData.ItemObject.Type == ItemType.Aid)
        {
            int recoveryValue = 0;
            DisplayItem(mouseHoverSlotData);
            
            if (mouseHoverSlotData.ItemObject.Data.ItemBuffs[0].Attribute == Attributes.RecoveryValue)
            {
                recoveryValue = mouseHoverSlotData.ItemObject.Data.ItemBuffs[0].Value;
            }
            
            _buttonUseText.text = "Use!";
        
            _interactaleUseItemButton.GetComponent<Button>().onClick.AddListener(() => IncreaseHealth(recoveryValue, mouseHoverSlotData));
        }
        else if (mouseHoverSlotData.ItemObject.Type == ItemType.Plants)
        {
            DisplayItem(mouseHoverSlotData);
            
            _buttonUseText.text = "Plant!";
        
            _interactaleUseItemButton.GetComponent<Button>().onClick.AddListener(() => TryPlantItem(mouseHoverSlotData));
        }
        else if (mouseHoverSlotData.ItemObject.Type == ItemType.Lightning)
        {
            DisplayItem(mouseHoverSlotData);
            
            _buttonUseText.text = "Place!";
        
            _interactaleUseItemButton.GetComponent<Button>().onClick.AddListener(() => TryPlaceItem(mouseHoverSlotData));
        }
        else
        {
            _interactivePanel.SetActive(true);
            _interactivePanel.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y + 140, 0);
            _buttonNameText.text = mouseHoverSlotData.ItemObject.Name;
            _buttonDescriptionText.text = mouseHoverSlotData.ItemObject.Description;
            _interactaleUseItemButton.SetActive(false);
        }
    }
    private void IncreaseHunger(int value, InventorySlot slot)
    {
        if (IsSlotContainsItem(slot) == false)
        {
            return;
        }
        _playerStatsChanger.IncreaseHunger(value);
        _playerActivePanel.RemoveItemAmountFromInventory(slot, 1);
    }
    private void IncreaseWater(int value, InventorySlot slot, ItemsData item)
    {
        if (IsSlotContainsItem(slot) == false)
        {
            return;
        }
        _playerStatsChanger.IncreaseWater(value);
        _playerActivePanel.RemoveItemAmountFromInventory(slot, 1);
        _dropResource.DropItem(item.Data, 1);
    }
    private void IncreaseHealth(int value, InventorySlot slot)
    {
        if (IsSlotContainsItem(slot) == false)
        {
            return;
        }
        _playerStatsChanger.IncreaseHealth(value);
        _playerActivePanel.RemoveItemAmountFromInventory(slot, 1);
    }
    private void TryPlantItem(InventorySlot slot)
    {
        if (IsSlotContainsItem(slot) == false)
        {
            return;
        }
        _plantItem.Plant(slot.ItemObject);
        _playerActivePanel.RemoveItemAmountFromInventory(slot, 1);
    }
    private void TryPlaceItem(InventorySlot slot)
    {
        if (IsSlotContainsItem(slot) == false)
        {
            return;
        }
        _placeItem.PlaceItemToGround(slot.ItemObject);
        _playerActivePanel.RemoveItemAmountFromInventory(slot, 1);
    }

    public void TurnOffInfoDisplay()
    {
        if (_interactivePanel.activeSelf == true)
        {
            CloseInteractivePanel();
            
            if (_interactaleUseItemButton.activeSelf == true)
            {
                _interactaleUseItemButton.GetComponent<Button>().onClick.RemoveAllListeners();
            }
        }
    }

    private void DisplayItem(InventorySlot mouseHoverSlotData)
    {
        if (_interactivePanel.activeSelf == true)
        {
            _interactivePanel.SetActive(false);
            _interactivePanel.GetComponentInChildren<Button>().onClick.RemoveAllListeners();
            return;
        }

        if (Input.touchCount <= 1)
        {
            _interactivePanel.SetActive(true);
            _interactaleUseItemButton.SetActive(true);
            _interactivePanel.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y + 140, 0);
            _buttonNameText.text = mouseHoverSlotData.ItemObject.Name;
            _buttonDescriptionText.text = mouseHoverSlotData.ItemObject.Description;
        }
    }

    private bool IsSlotContainsItem(InventorySlot currentSlot)
    {
        if (currentSlot.Amount <= 0)
        {
            return false;
        }

        if (currentSlot.Amount == 1)
        {
            CloseInteractivePanel();
        }

        return true;
    }

    private void CloseInteractivePanel()
    {
        _interactivePanel.SetActive(false);
        _closeInteractivePanelButton.SetActive(false);
    }
}
