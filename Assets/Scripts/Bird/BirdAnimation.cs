using UnityEngine;

public class BirdAnimation : MonoBehaviour
{
    [SerializeField] private Animator _birdAnimator;
    public void BirdTakingOff()
    {
        _birdAnimator.SetTrigger("IsBirdTakingOff");
    }
}
