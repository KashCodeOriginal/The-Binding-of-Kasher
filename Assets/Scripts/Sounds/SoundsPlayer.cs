using UnityEngine;

public class SoundsPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip _stepsSound;

    [SerializeField] private EmptyAudioSourceFinder _emptyAudioSourceFinder;

    public void PlayStepSound()
    {
        _emptyAudioSourceFinder.PlayAnySound(_stepsSound);
    }
}
