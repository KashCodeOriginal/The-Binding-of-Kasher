using UnityEngine;

public class CollectWood : MonoBehaviour
{
    [SerializeField] private int _comboClicks;

    [SerializeField] private SliderValueChanger _sliderValueChanger;

    [SerializeField] private InventoryObject _playerInventory;

    [SerializeField] private Item _wood;
    [SerializeField] private Item _apple;

    private void AddComboClick()
    {
        _comboClicks += 1;
    }

    private void AddWoodToInventory()
    {
        if (_comboClicks > 0)
        {
            _playerInventory.AddItemToInventory(_wood, _comboClicks);

            int value = Random.Range(0, 100);

            if (value >= 50)
            {
                if (_comboClicks / 2 > 0)
                {
                    _playerInventory.AddItemToInventory(_apple, _comboClicks / 2);
                }
            }
            
            _comboClicks = 0;
        }
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
