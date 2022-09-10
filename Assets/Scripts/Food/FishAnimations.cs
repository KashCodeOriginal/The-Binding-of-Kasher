using UnityEngine;

public class FishAnimations : MonoBehaviour
{
    [SerializeField] private Animation _animation;

    public void PlayCatchTextAnimation()
    {
        _animation.Play("CatchTextAnimation");
    }
    public void StopCatchTextAnimation()
    {
        _animation.Stop("CatchTextAnimation");
    }
}
