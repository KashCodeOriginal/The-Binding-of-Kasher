using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    [SerializeField] private int _health;

    public void TryApplyDamage(int value)
    {
        _health -= value;

        if (_health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Умер");
    }
}
