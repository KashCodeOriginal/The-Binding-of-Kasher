using UnityEngine;

public class EscapeDisplay : MonoBehaviour
{
    [SerializeField] private GameObject _escapeByWoodenBoatInterface;
    [SerializeField] private GameObject _escapeByPowerBoatInterface;
    [SerializeField] private GameObject _successfulEscapingInterface;

    public void DisplayWoodenBoatEscapingInterface()
    {
        _escapeByWoodenBoatInterface.SetActive(true);
    }
    public void HideWoodenBoatEscapingInterface()
    {
        _escapeByWoodenBoatInterface.SetActive(false);
    }
    public void DisplayPowerBoatEscapingInterface()
    {
        _escapeByPowerBoatInterface.SetActive(true);
    }
    public void HidePowerBoatEscapingInterface()
    {
        _escapeByPowerBoatInterface.SetActive(false);
    }

    public void ShowSuccessfulEscapingInterface()
    {
        _successfulEscapingInterface.SetActive(true);
    }
}
