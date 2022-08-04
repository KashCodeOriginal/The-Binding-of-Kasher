using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private InventoryObject _playerInventory;

    public InventoryObject PlayerInventory => _playerInventory;
}
