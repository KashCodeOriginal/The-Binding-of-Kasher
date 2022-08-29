using UnityEngine;

public class CollectWheat : MonoBehaviour
{
    [SerializeField] private ItemsData _wheat;
    [SerializeField] private DropResource _dropResource;

    [SerializeField] private CollectWheatDisplay _collectWheatDisplay;

    public void CollectWheatByPlayer()
    {
        _dropResource.DropItem(_wheat.Data, Random.Range(1,3));
        _collectWheatDisplay.HideCollectWheatInterface();
    }
}
