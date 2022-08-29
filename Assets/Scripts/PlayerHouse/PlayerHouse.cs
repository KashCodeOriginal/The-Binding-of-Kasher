using UnityEngine;

public class PlayerHouse : MonoBehaviour
{
    [SerializeField] private PlayerHouseDisplay _playerHouseDisplay;

    public void DisplayHouseInterface()
    {
        _playerHouseDisplay.ShowHouseInterface();
    }
    public void HideHouseInterface()
    {
        _playerHouseDisplay.HideHouseInterface();
    }
}
