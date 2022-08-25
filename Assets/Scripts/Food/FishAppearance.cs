using UnityEngine;

public class FishAppearance : MonoBehaviour
{
    [SerializeField] private float _minFishAppearanceTime;
    [SerializeField] private float _maxFishAppearanceTime;
    
    [SerializeField] private float _minTimeToCatchFish;
    [SerializeField] private float _maxTimeToCatchFish;

    [SerializeField] private FishingDisplay _fishingDisplay;

    [SerializeField] private Fishing _fishing;

    [SerializeField] private DropResource _dropResource;

    private float _randomFishAppearanceTime;

    private float _randomFishCatchingTime;
    
    private float _currentFishCatchingTime;

    private float _currentFishAppearedTime;

    private bool _isFishAppeared;

    private bool _isFishCaught;

    private bool _isFishCaughtByClick;

    private void Start()
    {
        _isFishAppeared = false;
    }
    private void Update()
    {
        if (_isFishAppeared == true)
        {
            _currentFishAppearedTime += Time.deltaTime;

            if (_currentFishAppearedTime >= _randomFishAppearanceTime)
            {
                Debug.Log("Попалась на крючок");
                _currentFishAppearedTime = 0;
                _isFishAppeared = false;
                _isFishCaught = true;
            }
        }

        if (_isFishCaught == true)
        {
            if (_isFishCaughtByClick == true)
            {
                _fishing.ResetPlayerAnimations();
                _dropResource.DropItem(_fishing.Fish.Data, 1);
                _isFishCaughtByClick = false;
                _isFishCaught = false;
                _currentFishCatchingTime = 0;
                _fishingDisplay.HideFishingInterface();
                _fishingDisplay.TurnOffFishCatchingButton();
                return; 
            }
            
            _currentFishCatchingTime += Time.deltaTime;
            
            _fishingDisplay.TurnOnFishCatchingButton();

            if (_currentFishCatchingTime >= _randomFishCatchingTime)
            {
                _fishing.ResetPlayerAnimations();
                _fishingDisplay.HideFishingInterface();
                _fishingDisplay.TurnOffFishCatchingButton();
                Debug.Log("Уплыла");
                _isFishCaught = false;
                _currentFishCatchingTime = 0;
            }
        }
    }
    public void StartFishAppearance()
    {
        _randomFishAppearanceTime = Random.Range(_minFishAppearanceTime, _maxFishAppearanceTime);

        _randomFishCatchingTime = Random.Range(_minTimeToCatchFish, _maxTimeToCatchFish);
        
        _isFishAppeared = true;
    }

    public void CatchFish()
    {
        _isFishCaughtByClick = true;
    }
}
