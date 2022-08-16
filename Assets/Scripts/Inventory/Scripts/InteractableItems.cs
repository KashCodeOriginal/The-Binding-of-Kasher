using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractableItems : MonoBehaviour
{
    [SerializeField] private GameObject _interactiveButton;

    [SerializeField] private PlayerStatsChanger _playerStatsChanger;

    [SerializeField] private InventoryObject _playerActivePanel;

    public void DisplayInteractableItem(InventorySlot mouseHoverSlotData)
    {
        if (mouseHoverSlotData.ItemObject.Type == ItemType.Food)
        {
            DisplayItem(_interactiveButton, mouseHoverSlotData);
            int recoveryValue = mouseHoverSlotData.ItemObject.Data.ItemBuffs[0].Value;
        
            _interactiveButton.GetComponentInChildren<Button>().onClick.AddListener(() => IncreaseHunger(recoveryValue, mouseHoverSlotData));
        }
        else if (mouseHoverSlotData.ItemObject.Type == ItemType.Drinks)
        {
            DisplayItem(_interactiveButton, mouseHoverSlotData);
            int recoveryValue = mouseHoverSlotData.ItemObject.Data.ItemBuffs[0].Value;
        
            _interactiveButton.GetComponentInChildren<Button>().onClick.AddListener(() => IncreaseWater(recoveryValue, mouseHoverSlotData));
        }
        else if (mouseHoverSlotData.ItemObject.Type == ItemType.Aid)
        {
            DisplayItem(_interactiveButton, mouseHoverSlotData);
            int recoveryValue = mouseHoverSlotData.ItemObject.Data.ItemBuffs[0].Value;
        
            _interactiveButton.GetComponentInChildren<Button>().onClick.AddListener(() => IncreaseHealth(recoveryValue, mouseHoverSlotData));
        }
        
    }
    private void IncreaseHunger(int value, InventorySlot slot)
    {
        _playerStatsChanger.IncreaseHunger(value);
        _playerActivePanel.RemoveItemAmountFromInventory(slot, 1);
    }
    private void IncreaseWater(int value, InventorySlot slot)
    {
        _playerStatsChanger.IncreaseWater(value);
        _playerActivePanel.RemoveItemAmountFromInventory(slot, 1);
    }
    private void IncreaseHealth(int value, InventorySlot slot)
    {
        _playerStatsChanger.IncreaseHealth(value);
        _playerActivePanel.RemoveItemAmountFromInventory(slot, 1);
    }

    public void TurnOffFoodDisplay()
    {
        if (_interactiveButton.activeSelf == true)
        {
            _interactiveButton.SetActive(false);
            _interactiveButton.GetComponentInChildren<Button>().onClick.RemoveAllListeners();
        }
    }

    private void DisplayItem(GameObject button, InventorySlot mouseHoverSlotData)
    {
        if (button.activeSelf == true)
        {
            button.SetActive(false);
            button.GetComponentInChildren<Button>().onClick.RemoveAllListeners();
            return;
        }
        button.SetActive(true);
        button.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y + 140, 0);
        button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = mouseHoverSlotData.ItemObject.Name;
        button.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = mouseHoverSlotData.ItemObject.Description;
        
        
    }
}
