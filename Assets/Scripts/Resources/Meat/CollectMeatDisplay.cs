using UnityEngine;

public class CollectMeatDisplay : MonoBehaviour
{
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
}
