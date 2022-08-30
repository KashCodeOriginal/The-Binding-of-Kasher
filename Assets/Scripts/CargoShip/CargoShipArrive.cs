using System.Collections;
using UnityEngine;

public class CargoShipArrive : MonoBehaviour
{
    [SerializeField] private float _timeBetweenChances;
    
    [SerializeField] private float _arrivingChance;

    [SerializeField] private bool _isShipArrived;

    [SerializeField] private CargoShipAnimator _shipAnimator;

    [SerializeField] private DayAndNightCycle _dayAndNightCycle;

    [SerializeField] private CargoShipItems _cargoShipItems;

    [SerializeField] private CargoshipSail _cargoshipSail;

    [SerializeField] private float _timeBeforeShipSail;
    
    [SerializeField] private GameObject _ship;

    private float _currentArrivingTime;

    private void Start()
    {
        _isShipArrived = false;
        _shipAnimator.StopAnimations();
        StartCoroutine(CargoShipArriveChance());
    }

    private void Update()
    {
        if (_isShipArrived == true)
        {
            _currentArrivingTime += Time.deltaTime;

            if (_currentArrivingTime >= _timeBeforeShipSail)
            {
                _cargoshipSail.Sail();
                _isShipArrived = false;
            }
        }
    }
    
    [SerializeField] private Lighthouse _lighthouse;

    private void TryToArrive()
    {
        if (_lighthouse.IsLightHouseWorking == true && _isShipArrived == false)
        {
            ShipArrive();
        }
    }

    private void ShipArrive()
    {
        _ship.transform.position = new Vector3(283.3f, 22, -37.6f);
        _cargoShipItems.FillChestWithRandomItems();
        _shipAnimator.StartShipArriving();
        _isShipArrived = true;
    }

    private IEnumerator CargoShipArriveChance()
    {
        while (true)
        {
            if (_isShipArrived == false && _dayAndNightCycle.CurrentDayPart == DayAndNightCycle.DayPart.Evening || _dayAndNightCycle.CurrentDayPart == DayAndNightCycle.DayPart.Night)
            {
                var randomChance = Random.Range(0, 100);

                if (randomChance <= _arrivingChance)
                {
                    TryToArrive();
                }
            }
        
            yield return new WaitForSeconds(_timeBetweenChances);
        }
    }
}
