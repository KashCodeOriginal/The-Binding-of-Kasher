using UnityEngine;

public class PlantItem : MonoBehaviour
{
    [SerializeField] private ItemsData _woodenSapling;

    [SerializeField] private GameObject _woodPrefab;
    
    [SerializeField] private ItemsData _wheatSeed;

    [SerializeField] private GameObject _wheatPrefab;

    [SerializeField] private GameObject _player;
    
    [SerializeField] private Transform _map;

    public void Plant(ItemsData item)
    {
        if (item == _woodenSapling)
        {
            PlantWood();
        }
        else if (item == _wheatSeed)
        {
            PlanWheat();
        }
    }

    private void PlantWood()
    {
        PlantAnyItem(_woodPrefab);
    }
    private void PlanWheat()
    {
        PlantAnyItem(_wheatPrefab);
    }

    private void PlantAnyItem(GameObject prefab)
    {
        var position = _player.transform.position;
        Instantiate(prefab, new Vector3(position.x, position.y - 2, position.z + 3), Quaternion.identity, _map);
    }
}
