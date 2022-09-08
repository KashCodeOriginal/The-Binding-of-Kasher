using System.Collections;
using UnityEngine;

public class CargoShipChestDisplay : MonoBehaviour
{
    [SerializeField] private GameObject _cargoShipChestDisplay;

    private void Start()
    {
        StartCoroutine(Delay());
    }
    
    public void DisplayCargoShipChestInterface()
    {
        _cargoShipChestDisplay.SetActive(true);
    }
    
    public void HideCargoShipChestInterface()
    {
        _cargoShipChestDisplay.SetActive(false);
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.1f);
        HideCargoShipChestInterface();
    }
}
