using UnityEngine;

public class PlayerTriggers : MonoBehaviour
{
    [SerializeField] private CollectWoodDisplay _collectWoodDisplay;
    [SerializeField] private CollectWood _collectWood;
    [SerializeField] private CollectWheatDisplay _collectWheatDisplay;
    [SerializeField] private CollectTorchDisplay _collectTorchDisplay;
    [SerializeField] private CollectTorch _collectTorch;
    [SerializeField] private PlayerAttack _playerAttack;
    [SerializeField] private PlayerAttackInterface _playerAttackInterface;
    
    [SerializeField] private InventoryObject _playerInventory;
    [SerializeField] private InventoryObject _playerActivePanel;
    
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out IInteractable interactable) == true)
        {
            interactable.Interact();
        }

        if (collider.TryGetComponent(out GroundItem groundItem) == true)
        {
            GroundItem item = groundItem;
            if(_playerActivePanel.AddItemToInventory(item.Item.Data, item.Amount) == true)
            {
                Destroy(collider.gameObject);
                return;
            }
            if (_playerInventory.AddItemToInventory(item.Item.Data, item.Amount))
            {
                Destroy(collider.gameObject);
            }
        }
        
        if (collider.CompareTag("Wheat"))
        {
            _collectWheatDisplay.DisplayCollectWheatInterface();
        }
        if (collider.CompareTag("Torch"))
        {
            _collectTorchDisplay.DisplayTorchCollectInterface();
            _collectTorch.SetCurrentTorch(collider.gameObject);
        }

        if (collider.CompareTag("Tree"))
        {
            _collectWoodDisplay.StartCollectWoodButton(true);
            _collectWoodDisplay.CollectWoodInterfaceActive(true);
            _collectWood.SetCurrentTree(collider.gameObject);
            
        }
        if (collider.CompareTag("Enemy"))
        {
            _playerAttack.SetCurrentEnemy(collider.gameObject);
            _playerAttackInterface.DisplayPlayerAttackInterface();
        }
    }
    private void OnTriggerExit(Collider collider)
    {
        if (collider.TryGetComponent(out IInteractable interactable) == true)
        {
            interactable.Interact();
        }
        
        if (collider.CompareTag("Wheat"))
        {
            _collectWheatDisplay.HideCollectWheatInterface();
        }
        if (collider.CompareTag("Torch"))
        {
            _collectTorchDisplay.HideTorchCollectInterface();
            _collectTorch.SetCurrentTorch(null);
        }
        if (collider.CompareTag("Tree"))
        {
            _collectWoodDisplay.StartCollectWoodButton(false);
            _collectWoodDisplay.CollectWoodInterfaceActive(false);
            _collectWood.SetCurrentTree(null);
        }
        if (collider.CompareTag("Enemy"))
        {
            _playerAttackInterface.HidePlayerAttackInterface();
            _playerAttack.SetCurrentEnemy(null);
        }
    }
}
