using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Mute : MonoBehaviour
{
    public AudioSource audioSource;
  
    public void ToggleAudioMute()
    {
        audioSource.mute = !audioSource.mute;
    }
}
