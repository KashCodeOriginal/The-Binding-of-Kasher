using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float _timeBetweenAttacks;
    
    [SerializeField] private float _attackRange;

    private EnemyAI _enemyAI;

    private float _currentAttackTime;

    private bool _isAttacked;

    public float AttackRange => _attackRange;

    private void Start()
    {
        _isAttacked = false;

        _enemyAI = gameObject.GetComponent<EnemyAI>();
    }

    private void Update()
    {
        if (_isAttacked == true)
        {
            _currentAttackTime += Time.deltaTime;

            if (_currentAttackTime >= _timeBetweenAttacks)
            {
                Attack();
                _enemyAI.ChangeState(EnemyAI.State.FollowingTarget);
                _isAttacked = false;
                _currentAttackTime = 0;
            }
        }
    }
    
    public void TryAttackPlayer()
    {
        if (_isAttacked == false)
        {
            _isAttacked = true;
        }
    }

    private void Attack()
    {
        Debug.Log("Ударил");
    }
}
