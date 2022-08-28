using UnityEngine;

public class PlantItem : MonoBehaviour
{
    [SerializeField] private ItemsData _woodenSapling;

    [SerializeField] private GameObject _woodPrefab;

    [SerializeField] private GameObject _player;
    
    [SerializeField] private Transform _map;

    public void Plant(ItemsData item)
    {
        if (item == _woodenSapling)
        {
            PlantWood();
        }
    }

    private void PlantWood()
    {
        var position = _player.transform.position;
        GameObject obj = Instantiate(_woodPrefab, new Vector3(position.x, position.y, position.z + 3), Quaternion.identity, _map);
    }
}
