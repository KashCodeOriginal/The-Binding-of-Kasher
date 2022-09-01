using UnityEngine;

public class CargoShipChestDisplay : MonoBehaviour
{
    [SerializeField] private GameObject _cargoShipChestDisplay;

    private void Start()
    {
        HideCargoShipChestInterface();
    }

    public void DisplayCargoShipChestInterface()
    {
        _cargoShipChestDisplay.SetActive(true);
    }
    
    public void HideCargoShipChestInterface()
    {
        _cargoShipChestDisplay.SetActive(false);
    }
}
