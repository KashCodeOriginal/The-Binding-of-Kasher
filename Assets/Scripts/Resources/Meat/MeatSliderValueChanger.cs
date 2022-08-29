using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MeatSliderValueChanger : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    [SerializeField] private bool _isButtonHeld;

    [SerializeField] private float _sliderValueChangeStep;
    
    [SerializeField] private float _sliderValueChangeSpeed;

    [SerializeField] private SliderPoints _sliderPoints;
    
    [SerializeField] private CollectMeat _collectMeat;

    [SerializeField] private EnergyForActionCheck _energyForActionCheck;

    [SerializeField] private CollectMeatDisplay _collectMeatDisplay;

    [SerializeField] private float _timeBetweenRounds;
    
    [SerializeField] private float _timeToWinRound;

    private bool _isSliderActivated;

    private float _currentRoundTime;

    private float _timeWithoutMoving;
    
    public event UnityAction ComboPointAdd;
    public event UnityAction ComboEnded;
    

    public void StartSliderMoving()
    {
        _isSliderActivated = true;
        _sliderPoints.CreatePoints();

        _collectMeatDisplay.TurnOnHoldCollectButton();
    }
    private void Update()
    {
        if (_isSliderActivated == true)
        {
            if (_isButtonHeld == true)
            {
                _slider.value += _sliderValueChangeStep * _sliderValueChangeSpeed * Time.deltaTime;
            }
            else
            {
                _slider.value -= _sliderValueChangeStep * _sliderValueChangeSpeed * Time.deltaTime;
            }

            CheckSliderValue();
        }
    }
    
    public void OnDown()
    {
        _isButtonHeld = true;
    }
    public void OnUp()
    {
        _isButtonHeld = false;
    }

    private void CheckSliderValue()
    {
        if (_energyForActionCheck.IsPlayerHasEnoughEnergy(_collectMeat.SpentEnergyByHolding) == true)
        {
            if (_slider.value >= _sliderPoints.FirstPoint && _slider.value <= _sliderPoints.SecondPoint)
            {
                _currentRoundTime += Time.deltaTime;
 
                if (_currentRoundTime >= _timeBetweenRounds)
                {
                    _currentRoundTime = 0;
                    _sliderPoints.CreatePoints();
                    _sliderValueChangeSpeed += 2;
                    _timeWithoutMoving = 0;
                    ComboPointAdd?.Invoke();
                }
            }
            else
            {
                _timeWithoutMoving += Time.deltaTime;

                if (_timeWithoutMoving >= _timeToWinRound)
                {
                    _timeWithoutMoving = 0;
                    ComboEnded?.Invoke();
                    _collectMeatDisplay.HideCollectMeatInterface();
                }
                
                _currentRoundTime = 0;
            }
        }
        else
        {
            ComboEnded?.Invoke();
            _collectMeatDisplay.HideCollectMeatInterface();
        }
    }
}
