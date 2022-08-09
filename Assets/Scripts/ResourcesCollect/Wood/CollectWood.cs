using UnityEngine;

public class CollectWood : MonoBehaviour
{
    [SerializeField] private int _comboClicks;

    [SerializeField] private SliderValueChanger _sliderValueChanger;

    [SerializeField] private InventoryObject _playerInventory;

    [SerializeField] private ItemsData _wood;
    [SerializeField] private ItemsData _apple;

    private void AddComboClick()
    {
        _comboClicks += 1;
    }

    private void AddWoodToInventory()
    {
        _playerInventory.AddItemToInventory(_wood, _comboClicks);

        int value = Random.Range(0, 100);

        if (value >= 50)
        {
            _playerInventory.AddItemToInventory(_apple, _comboClicks / 2);
        }
        
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
