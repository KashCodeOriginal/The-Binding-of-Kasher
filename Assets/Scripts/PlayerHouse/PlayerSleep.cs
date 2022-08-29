using UnityEngine;
using UnityEngine.Events;

public class PlayerSleep : MonoBehaviour
{
    [SerializeField] private DayAndNightCycle _dayAndNightCycle;

    [SerializeField] private PlayerHouseDisplay _playerHouseDisplay;

    public event UnityAction PlayerStartsSleep;

    public void TrySleep()
    {
        if (_dayAndNightCycle.CurrentDayPart == DayAndNightCycle.DayPart.Night)
        {
            PlayerStartsSleep?.Invoke();
            _playerHouseDisplay.HideHouseInterface();
        }
    }
}
