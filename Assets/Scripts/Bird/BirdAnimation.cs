using UnityEngine;

public class BirdAnimation : MonoBehaviour
{
    [SerializeField] private Animation _birdAnimation;

    public void BirdTakingOff()
    {
        _birdAnimation.Play("BirdTakeOff");
    }
}
