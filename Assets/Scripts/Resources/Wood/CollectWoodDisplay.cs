using UnityEngine;

public class CollectWoodDisplay : MonoBehaviour
{

    [SerializeField] private GameObject _woodCollectButton;

    [SerializeField] private GameObject _woodCollectInterface;
    
    [SerializeField] private GameObject _startWoodCollectButton;

    public void StartCollectWoodButton(bool isActivated)
    {
        _startWoodCollectButton.SetActive(isActivated);
    }

    public void CollectWoodButtonActive(bool isActivated)
    {
        _woodCollectButton.SetActive(isActivated);
    }

    public void CollectWoodInterfaceActive(bool isActivated)
    {
        _woodCollectInterface.SetActive(isActivated);
    }

}
