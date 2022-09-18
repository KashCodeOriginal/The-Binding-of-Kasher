using UnityEngine;

public class WaterDisplay : MonoBehaviour, IInteractable
{
    [SerializeField] private Fishing _fishing;
    [SerializeField] private CollectWater _collectWater;
    [SerializeField] private Escape _escape;
    [SerializeField] private CollectWaterDisplay _collectWaterDisplay;
    [SerializeField] private FishingDisplay _fishingDisplay;
    [SerializeField] private EscapeDisplay _escapeDisplay;
        
    public bool IsActive { get; private set; } = false;
    public void Interact()
    {
        if (IsActive == false)
        {
            _collectWater.TryCollectWater();
            _fishing.TryToCatchFish();
            _escape.CheckForItemsInHand();
            IsActive = true;
            return;
        }
        _collectWaterDisplay.HideWaterInterface();
        _fishingDisplay.HideFishingInterface();
        _escapeDisplay.HidePowerBoatEscapingInterface();
        _escapeDisplay.HideWoodenBoatEscapingInterface();
        IsActive = false;
    }
}
