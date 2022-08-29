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
        PlantAnyItem(_woodPrefab, 25.5f);
    }
    private void PlanWheat()
    {
        PlantAnyItem(_wheatPrefab, 25);
    }

    private void PlantAnyItem(GameObject prefab, float yPos)
    {
        var position = _player.transform.position;
        Instantiate(prefab, new Vector3(position.x, yPos, position.z), Quaternion.identity, _map);
    }
}
