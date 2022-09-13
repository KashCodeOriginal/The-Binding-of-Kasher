using UnityEngine;

public class TextWritingSound : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    public void PlayCharSound()
    {
        _audioSource.Play();
    }
}
