using UnityEngine;
using UnityEngine.Events;

public class PlayerDeathDisplay : MonoBehaviour
{
    [SerializeField] private GameObject _playerDeathDisplay;

    [SerializeField] private PlayerDeath _playerDeath;

    public void DeathDisplayActive()
    {
        if (_playerDeathDisplay.activeSelf == true)
        {
            _playerDeathDisplay.SetActive(false);
            return;
        }
        _playerDeathDisplay.SetActive(true);
    }

    public void RespawnButtonClick()
    {
        _playerDeath.Respawned();
    }
}
