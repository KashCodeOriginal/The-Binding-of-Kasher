using UnityEngine;

public class CargoShipItems : MonoBehaviour
{
    [SerializeField] private InventoryObject _shipChest;

    [SerializeField] private ItemsDataBase _itemsDataBase;

    public void FillChestWithRandomItems()
    {
        for (int i = 0; i < _shipChest.GetSlots.Length; i++)
        {
            var randomItemID = Random.Range(0, _itemsDataBase.ItemsData.Length);

            if (_itemsDataBase.ItemsData[randomItemID].Stackable == false)
            {
                _shipChest.AddItemToInventory(_itemsDataBase.ItemsData[randomItemID].Data, 1);
            }
            else
            {
                _shipChest.AddItemToInventory(_itemsDataBase.ItemsData[randomItemID].Data, Random.Range(5, 10));
            }
        }
    }

    public void ClearCargoChest()
    {
        _shipChest.ItemsContainer.ClearItems();
    }
}
