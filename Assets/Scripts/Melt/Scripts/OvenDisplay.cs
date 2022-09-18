using UnityEngine;

public class OvenDisplay : MonoBehaviour, IInteractable
{
    public bool IsActive { get; private set; } = false;
    
    [SerializeField] private GameObject _ovenInterface;

    public void DisplayOvenInterface()
    {
        _ovenInterface.SetActive(true);
    }
    public void HideOvenInterface()
    {
        _ovenInterface.SetActive(false);
    }

    
    public void Interact()
    {
        if (IsActive == false)
        {
            DisplayOvenInterface();
            IsActive = true;
            return;
        }
        HideOvenInterface();
        IsActive = false;
    }
}
