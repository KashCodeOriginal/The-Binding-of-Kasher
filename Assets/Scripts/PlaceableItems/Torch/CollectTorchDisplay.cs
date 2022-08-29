using UnityEngine;

public class CollectTorchDisplay : MonoBehaviour
{
    [SerializeField] private GameObject _torchCollectInterface;

    public void DisplayTorchCollectInterface()
    {
        _torchCollectInterface.SetActive(true);
    }
    public void HideTorchCollectInterface()
    {
        _torchCollectInterface.SetActive(false);
    }
}
