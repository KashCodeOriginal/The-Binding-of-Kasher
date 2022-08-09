using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStatsChanger : MonoBehaviour
{
    [SerializeField] private Player _player;
    
    [SerializeField] private int _healthDecreaseStep;
    [SerializeField] private int _timeBetweenHealthSteps;

    [SerializeField] private int _hungerDecreaseStep;
    [SerializeField] private int _timeBetweenHungerSteps;
    
    [SerializeField] private int _waterDecreaseStep;
    [SerializeField] private int _timeBetweenWaterSteps;
    
    public event UnityAction<int> HealthIsIncreased;
    public event UnityAction<int> HealthIsDecreased;
    
    public event UnityAction<int> HungerIsIncreased;
    public event UnityAction<int> HungerIsDecreased;

    public event UnityAction<int> WaterIsIncreased;
    public event UnityAction<int> WaterIsDecreased;

    public event UnityAction PlayerIsDied;
    
    private void Start()
    {
        StartCoroutine(DecreaseHunger());
        StartCoroutine(DecreaseWater());
        StartCoroutine(CheckStats());
    }

    private IEnumerator DecreaseHunger()
    {
        while(_player.HungerPoint > 0)
        {
            HungerIsDecreased?.Invoke(_hungerDecreaseStep);
            yield return new WaitForSeconds(_timeBetweenHungerSteps);
        }
    }
    private IEnumerator DecreaseWater()
    {
        while(_player.WaterPoint > 0)
        {
            WaterIsDecreased?.Invoke(_waterDecreaseStep);
            yield return new WaitForSeconds(_timeBetweenWaterSteps);
        }
    }

    private IEnumerator CheckStats()
    {
        while (_player.HealthPoint > 0)
        {
            while ((_player.WaterPoint == 0 || _player.HungerPoint == 0) && _player.HealthPoint > 0)
            {
                HealthIsDecreased(_healthDecreaseStep);
                yield return new WaitForSeconds(_timeBetweenHealthSteps);
            }
            yield return new WaitForSeconds(5f);
        }
    }
}
