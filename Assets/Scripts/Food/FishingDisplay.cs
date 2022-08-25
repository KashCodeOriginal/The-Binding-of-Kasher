using UnityEngine;

public class FishingDisplay : MonoBehaviour
{
    [SerializeField] private GameObject _fishingInterface;
    
    [SerializeField] private GameObject _castFishingRodInterface;
    
    [SerializeField] private GameObject _catchFishButton;

    public void ShowFishingInterface()
    {
        _fishingInterface.SetActive(true);
    }
    public void HideFishingInterface()
    {
        _fishingInterface.SetActive(false);
    }
    public void ShowCastFishingRodInterface()
    {
        _castFishingRodInterface.SetActive(true);
    }
    public void HideCastFishingRodInterface()
    {
        _castFishingRodInterface.SetActive(false);
    }
    public void TurnOnFishCatchingButton()
    {
        _catchFishButton.SetActive(true);
    }
    public void TurnOffFishCatchingButton()
    {
        _catchFishButton.SetActive(false);
    }
}
