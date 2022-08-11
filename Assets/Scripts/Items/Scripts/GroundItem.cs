using UnityEngine;

public class GroundItem : MonoBehaviour
{
    [SerializeField] private ItemsData _item;
    public ItemsData Item => _item;
}
