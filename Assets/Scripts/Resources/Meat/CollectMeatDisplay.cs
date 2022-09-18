using UnityEngine;

public class CollectMeatDisplay : MonoBehaviour, IInteractable
{
    public bool IsActive { get; private set; } = false;
    
    [SerializeField] private GameObject _collectMeatInterface;
    [SerializeField] private GameObject _holdCollectButton;
    
    public void DisplayCollectMeatInterface()
    {
        _collectMeatInterface.SetActive(true);
    }
    public void HideCollectMeatInterface()
    {
        _collectMeatInterface.SetActive(false);
    }
    public void TurnOnHoldCollectButton()
    {
        _holdCollectButton.SetActive(true);
    }
    public void TurnOffHoldCollectButton()
    {
        _holdCollectButton.SetActive(false);
    }
    public void Interact()
    {
        if (IsActive == false)
        {
            DisplayCollectMeatInterface();
            TurnOnHoldCollectButton();
            IsActive = true;
            return;
        }
        HideCollectMeatInterface();
        IsActive = false;
    }
}
