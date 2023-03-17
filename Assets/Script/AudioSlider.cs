using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioSlider : MonoBehaviour
{
    public AudioSource audioSource;
    private float musicvolume = 1f;

    void Update()
    {
        audioSource.volume = musicvolume;        
    }
    public void SetVolume(float vol)
    {
        musicvolume = vol;
    }
}
