using UnityEngine;

public class GameStarted : MonoBehaviour
{
    [SerializeField] private Animation _blackScreenAnimation;
    private void Start()
    {
        StartBlackScreenDisappearAnimation();
    }

    private void StartBlackScreenDisappearAnimation()
    {
        _blackScreenAnimation.Play("BlackScreenDisappear");
    }
}
