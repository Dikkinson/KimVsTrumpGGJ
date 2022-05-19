using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public bool isMusic;
    AudioSource audio;
    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }
    void Update()
    {
        if(isMusic == true)
        {
            audio.volume = AudioSettingsController.MusicVolume;
        }
        else
        {
            audio.volume = AudioSettingsController.EffectVolume;
        }
    }
}
