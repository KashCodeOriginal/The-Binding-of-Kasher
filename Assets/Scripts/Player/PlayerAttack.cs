using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject _currentEnemy;

    [SerializeField] private int _damage;
    
    public void TryAttack()
    {
        if (_currentEnemy != null)
        {
            _currentEnemy.TryGetComponent(out IDamagable idamagable);

            if (idamagable != null)
            {
                idamagable.TryApplyDamage(_damage);
            }
        }
    }

    public void SetCurrentEnemy(GameObject currentEnemy)
    {
        _currentEnemy = currentEnemy;
    }
}
