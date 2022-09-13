using UnityEngine;

public class GameStarted : MonoBehaviour
{
    [SerializeField] private Animation _blackScreenAnimation;

    public void StartBlackScreenDisappearAnimation()
    {
        _blackScreenAnimation.Play("BlackScreenDisappear");
    }
}
