using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class RainEvent : MonoBehaviour
{
    public bool IsRainStarted => _isRainStarted;
    
    [SerializeField] private GameObject _rainParticle;
    
    [SerializeField] private float _rainChance;
    [SerializeField] private float _lightningChance;
    
    [SerializeField] private int _minRainDuration;
    [SerializeField] private int _maxRainDuration;
    
    [SerializeField] private int _rainPower;
    [SerializeField] private int _lightningPower;
    
    [SerializeField] private int _minRainPower;
    [SerializeField] private int _maxRainPower;
    
    [SerializeField] private float _minLightningDuration;
    [SerializeField] private float _maxLightningDuration;
    
    [SerializeField] private AnimationCurve _rainAppearanceCurve;
    [SerializeField] private AnimationCurve _lightningCurve;
    
    [SerializeField] private Light _lightning;
    [SerializeField] private int _rainPowerForLightning;
    
    private bool _isRainStarted;
    private bool _isLightningStarted;
    
    private float _passedRainTime;
    private float _rainDuration;
    
    private float _passedLightningTime;
    private float _lightningDuration;
    
    private float _randomRainChance;
    private float _randomLightningChance;
    
    private void Start()
    {
        _isRainStarted = false;
        _isLightningStarted = false;
        StartCoroutine(RainRandomStart());
    }
    
    private void Update()
    {
        if (_isRainStarted == true)
        {
            _passedRainTime += Time.deltaTime / _rainDuration;
        }

        if (_isLightningStarted == true)
        {
            _passedLightningTime += Time.deltaTime / _lightningDuration;

            _lightning.intensity = _lightningCurve.Evaluate(_passedLightningTime) * _lightningPower;

            if (_passedLightningTime >= 1)
            {
                _isLightningStarted = false;
                _passedLightningTime = 0;
            }
        }
    }

    private void StartRain()
    {
        _isRainStarted = true;
        _rainDuration = Random.Range(_minRainDuration, _maxRainDuration);
        _rainPower = Random.Range(_minRainPower, _maxRainPower);

        if (_rainPower >= _rainPowerForLightning)
        {
            StartCoroutine(LightningAppearance());
        }
        
        _rainParticle.SetActive(true);
        StartCoroutine(RainTimeCheck());
        StartCoroutine(RainPower(_rainPower));
    }

    private void StopRain()
    {
        _isRainStarted = false;
        _rainDuration = 0;
        _passedRainTime = 0;
        _rainParticle.SetActive(false);
        StopCoroutine(RainPower(_rainPower));
    }
    
    private void DoLightning()
    {
        _isLightningStarted = true;
        _lightningDuration = Random.Range(_minLightningDuration, _maxLightningDuration);
    }
    
    private IEnumerator RainTimeCheck()
    {
        while (true)
        {
            if (_passedRainTime >= 1f)
            {
                StopRain();
                yield break;
            }
            
            yield return new WaitForSeconds(5f);
        }
    }

    private IEnumerator RainRandomStart()
    {
        while (true)
        {
            if (_isRainStarted == false)
            {
                _randomRainChance = Random.Range(0f, 1f);

                if (_randomRainChance <= _rainChance)
                {
                    StartRain();
                    _randomRainChance = 0;
                }
            }

            yield return new WaitForSeconds(20f);
        }
    }

    private IEnumerator RainPower(int powerValue)
    {
        while (true)
        {
            if (_passedRainTime >= 1)
            {
                yield break;
            }
            var emission = _rainParticle.GetComponent<ParticleSystem>().emission;
            emission.rateOverTime = (_rainPower * _rainAppearanceCurve.Evaluate(_passedRainTime));
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator LightningAppearance()
    {
        while (true)
        {
            if (_passedRainTime >= 1)
            {
                yield break;
            }

            if (_isLightningStarted == false)
            {
                _randomLightningChance = Random.Range(0f, 1f);

                if (_randomLightningChance <= _lightningChance)
                {
                    DoLightning();
                    _randomLightningChance = 0;
                }
            }
            yield return new WaitForSeconds(5f);
        }
    }
}
