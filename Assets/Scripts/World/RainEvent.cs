using System.Collections;
using UnityEngine;

public class RainEvent : MonoBehaviour
{
    public bool IsRainStarted => _isRainStarted;
    
    [SerializeField] private GameObject _rainParticle;
    
    [SerializeField] private float _rainChance;
    
    [SerializeField] private int _minRainDuration;
    [SerializeField] private int _maxRainDuration;

    [SerializeField] private int _rainPower;
    
    [SerializeField] private int _minRainPower;
    [SerializeField] private int _maxRainPower;

    [SerializeField] private AnimationCurve _rainAppearanceCurve;
    
    private bool _isRainStarted;
    float _totalPassedTime;
    float _rainDuration;
    
    private float _randomvalue;
    private void Start()
    {
        _isRainStarted = false;
        StartCoroutine(RainRandomStart());
    }
    
    private void Update()
    {
        if (_isRainStarted == true)
        {
            _totalPassedTime += Time.deltaTime / _rainDuration;
        }
    }

    private void StartRain()
    {
        _isRainStarted = true;
        _rainDuration = Random.Range(_minRainDuration, _maxRainDuration);
        _rainPower = Random.Range(_minRainPower, _maxRainPower);
        _rainParticle.SetActive(true);
        StartCoroutine(RainTimeCheck());
        StartCoroutine(RainPower(_rainPower));
    }

    private void StopRain()
    {
        _isRainStarted = false;
        _rainDuration = 0;
        _totalPassedTime = 0;
        _rainParticle.SetActive(false);
        StopCoroutine(RainPower(_rainPower));
    }
    
    private IEnumerator RainTimeCheck()
    {
        while (true)
        {
            if (_totalPassedTime >= 1f)
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
                _randomvalue = Random.Range(0f, 1f);

                if (_randomvalue <= _rainChance)
                {
                    StartRain();
                }
            }

            yield return new WaitForSeconds(20f);
        }
    }

    private IEnumerator RainPower(int powerValue)
    {
        while (true)
        {
            if (_totalPassedTime >= 1)
            {
                yield break;
            }
            var emission = _rainParticle.GetComponent<ParticleSystem>().emission;
            emission.rateOverTime = (_rainPower * _rainAppearanceCurve.Evaluate(_totalPassedTime));
            yield return new WaitForSeconds(0.1f);
        }
    }
}
