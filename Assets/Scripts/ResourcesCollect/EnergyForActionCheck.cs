using UnityEngine;

public class EnergyForActionCheck : MonoBehaviour
{
    [SerializeField] private Player _player;

    public bool IsPlayerHasEnoughEnergy(int requiredEnergy)
    {
        if (_player.EnergyPoint - requiredEnergy < 0)
        {
            return false;
        }

        return true;
    }
}
