using UnityEngine;

public class CollectWood : MonoBehaviour
{
    [SerializeField] private InventoryObject _playerInventory;
    [SerializeField] private ItemsData _wood;

    [SerializeField] private int _amount;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            _playerInventory.AddItemToInventory(_wood, _amount);
        }
    }
}
