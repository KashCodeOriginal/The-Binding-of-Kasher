using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Lighthouse : MonoBehaviour
{
    [SerializeField] private float _woodBurningTime;

    [SerializeField] private LighthouseLight _lighthouseLight;
    
    [SerializeField] private InventoryObject _lighthouseInventory;

    [SerializeField] private ItemsData _wood;

    [SerializeField] private bool _isLighthouseWorking;

    private float _currentWoodBurningTime;

    private bool _isWoodBurning;

    public bool IsLightHouseWorking => _isLighthouseWorking;

    public event UnityAction IsLighthouseIndicatorTurnedOn;
    public event UnityAction IsLighthouseIndicatorTurnedOff;
    
    private void Start()
    {
        StartCoroutine(LighthouseFillCheck());
    }

    private void Update()
    {
        if (_isWoodBurning == true)
        {
            _currentWoodBurningTime += Time.deltaTime;

            _isLighthouseWorking = true;
            
            IsLighthouseIndicatorTurnedOn?.Invoke();
            
            if (_currentWoodBurningTime >= _woodBurningTime)
            {
                _isWoodBurning = false;
                _currentWoodBurningTime = 0;
                _lighthouseLight.LightsControll(false);
                _isLighthouseWorking = false;
                
                IsLighthouseIndicatorTurnedOff?.Invoke();
            }
        }
    }

    private void BurnWood()
    {
        _isWoodBurning = true;
        _lighthouseLight.LightsControll(true);
    }

    private IEnumerator LighthouseFillCheck()
    {
        while (true)
        {
            if (_isWoodBurning == false)
            {
                if (_lighthouseInventory.FindItemInInventory(_wood.Data, 1))
                {
                    BurnWood();
                    _lighthouseInventory.RemoveItemAmountFromInventory(_lighthouseInventory.FindItemInInventory(_wood.Data), 1);
                }
            }
            yield return new WaitForSeconds(10f);
        }
    }
}
