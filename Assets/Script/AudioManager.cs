using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource musicAudio;
    public AudioClip music;
    private void Start()
    {
        musicAudio.clip = music;
        musicAudio.Play();
    }   

    
}
