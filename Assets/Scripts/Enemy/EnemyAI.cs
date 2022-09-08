using Pathfinding;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float _minX;
    [SerializeField] private float _maxX;
    [SerializeField] private float _minZ;
    [SerializeField] private float _maxZ;
   
    [SerializeField] private float _height;

    [SerializeField] private float _minWalkableDistance;
    [SerializeField] private float _maxWalkableDistance;
    
    [SerializeField] private float _reachedPointDistance;
    
    [SerializeField] private GameObject _roamTarget;

    [SerializeField] private float _targetRange;
    
    [SerializeField] private float _stopTargetFollowingRange;

    private EnemyAttack _enemyAttack;
    
    private Vector3 _startPosition;
    private Vector3 _roamPosition;

    private AIDestinationSetter _aiDestinationSetter;
    private AILerp _aiLerp;

    private GameObject _player;
    
    private State _currentState;

    public GameObject Player => _player;
    
    public enum State
    {
        Roaming,
        FollowingTarget,
        AttackingTarget,
        GoingBackToStartPosition
    }

    private void Start()
    {
        _currentState = State.Roaming;
        
        _startPosition = GenerateRandomPosition();
        _roamPosition = GenerateRoamingPosition();
        _aiDestinationSetter = gameObject.GetComponent<AIDestinationSetter>();
        _enemyAttack = gameObject.GetComponent<EnemyAttack>();
        _aiLerp = gameObject.GetComponent<AILerp>();

        _player = GameObject.FindObjectOfType<Player>().gameObject;
        
        gameObject.transform.position = _startPosition;
    }

    private void Update()
    {
        switch (_currentState)
        {
            default:
                case State.Roaming:
                    _aiLerp.canMove = true;
                        
                    _roamTarget.transform.position = _roamPosition;
                        
                    if (Vector3.Distance(gameObject.transform.position, _roamPosition) < _reachedPointDistance) 
                    {
                            _roamPosition = GenerateRoamingPosition();
                    }
            
                    _aiDestinationSetter.target = _roamTarget.transform;
                    
                    FindTarget();
                    
                    break;
                case  State.FollowingTarget:
                    _aiLerp.canMove = true;
                    
                    _aiDestinationSetter.target = _player.transform;

                    if (Vector3.Distance(gameObject.transform.position, _player.transform.position) < _enemyAttack.AttackRange)
                    {
                        _enemyAttack.TryAttackPlayer();
                        gameObject.GetComponent<Animator>().SetTrigger("Punch");
                        _currentState = State.AttackingTarget;
                    }

                    if (Vector3.Distance(gameObject.transform.position, _player.transform.position) > _stopTargetFollowingRange)
                    {
                        _currentState = State.GoingBackToStartPosition;
                    }
                    break;
                case State.AttackingTarget:
                    _aiLerp.canMove = false;
                    break;
                case State.GoingBackToStartPosition:
                    _roamTarget.transform.position = _startPosition;
                    
                    _aiDestinationSetter.target = _roamTarget.transform;
                    
                    if (Vector3.Distance(gameObject.transform.position, _player.transform.position) < _targetRange)
                    {
                        _currentState = State.FollowingTarget;
                    }
                    
                    if (Vector3.Distance(gameObject.transform.position, _roamTarget.transform.position) < _reachedPointDistance)
                    {
                        _currentState = State.Roaming;
                    }
                    break;
        }
        
        
    }

    private Vector3 GenerateRandomPosition()
    {
        Vector3 position = new Vector3(Random.Range(_minX, _maxX), _height, Random.Range(_minZ, _maxZ));
        return position;
    }
    private Vector3 GenerateRoamingPosition()
    {
        Vector3 roamingPosition = _startPosition + GenerateRandomDirection() * Random.Range(_minWalkableDistance, _maxWalkableDistance);
        return roamingPosition;
    }
    private Vector3 GenerateRandomDirection()
    {
        Vector3 direction = new Vector3(Random.Range(-1f, 1f),0 ,Random.Range(-1f, 1f)).normalized;
        return direction;
    }

    private void FindTarget()
    {
        if (Vector3.Distance(gameObject.transform.position, _player.transform.position) < _targetRange)
        {
            _currentState = State.FollowingTarget;
        }
    }

    public void ChangeState(State state)
    {
        _currentState = state;
    }
}
