using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class DayAndNightCycle : MonoBehaviour
{
    [Range(0, 1)] [SerializeField] private float _dayTime;
    [SerializeField] private float _dayDuration;
    [SerializeField] private int _totalDaysPassed;

    [SerializeField] private int _morningStartHour;
    [SerializeField] private int _dayStartHour;
    [SerializeField] private int _eveningStartHour;
    [SerializeField] private int _nightStartHour;

    [SerializeField] private AnimationCurve _sunCurve;
    [SerializeField] private AnimationCurve _moonCurve;
    [SerializeField] private AnimationCurve _skyboxCurve;

    [SerializeField] private float _updateDelay;

    [SerializeField] private RainEvent _rainEvent;
    [SerializeField] private Material _rainyDaySkybox;
    
    [SerializeField] private Material _nightSkybox;

    [SerializeField] private Material _currentDaySkybox;

    [SerializeField] private Light _sun;
    [SerializeField] private Light _moon;

    [SerializeField] private float _timeMultiplier;
    
    [SerializeField] private float _startHour;

    [SerializeField] private PlayerSleep _playerSleep;

    private DateTime _currentTime;

    private TimeSpan _newDayTime;

    private bool _newDayAdded;

    private float _sunIntensity;
    private float _moonIntensity;
    
    private enum DayPart
    {
        Morning,
        Day,
        Evening,
        Night
    }

    private string _currentDayPart;

    public DateTime CurrentTime => _currentTime;
    
    public event UnityAction<DateTime> TimeWasChanged;
    public event UnityAction<int> PassedDaysAmountChanged;
    
    public event UnityAction<string> CurrentDayPartChanged;
    
    private void Start()
    {
        _sunIntensity = _sun.intensity;
        _moonIntensity = _moon.intensity;
        
        _currentTime = DateTime.Now.Date + TimeSpan.FromHours(_startHour);
        
        _timeMultiplier = (60 * 60) / (_dayDuration / 24); //1 hour for second in real time
        
        _newDayTime = TimeSpan.FromHours(0);
        
        StartCoroutine(Delay());

        _currentDayPart = DayPart.Morning.ToString();

        _newDayAdded = false;
    }

    private void Update()
    {
        _dayTime += Time.deltaTime / _dayDuration;
        
        if (_dayTime >= 1)
        {
            _dayTime = 0;
            _newDayAdded = false;
        }
        
        _sun.transform.localRotation = Quaternion.Euler(_dayTime * 360f, 0, 0);
        _moon.transform.localRotation = Quaternion.Euler(_dayTime * 360f + 180f, 0, 0);
        
        _currentTime = _currentTime.AddSeconds(Time.deltaTime  * _timeMultiplier);
        
        TimeWasChanged?.Invoke(_currentTime);

        if (_currentTime.Hour == _newDayTime.Hours && _newDayAdded == false)
        {
            _totalDaysPassed++;
            PassedDaysAmountChanged?.Invoke(_totalDaysPassed);
            _newDayAdded = true;
        }
        
        CheckDayPart(_morningStartHour, _dayStartHour, DayPart.Morning);
        CheckDayPart(_dayStartHour, _eveningStartHour, DayPart.Day);
        CheckDayPart(_eveningStartHour, _nightStartHour, DayPart.Evening);
        CheckDayPart(_nightStartHour, _morningStartHour, DayPart.Night);
    }

    private IEnumerator Delay()
    {
        while (true)
        {
            if (_dayTime >= 0.45f)
            {
                _moon.enabled = true;
            }
            else
            {
                _moon.enabled = false;
            }

            if (_rainEvent.IsRainStarted == true)
            {
                _currentDaySkybox = _rainyDaySkybox;
            }
            RenderSettings.skybox.Lerp(_nightSkybox, _currentDaySkybox, _skyboxCurve.Evaluate(_dayTime));
            RenderSettings.sun = _skyboxCurve.Evaluate(_dayTime) > 0.1f ? _sun : _moon;
            DynamicGI.UpdateEnvironment();
            _sun.intensity = _sunIntensity * _sunCurve.Evaluate(_dayTime);
            _moon.intensity = _moonIntensity * _moonCurve.Evaluate(_dayTime);
            yield return new WaitForSeconds(_updateDelay);
        }
    }

    private void SetNewDay()
    {
        _dayTime = 0;
        _currentTime = DateTime.Now.Date + TimeSpan.FromHours(0);
        _currentTime = DateTime.Now.Date + TimeSpan.FromHours(_startHour);
    }

    private void OnEnable()
    {
        _playerSleep.PlayerStartsSleep += SetNewDay;
    }
    private void OnDisable()
    {
        _playerSleep.PlayerStartsSleep -= SetNewDay;
    }

    private void CheckDayPart(int firstTimeBorder, int secondTimeBorder, DayPart dayPart)
    {
        if (dayPart == DayPart.Night)
        {
            if (_currentTime.Hour >= firstTimeBorder)
            {
                _currentDayPart = dayPart.ToString();
                CurrentDayPartChanged?.Invoke(_currentDayPart);
                return;
            }
        }
        
        if(_currentTime.Hour >= firstTimeBorder && _currentTime.Hour < secondTimeBorder && _currentDayPart != dayPart.ToString())
        {
            _currentDayPart = dayPart.ToString();
            CurrentDayPartChanged?.Invoke(_currentDayPart);
        }
    }
}

