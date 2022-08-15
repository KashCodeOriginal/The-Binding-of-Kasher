using UnityEngine;

public class CollectWood : MonoBehaviour
{
    [SerializeField] private int _comboClicks;

    [SerializeField] private SliderValueChanger _sliderValueChanger;

    [SerializeField] private InventoryObject _playerInventory;

    [SerializeField] private ItemsData _wood;
    [SerializeField] private ItemsData _apple;

    [SerializeField] private DropResource _dropResource;

    private void AddComboClick()
    {
        _comboClicks += 1;
    }

    private void DropWood()
    {
        if (_comboClicks > 0)
        {
            _dropResource.DropItem(_wood.Data, _comboClicks);

            int value = Random.Range(0, 100);
            if (value >= 50)
            {
                if (_comboClicks / 2 > 0)
                {
                    _dropResource.DropItem(_apple.Data, _comboClicks / 2);
                }
            }
            
            _comboClicks = 0;
        }
    }

    private void OnEnable()
    {
        _sliderValueChanger.ComboPointAdd += AddComboClick;
        _sliderValueChanger.ComboEnded += DropWood;
    }
    private void OnDisable()
    {
        _sliderValueChanger.ComboPointAdd -= AddComboClick;
        _sliderValueChanger.ComboEnded -= DropWood;
    }
    
}
