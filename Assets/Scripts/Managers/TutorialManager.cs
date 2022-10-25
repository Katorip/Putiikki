// Manages game tutorial

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    // Public variables
    [Header("Managers")]
    public WinCondition winCondition;

    [Header("Game Objects")]
    public GameObject tutorialBox;
    public GameObject spawnPoint;
    public GameObject lemonTutorial;
    public GameObject helpSignTutorial;

    [Header("Text Objects")]
    public Text tutorialText;

    [Header("Sounds")]
    public AudioSource buttonSound;

    [Header("Animators")]
    public Animator playerAnimator;
    public Animator cashRegisterAnimator;
    public Animator storageAnimator;
    public Animator winConditionAnimator;
    public Animator timeAnimator;
    public Animator speechBubbleAnimator;
    public Animator moneyAnimator;
    public Animator helpSignAnimator;

    [Header("Text Array")]
    public string[] tutorialTexts;

    // Private variables
    private int round = 0;

    private void Start()
    {
        helpSignTutorial.SetActive(true);
    }

    // When player clicks the close button of tutorial box
    public void CloseTutorialBox()
    {
        // Play button sound
        if (SettingsManager.soundOn)
        {
            buttonSound.Play();
        }

        SetTutorialText(tutorialTexts[round]);
        
        switch (round)
        {
            // Player info
            case 1:
                playerAnimator.SetBool("Tutorial", true);
                break;

            // Speech bubble info
            case 2:
                playerAnimator.SetBool("Tutorial", false);
                speechBubbleAnimator.SetBool("Tutorial", true);
                break;

            // Storage info
            case 3:
                speechBubbleAnimator.SetBool("Tutorial", false);
                storageAnimator.SetBool("Tutorial", true);
                break;

            // Money info
            case 4:
                storageAnimator.SetBool("Tutorial", false);
                moneyAnimator.SetBool("Tutorial", true);
                break;

            // Help sign info
            case 5:
                helpSignTutorial.SetActive(true);
                moneyAnimator.SetBool("Tutorial", false);
                helpSignAnimator.SetBool("Tutorial", true);
                break;

            case 6:
                helpSignAnimator.SetBool("Tutorial", false);
                break;

            // Cash Register info
            case 7:
                cashRegisterAnimator.SetBool("Tutorial", true);
                break;

            // Win condition info
            case 8:
                cashRegisterAnimator.SetBool("Tutorial", false);
                winConditionAnimator.SetBool("Tutorial", true);               
                break;

            // Time info
            case 9:
                winConditionAnimator.SetBool("Tutorial", false);
                timeAnimator.SetBool("Tutorial", true);              
                break;

            // Money info
            case 10:
                timeAnimator.SetBool("Tutorial", false);
                moneyAnimator.SetBool("Tutorial", true);
                break;

            // Start the game
            case 11:
                moneyAnimator.SetBool("Tutorial", false);
                Destroy(lemonTutorial);
                tutorialBox.SetActive(false);
                helpSignTutorial.SetActive(false);
                spawnPoint.SetActive(true);
                winCondition.StartGameTime();
                break;
        }

        round++;
    }

    // Set tutorial text to the text box
    private void SetTutorialText(string infoText)
    {
        tutorialText.text = infoText;
    }
}