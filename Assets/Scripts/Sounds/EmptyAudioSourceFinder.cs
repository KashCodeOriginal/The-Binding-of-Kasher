using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EmptyAudioSourceFinder : MonoBehaviour
{
    [SerializeField] private List<AudioSource> _audioSources = new List<AudioSource>();

    private void Start()
    {
        _audioSources.Add(gameObject.GetComponent<AudioSource>());
    }
    
    public void PlayAnySound(AudioClip sound)
    {
        AudioSource _emptyAudioSource = FindFirstEmptyAudioSource();

        if (_emptyAudioSource == null)
        {
            CreateAudioSource(sound);
            return;
        }

        AddSoundToAudioSource(_emptyAudioSource, sound);
    }

    private void CreateAudioSource(AudioClip clip)
    {
        var audioSource = gameObject.AddComponent<AudioSource>();
        _audioSources.Add(audioSource);
        AddSoundToAudioSource(audioSource, clip);
    }

    private AudioSource FindFirstEmptyAudioSource()
    {
        var audioSource = _audioSources.FirstOrDefault(p => p.isPlaying == false);
        return audioSource;
    }

    private void AddSoundToAudioSource(AudioSource audioSource, AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
        audioSource.volume = 0.05f;
    }
}
