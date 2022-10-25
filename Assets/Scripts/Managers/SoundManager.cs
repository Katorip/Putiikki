// Manages game sounds

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Public variables
    public AudioSource bgMusic;

    void Start()
    {
        bgMusic.volume = SettingsManager.musicVolume;
        bgMusic.enabled = SettingsManager.musicOn;
    }

    // Stop background music
    public void StopBackgroundMusic()
    {
        bgMusic.enabled = false;
        bgMusic.Stop();
        bgMusic.mute = true;
    }
}