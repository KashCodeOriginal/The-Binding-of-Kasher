using System.Collections;
using UnityEngine;

public class Lighthouse : MonoBehaviour
{
    [SerializeField] private float _woodBurningTime;

    [SerializeField] private LighthouseLight _lighthouseLight;
    
    [SerializeField] private InventoryObject _lighthouseInventory;

    [SerializeField] private ItemsData _wood;

    private float _currentWoodBurningTime;

    private bool _isWoodBurning;

    private void Start()
    {
        StartCoroutine(LighthouseFillCheck());
    }

    private void Update()
    {
        if (_isWoodBurning == true)
        {
            _currentWoodBurningTime += Time.deltaTime;
            
            if (_currentWoodBurningTime >= _woodBurningTime)
            {
                _isWoodBurning = false;
                _currentWoodBurningTime = 0;
                _lighthouseLight.LightsControll(false);
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
