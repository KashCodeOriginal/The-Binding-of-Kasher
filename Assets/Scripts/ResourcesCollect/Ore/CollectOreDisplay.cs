using UnityEngine;

public class CollectOreDisplay : MonoBehaviour
{
    [SerializeField] private GameObject _oreCollectButton;

    [SerializeField] private GameObject _oreCollectInterface;
    
    [SerializeField] private GameObject _startOreCollect;

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
