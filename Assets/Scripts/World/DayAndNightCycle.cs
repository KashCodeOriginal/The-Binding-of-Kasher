using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DayAndNightCycle : MonoBehaviour
{
    [Range(0, 1)] [SerializeField] private float _dayTime;
    [SerializeField] private float _dayDuration;
    [SerializeField] private int _totalDaysPassed;

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

    [SerializeField] private TextMeshProUGUI _dayClockText;
    
    [SerializeField] private TextMeshProUGUI _totalDaysPassedText;

    [SerializeField] private float _timeMultiplier;
    
    [SerializeField] private float _startHour; 

    private DateTime _currentTime;

    private TimeSpan _newDayTime;

    private bool _newDayAdded;

    private float _sunIntensity;
    private float _moonIntensity;
    
    private void Start()
    {
        _sunIntensity = _sun.intensity;
        _moonIntensity = _moon.intensity;
        
        _currentTime = DateTime.Now.Date + TimeSpan.FromHours(_startHour);
        
        _timeMultiplier = (60 * 60) / (_dayDuration / 24); //1 hour for second in real time
        
        _newDayTime = TimeSpan.FromHours(0);
        
        StartCoroutine(Delay());

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
        
        if (_dayClockText != null)
        {
            _dayClockText.text = _currentTime.ToString("HH:mm");
        }

        if (_currentTime.Hour == _newDayTime.Hours && _newDayAdded == false)
        {
            _totalDaysPassed++;
            _totalDaysPassedText.text = "Total Days Passed: " + _totalDaysPassed;
            _newDayAdded = true;
        }
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
}

