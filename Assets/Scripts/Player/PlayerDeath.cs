using UnityEngine;
using UnityEngine.Events;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private InventoryObject _playerInventory;
    [SerializeField] private InventoryObject _playerActivePanel;

    [SerializeField] private PlayerDeathDisplay _playerDeathDisplay;

    public event UnityAction PlayerIsRespawned;

    private void Died()
    {
        _playerDeathDisplay.DeathDisplayActive();
        
        _player.GetComponentInChildren<Animator>().SetBool("Died", true);
        _playerInventory.DropAllItemsFromInventory();
        _playerActivePanel.DropAllItemsFromInventory();
        
        _player.GetComponent<CapsuleCollider>().enabled = false;
        _player.GetComponent<Rigidbody>().useGravity = false;
    }

    public void Respawned()
    {
        _playerDeathDisplay.DeathDisplayActive();
        
        _player.SpawnPlayer();
        
        _player.GetComponentInChildren<Animator>().SetBool("Died", false);

        _playerMovement.SetDefaultSpeed();
        
        PlayerIsRespawned?.Invoke();
    }

    private void OnEnable()
    {
        _player.PlayerDied += Died;
    }
    private void OnDisable()
    {
        _player.PlayerDied -= Died;
    }
}
