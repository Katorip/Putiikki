// Manages game settings

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    // Public variables
    public GameObject settingsWindow;
    public GameObject creditsWindow;
    public GameObject moreInfoBox;
    public GameObject deviceInfoWindow;
    public Slider volumeSlider;
    public Toggle musicToggle;
    public Toggle soundToggle;
    public AudioSource buttonSound;

    // Static variables
    public static float musicVolume = 1.0f;
    public static bool musicOn = true;
    public static bool soundOn = true;

    void Start()
    {
        settingsWindow.SetActive(false);
        creditsWindow.SetActive(false);
        moreInfoBox.SetActive(false);
        deviceInfoWindow.SetActive(false);
        volumeSlider.value = musicVolume;
        musicToggle.isOn = musicOn;
        soundToggle.isOn = soundOn;
    }

    // Set visibility of settings window
    public void ShowSettingsWindow(bool show)
    {
        settingsWindow.SetActive(show);
        PlayButtonSound();
    }

    // Set visibility of credits window
    public void ShowCreditsWindow(bool show)
    {
        creditsWindow.SetActive(show);
        PlayButtonSound();
    }

    // Set visibility of more info box
    public void ShowMoreInfoBox(bool show)
    {
        moreInfoBox.SetActive(show);
        PlayButtonSound();
    }

    // Set visibility of device info window
    public void ShowDeviceInfoWindow(bool show)
    {
        deviceInfoWindow.SetActive(show);
        PlayButtonSound();
    }

    // Open privacy policy link
    public void OpenPrivacyPolicy()
    {
        PlayButtonSound();
        Application.OpenURL("https://iol.lab.fi/index.php/privacy-policy/");
    }

    // Set volume with slider
    public void SetVolume(float volume)
    {
        musicVolume = volume;
    }

    // Toggle background music on and off
    public void ToggleMusic(bool music)
    {
        musicOn = music;
        PlayButtonSound();
    }

    // Toggle button sound on and off
    public void ToggleSound(bool sound)
    {
        soundOn = sound;
        PlayButtonSound();
    }

    // Play button sound
    private void PlayButtonSound()
    {
        if (soundOn)
        {
            buttonSound.Play();
        }       
    }
}