using UnityEngine;

public class CargoShipAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void StartShipArriving()
    {
        StartAnimations();
    }
    
    public void StartShipSailing()
    {
        _animator.SetTrigger("IsShipSailing");
    }
    
    public void StartAnimations()
    {
        _animator.speed = 1;
    }
    
    public void StopAnimations()
    {
        _animator.speed = 0;
    }

    public void MoveShipToDefaultPosition()
    {
        StopAnimations();
    }
}
