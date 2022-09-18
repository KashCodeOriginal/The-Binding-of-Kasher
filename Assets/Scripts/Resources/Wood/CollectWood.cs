using UnityEngine;

public class CollectWood : MonoBehaviour
{
    [SerializeField] private int _comboClicks;

    [SerializeField] private SliderValueChanger _sliderValueChanger;

    [SerializeField] private ItemsData _wood;
    [SerializeField] private ItemsData _apple;
    [SerializeField] private ItemsData _web;
    [SerializeField] private ItemsData _sapling;

    [SerializeField] private DropResource _dropResource;

    [SerializeField] private GameObject _currentCollectingTree;

    [SerializeField] private PlayerStatsChanger _playerStatsChanger;

    [SerializeField] private int _spentEnergyByClick;

    [SerializeField] private Animator _playerAnimator;
    
    public int SpentEnergyByClick => _spentEnergyByClick;

    private void AddComboClick()
    {
        _comboClicks += 1;
        _playerAnimator.SetTrigger("CollectWoodTrigger");
        DecreaseEnergy();
    }

    private void DecreaseEnergy()
    {
        _playerStatsChanger.DecreaseEnergyByAction(_spentEnergyByClick);
    }

    private void DropResources()
    {
        if (_comboClicks > 0)
        {
            _dropResource.DropItem(_wood.Data, _comboClicks);

            int appleChanceValue = Random.Range(0, 100);
            int webChanceValue = Random.Range(0, 100);
            int sapplingAmount = Random.Range(1, 3);
            
            if (appleChanceValue >= 50)
            {
                if (_comboClicks / 2 > 0)
                {
                    _dropResource.DropItem(_apple.Data, _comboClicks / 2);
                }
            }
            if (webChanceValue <= 35)
            {
                _dropResource.DropItem(_web.Data, _comboClicks);
            }
            
            _dropResource.DropItem(_sapling.Data, sapplingAmount);
            
            _comboClicks = 0;
            
            Destroy(_currentCollectingTree);
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

    public void SetCurrentTree(GameObject tree)
    {
        _currentCollectingTree = tree;
    }
}
