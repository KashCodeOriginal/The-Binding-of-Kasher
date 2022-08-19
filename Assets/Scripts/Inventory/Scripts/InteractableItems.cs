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

    public void DisplayInteractableItem(InventorySlot mouseHoverSlotData)
    {
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
        _playerStatsChanger.IncreaseHunger(value);
        _playerActivePanel.RemoveItemAmountFromInventory(slot, 1);
    }
    private void IncreaseWater(int value, InventorySlot slot, ItemsData _item)
    {
        _playerStatsChanger.IncreaseWater(value);
        _playerActivePanel.RemoveItemAmountFromInventory(slot, 1);
        _dropResource.DropItem(_item.Data, 1);
    }
    private void IncreaseHealth(int value, InventorySlot slot)
    {
        _playerStatsChanger.IncreaseHealth(value);
        _playerActivePanel.RemoveItemAmountFromInventory(slot, 1);
    }

    public void TurnOffInfoDisplay()
    {
        if (_interactivePanel.activeSelf == true)
        {
            _interactivePanel.SetActive(false);
            
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
        _interactivePanel.SetActive(true);
        _interactaleUseItemButton.SetActive(true);
        _interactivePanel.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y + 140, 0);
        _buttonNameText.text = mouseHoverSlotData.ItemObject.Name;
        _buttonDescriptionText.text = mouseHoverSlotData.ItemObject.Description;
    }
}
