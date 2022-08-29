using UnityEngine;

public class PlayerHouseDisplay : MonoBehaviour
{
    [SerializeField] private GameObject _houseInterface;

    public void DisplayHouseInterface()
    {
        _houseInterface.SetActive(true);
    }
    public void HideHouseInterface()
    {
        _houseInterface.SetActive(false);
    }
}
