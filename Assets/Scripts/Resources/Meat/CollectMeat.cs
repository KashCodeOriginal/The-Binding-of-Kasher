using UnityEngine;

public class CollectMeat : MonoBehaviour
{
    [SerializeField] private int _comboClicks;
    
    [SerializeField] private int _spentEnergyByHolding;

    [SerializeField] private ItemsData _rawMeat;
    [SerializeField] private ItemsData _manure;
    
    [SerializeField] private DropResource _dropResource;
    
    [SerializeField] private PlayerStatsChanger _playerStatsChanger;

    [SerializeField] private MeatSliderValueChanger _sliderValueChanger;

    [SerializeField] private CollectMeatDisplay _collectMeatDisplay;
    
    //[SerializeField] private Animator _playerAnimator;

    public int SpentEnergyByHolding => _spentEnergyByHolding;
    
    private void AddComboClick()
    {
        _comboClicks += 1;
        //_playerAnimator.SetTrigger("CollectWoodTrigger");
        DecreaseEnergy();
    }
    
    private void DecreaseEnergy()
    {
        _playerStatsChanger.DecreaseEnergyByAction(_spentEnergyByHolding);
    }
    
    private void DropResources()
    {
        if (_comboClicks > 0)
        {
            _dropResource.DropItem(_rawMeat.Data, Random.Range(1, _comboClicks));
            _dropResource.DropItem(_manure.Data, Random.Range(1, _comboClicks));
            _comboClicks = 0;
        }
    }

    private void OnEnable()
    {
        _sliderValueChanger.ComboPointAdd += AddComboClick;
        _sliderValueChanger.ComboEnded += DropResources;
    }
    private void OnDisable()
    {
        _sliderValueChanger.ComboPointAdd -= AddComboClick;
        _sliderValueChanger.ComboEnded -= DropResources;
    }
}
