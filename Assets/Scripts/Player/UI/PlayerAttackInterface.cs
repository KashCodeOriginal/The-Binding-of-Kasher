using UnityEngine;

public class PlayerAttackInterface : MonoBehaviour
{
    [SerializeField] private GameObject _playerAttackInterface;

    public void DisplayPlayerAttackInterface()
    {
        _playerAttackInterface.SetActive(true);
    }
    public void HidePlayerAttackInterface()
    {
        _playerAttackInterface.SetActive(false);
    }
}
