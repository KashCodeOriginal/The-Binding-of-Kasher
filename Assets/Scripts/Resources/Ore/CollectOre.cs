using UnityEngine;

public class CollectOre : MonoBehaviour
{
    [SerializeField] private int _comboClicks;

    [SerializeField] private CircleMove _circleMove;
    
    [SerializeField] private ItemsData _coal;
    [SerializeField] private ItemsData _iron;
    [SerializeField] private ItemsData _copper;
    [SerializeField] private ItemsData _tin;

    [SerializeField] private DropResource _dropResource;

    [SerializeField] private PlayerStatsChanger _playerStatsChanger;

    [SerializeField] private int _spentEnergyByClick;

    [SerializeField] private Animator _playerAnimator;

    public int SpentEnergyByClick => _spentEnergyByClick;

    private void AddComboClick()
    {
        _comboClicks += 1;
        //_playerAnimator.SetTrigger("CollectWoodTrigger");
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
            _dropResource.DropItem(_coal.Data, _comboClicks);
            
            int coalChanceValue = Random.Range(0, 100);
            int ironChanceValue = Random.Range(0, 100);
            int copperChanceValue = Random.Range(0, 100);
            int tinChanceValue = Random.Range(0, 100);

            int oreAmount;
            
            if (coalChanceValue >= 20)
            {
                oreAmount = Random.Range(1, _comboClicks);
                _dropResource.DropItem(_coal.Data, oreAmount);
            }
            if (ironChanceValue <= 50)
            {
                oreAmount = Random.Range(1, _comboClicks);
                _dropResource.DropItem(_iron.Data, oreAmount);
            }
            if (copperChanceValue <= 70)
            {
                oreAmount = Random.Range(1, _comboClicks);
                _dropResource.DropItem(_copper.Data, oreAmount);
            }
            if (tinChanceValue <= 85)
            {
                oreAmount = Random.Range(1, _comboClicks);
                _dropResource.DropItem(_tin.Data, oreAmount);
            }
            _comboClicks = 0;
        }
    }

    private void OnEnable()
    {
        _circleMove.ComboPointAdd += AddComboClick;
        _circleMove.ComboEnded += DropResources;
    }
    private void OnDisable()
    {
        _circleMove.ComboPointAdd -= AddComboClick;
        _circleMove.ComboEnded -= DropResources;
    }
}
