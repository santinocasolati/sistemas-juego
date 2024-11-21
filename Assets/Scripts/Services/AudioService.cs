using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioService : BaseService
{
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayAudio(AudioClip clip)
    {
        _audioSource.PlayOneShot(clip);
    }
}
