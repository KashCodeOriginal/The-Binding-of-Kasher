using UnityEngine;

public class CollectWood : MonoBehaviour
{
    [SerializeField] private int _comboClicks;

    [SerializeField] private SliderValueChanger _sliderValueChanger;

    [SerializeField] private InventoryObject _playerInventory;

    [SerializeField] private ItemsData _wood;

    private void AddComboClick()
    {
        _comboClicks += 1;
    }

    private void AddWoodToInventory()
    {
        _playerInventory.AddItemToInventory(_wood, _comboClicks);
        _comboClicks = 0;
    }

    private void OnEnable()
    {
        _sliderValueChanger.ComboPointAdd += AddComboClick;
        _sliderValueChanger.ComboEnded += AddWoodToInventory;
    }
    private void OnDisable()
    {
        _sliderValueChanger.ComboPointAdd -= AddComboClick;
        _sliderValueChanger.ComboEnded -= AddWoodToInventory;
    }
}
