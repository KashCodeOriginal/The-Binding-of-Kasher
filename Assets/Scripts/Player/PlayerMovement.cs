using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _speed;

    [SerializeField] private bool _canRun;

    [SerializeField] private bool _canMove;

    [SerializeField] private Animator _animator;

    [SerializeField] private SoundsPlayer _soundsPlayer;

    [SerializeField] private float _timeBetweenStepsSound;

    [SerializeField] private float _currentTimeBetweenStepsSound;

    private float horizontal;
    private float vertical;

    private void Start()
    {
        _canRun = true;
        _canMove = true;

        _currentTimeBetweenStepsSound = 0;
    }

    public void MovePlayer(FloatingJoystick joystick)
    {
        if (_canMove == true)
        {
            _rigidbody.velocity = new Vector3(joystick.Horizontal * _speed, _rigidbody.velocity.y, joystick.Vertical * _speed);
            if (joystick.Horizontal != 0 || joystick.Vertical != 0)
            {
                Quaternion _rotation = Quaternion.LookRotation(_rigidbody.velocity, Vector3.up);
                transform.rotation = new Quaternion(0, _rotation.y, 0, _rotation.w);
            
                _animator.SetBool("IsWalking", true);
                
                _currentTimeBetweenStepsSound += Time.deltaTime;

                if (_currentTimeBetweenStepsSound >= _timeBetweenStepsSound)
                {
                    _soundsPlayer.PlayStepSound();
                    _currentTimeBetweenStepsSound = 0;
                }

                if (_canRun == true)
                {
                    horizontal = joystick.Horizontal >= 0 ? horizontal = joystick.Horizontal : horizontal = -joystick.Horizontal;
                    vertical = joystick.Vertical >= 0 ? vertical = joystick.Vertical : vertical = -joystick.Vertical;

                    _timeBetweenStepsSound = 0.4f;

                    if (horizontal >= 0.3f)
                    {
                        _animator.SetFloat("WalkToRun", horizontal);
                    }
                    else if (vertical >= 0.3f)
                    {
                        _animator.SetFloat("WalkToRun", vertical);
                    }
                    else
                    {
                        horizontal = 0;
                        vertical = 0;
                    }
                }
                else
                {
                    _animator.SetFloat("WalkToRun", 0);
                    _timeBetweenStepsSound = 0.8f;
                }
                
            }
            else
            {
                _animator.SetBool("IsWalking", false);
            }
        }
    }

    public void SetDefaultSpeed()
    {
        _speed = 10;
    }
    public void IncreaseSpeed(int value)
    {
        _speed = value;
        if (_speed > 4)
        {
            _canRun = true;
        }
    }
    public void DecreaseSpeed(int value)
    {
        _speed = value;

        if (_speed <= 4)
        {
            _canRun = false;
        }
    }

    public void SetMoveState(bool canMove)
    {
        _canMove = canMove;
    }

    public void StopPlayerMovement()
    {
        _rigidbody.useGravity = false;
        _canMove = false;
    }
    public void StartPlayerMovement()
    {
        _rigidbody.useGravity = true;
        _canMove = true;
    }
}
