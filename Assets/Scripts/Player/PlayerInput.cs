using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private FloatingJoystick _joystick;

    private void FixedUpdate()
    {
        gameObject.GetComponent<PlayerMovement>().MovePlayer(_joystick);
    }
}
