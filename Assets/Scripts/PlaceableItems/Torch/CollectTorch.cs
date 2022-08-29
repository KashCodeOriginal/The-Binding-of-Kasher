using UnityEngine;

public class CollectTorch : MonoBehaviour
{
    [SerializeField] private CollectTorchDisplay _collectTorchDisplay;

    [SerializeField] private ItemsData _torch;

    [SerializeField] private GameObject _currentTorch;

    [SerializeField] private DropResource _dropResource;

    public void CollectTorchByPlayer()
    {
        _collectTorchDisplay.HideTorchCollectInterface();
        
        _dropResource.DropItem(_torch.Data, 1);

        if (_currentTorch != null)
        {
            Destroy(_currentTorch);
        }
    }

    public void SetCurrentTorch(GameObject torch)
    {
        _currentTorch = torch;
    }
}
