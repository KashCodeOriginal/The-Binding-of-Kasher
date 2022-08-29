using UnityEngine;
using UnityEngine.Events;

public class PlayerSleep : MonoBehaviour
{
    [SerializeField] private DayAndNightCycle _dayAndNightCycle;

    public event UnityAction PlayerStartsSleep;

    public void TrySleep()
    {
        if (_dayAndNightCycle.CurrentTime.Hour >= 22)
        {
            PlayerStartsSleep?.Invoke();
        }
    }
}
