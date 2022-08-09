using System.Collections;
using UnityEngine;

public class DayAndNightCycle : MonoBehaviour
{
    [Range(0, 1)]
    [SerializeField] private float _dayTime;
    [SerializeField] private float _dayDuration;

    [SerializeField] private AnimationCurve _sunCurve;
    [SerializeField] private AnimationCurve _moonCurve;
    [SerializeField] private AnimationCurve _skyboxCurve;

    [SerializeField] private Material _daySkybox;
    [SerializeField] private Material _nightSkybox;
    
    [SerializeField] private Light _sun;
    [SerializeField] private Light _moon;

    private bool _isCoroutineStarted;

    private float _sunIntensity;
    private float _moonIntensity;

    private void Start()
    {
        _sunIntensity = _sun.intensity;
        _moonIntensity = _moon.intensity;
        _isCoroutineStarted = false;
    }

    private void Update()
    {
        _dayTime += Time.deltaTime / _dayDuration;
        if (_dayTime >= 1)
        {
            _dayTime = 0;
        }
        _sun.transform.localRotation = Quaternion.Euler(_dayTime * 360f, 0, 0);
        _moon.transform.localRotation = Quaternion.Euler(_dayTime * 360f + 180f, 0, 0);

        StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
        if (_isCoroutineStarted == false)
        {
            while (true)
            {
                if (_dayTime >= 0.5f)
                {
                    _moon.enabled = true;
                }
                else
                {
                    _moon.enabled = false;
                }

                RenderSettings.skybox.Lerp(_nightSkybox, _daySkybox, _skyboxCurve.Evaluate(_dayTime));
                RenderSettings.sun = _skyboxCurve.Evaluate(_dayTime) > 0.1f ? _sun : _moon;
                DynamicGI.UpdateEnvironment();
                _sun.intensity = _sunIntensity * _sunCurve.Evaluate(_dayTime);
                _moon.intensity = _moonIntensity * _moonCurve.Evaluate(_dayTime);
                yield return new WaitForSeconds(0.5f);
                _isCoroutineStarted = true;
            }
        }
    }
}