using UnityEngine;

public class PlaceItem : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    
    [SerializeField] private Transform _map;

    [SerializeField] private ItemsData _torch;
    
    [SerializeField] private GameObject _torchPrefab;

    public void PlaceItemToGround(ItemsData item)
    {
        if (item == _torch)
        {
            PlaceAnyItem(_torchPrefab, 26);
        }
    }
    
    private void PlaceAnyItem(GameObject prefab, float yPos)
    {
        var position = _player.transform.position;
        Instantiate(prefab, new Vector3(position.x, yPos, position.z), Quaternion.identity, _map);
    }
}
