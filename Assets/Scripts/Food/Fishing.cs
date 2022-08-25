using UnityEngine;

public class Fishing : MonoBehaviour
{
    [SerializeField] private InventoryObject _playerEquipment;

    [SerializeField] private ItemsData _fishingRod;
    
    [SerializeField] private ItemsData _fish;

    [SerializeField] private FishingDisplay _fishingDisplay;

    [SerializeField] private FishAppearance _fishAppearance;

    [SerializeField] private Animator _playerAnimator;

    [SerializeField] private PlayerMovement _playerMovement;

    public ItemsData Fish => _fish;
    
    public void TryToCatchFish()
    {
        if (_playerEquipment.ItemsContainer.Slots[^1].ItemObject == _fishingRod)
        {
            _fishingDisplay.ShowFishingInterface();
        }
        else
        {
            _fishingDisplay.HideFishingInterface();
        }
    }

    public void StartFishing()
    {
        _playerMovement.StopPlayerMovement();
        
        _fishingDisplay.HideFishingInterface();
        _playerAnimator.SetBool("IsFishing", true);
        
        _fishingDisplay.ShowCastFishingRodInterface();
    }

    public void CastFishingRod()
    {
        _playerAnimator.SetBool("IsFishingRodCasted", true);
        _fishingDisplay.HideCastFishingRodInterface();
        _fishAppearance.StartFishAppearance();
    }

    public void ResetPlayerAnimations()
    {
        _playerAnimator.SetBool("IsFishing", false);
        _playerAnimator.SetBool("IsFishingRodCasted", false);
        _playerMovement.StartPlayerMovement();
    }
}
