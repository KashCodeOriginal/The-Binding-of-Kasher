using System.Collections;
using UnityEngine;

public class CargoShipChestDisplay : MonoBehaviour, IInteractable
{
    public bool IsActive { get; private set; } = false;
    
    [SerializeField] private GameObject _cargoShipChestDisplay;

    private void Start()
    {
        StartCoroutine(Delay());
    }
    
    public void Interact()
    {
        if (IsActive == false)
        {
            DisplayCargoShipChestInterface();
            IsActive = true;
            return;
        }
        HideCargoShipChestInterface();
        IsActive = false;
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
