using UnityEngine;

public class CollectWheat : MonoBehaviour
{
    [SerializeField] private ItemsData _wheat;
    [SerializeField] private DropResource _dropResource;

    [SerializeField] private CollectWheatDisplay _collectWheatDisplay;

    private GameObject _currentWheat;

    public void CollectWheatByPlayer()
    {
        _dropResource.DropItem(_wheat.Data, Random.Range(1,3));
        _collectWheatDisplay.HideCollectWheatInterface();

        if (_currentWheat != null)
        {
            Destroy(_currentWheat);
        }
    }

    public void SetCurrentWheat(GameObject currentWheat)
    {
        _currentWheat = currentWheat;
    }
}
