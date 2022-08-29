using UnityEngine;

public class CollectWheatDisplay : MonoBehaviour
{
    [SerializeField] private GameObject _collectWheatDisplay;

    public void DisplayCollectWheatInterface()
    {
        _collectWheatDisplay.SetActive(true);
    }
    public void HideCollectWheatInterface()
    {
        _collectWheatDisplay.SetActive(false);
    }
}
