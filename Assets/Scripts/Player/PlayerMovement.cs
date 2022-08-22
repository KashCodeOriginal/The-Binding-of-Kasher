using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _speed;

    [SerializeField] private bool _canRun;

    [SerializeField] private Animator _animator;

    private float horizontal;
    private float vertical;

    private void Start()
    {
        _canRun = true;
    }

    public void MovePlayer(FloatingJoystick joystick)
    {
        _rigidbody.velocity = new Vector3(joystick.Horizontal * _speed, _rigidbody.velocity.y, joystick.Vertical * _speed);
        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            Quaternion _rotation = Quaternion.LookRotation(_rigidbody.velocity, Vector3.up);
            transform.rotation = new Quaternion(0, _rotation.y, 0, _rotation.w);
            
            _animator.SetBool("IsWalking", true);

            horizontal = joystick.Horizontal >= 0 ? horizontal = joystick.Horizontal : horizontal = -joystick.Horizontal;
            vertical = joystick.Vertical >= 0 ? vertical = joystick.Vertical : vertical = -joystick.Vertical;
            
            if (horizontal >= 0.3f && _canRun == true)
            {
                _animator.SetFloat("WalkToRun", horizontal);
            }
            else if (vertical >= 0.3f && _canRun == true)
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
            _animator.SetBool("IsWalking", false);
        }
    }

    public void SetDefaultSpeed()
    {
        _speed = 10;
    }
    public void IncreaseSpeed(int value)
    {
        _speed = value;
        if (_speed > 5)
        {
            _canRun = true;
        }
    }
    public void DecreaseSpeed(int value)
    {
        _speed = value;

        if (_speed <= 5)
        {
            _canRun = false;
        }
    }
    
}
