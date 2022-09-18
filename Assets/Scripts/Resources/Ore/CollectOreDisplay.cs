using UnityEngine;

public class CollectOreDisplay : MonoBehaviour, IInteractable
{
    public bool IsActive { get; private set; } = false;
    
    [SerializeField] private GameObject _oreCollectButton;

    [SerializeField] private GameObject _oreCollectInterface;
    
    [SerializeField] private GameObject _startOreCollect;
    
    public void Interact()
    {
        if (IsActive == false)
        {
            CollectOreInterfaceActive(true);
            StartCollectOreButton(true);
            IsActive = true;
            return;
        }
        CollectOreInterfaceActive(false);
        StartCollectOreButton(false);
        IsActive = false;
    }
    
    public void StartCollectOreButton(bool isActivated)
    {
        _startOreCollect.SetActive(isActivated);
    }

    public void CollectOreButtonActive(bool isActivated)
    {
        _oreCollectButton.SetActive(isActivated);
    }

    public void CollectOreInterfaceActive(bool isActivated)
    {
        _oreCollectInterface.SetActive(isActivated);
    }

    
}
