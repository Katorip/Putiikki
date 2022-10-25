// Manages game levels

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoadManager : MonoBehaviour
{
    // Public variables
    public GameObject pauseMenu;
    public MoneyManager moneyManager;
    public AudioSource buttonSound;

    // Private variables
    private string sceneName = "";

    // Pause the game time
    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        PlayButtonSound();
    }

    // Continue playing
    public void ContinueGame()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        PlayButtonSound();
    }

    // Load new level scene
    public void LoadNewLevel(string name)
    {
        // If player has lives left
        if (LifeTimerManager.lives > 0)
        {
            // Load spesific level
            sceneName = name;
            SceneManager.LoadScene(sceneName);
            Time.timeScale = 1;
            PlayButtonSound();
        }
    }

    // Load current level again
    public void LoadCurrentLevel()
    {
        // If player has lives left
        if (LifeTimerManager.lives > 0)
        {
            // Current level load 
            moneyManager.ResetMoneyAmount();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Time.timeScale = 1;
            PlayButtonSound();
        } 
    }

    // Load main menu
    public void LoadMainMenu()
    {
        SaveGame();
        SceneManager.LoadScene("MainMenu");
        PlayButtonSound();
    }

    // Close the game
    public void ExitTheGame()
    {
        SaveGame();
        PlayButtonSound();
        Application.Quit();
    }

    // Save game
    private void SaveGame()
    {
        GPGSAuthentication.Instance.OpenSaveToCloud(true);
    }

    // Load saved game
    private void OpenSavedGame()
    {
        GPGSAuthentication.Instance.OpenSaveToCloud(false);
    }

    // Play button sound
    private void PlayButtonSound()
    {
        // If sound is not switched off
        if (SettingsManager.soundOn)
        {
            buttonSound.Play();
        }        
    }
}