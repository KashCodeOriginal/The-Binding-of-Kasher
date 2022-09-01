using UnityEngine;

public class CargoShipAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    
    public void StartShipSailing()
    {
        _animator.SetTrigger("IsShipSailing");
    }
    public void StartShipArriving()
    {
        _animator.SetTrigger("IsShipArriving");
    }

    public void SetApplyRootMotionToTrue()
    {
        _animator.applyRootMotion = true;
    }
    public void SetApplyRootMotionToFalse()
    {
        _animator.applyRootMotion = false;
    }
}
