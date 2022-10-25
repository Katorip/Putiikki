using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeforeTutorialManager : MonoBehaviour
{
    // Public static variables
    public static bool tutorialPlayed = false;

    // Public variables
    public GameObject levels;
    public GameObject shop;

    void Start()
    {
        // If tutorial has been played, show level buttons and shop
        if (tutorialPlayed)
        {
            levels.SetActive(true);
            shop.SetActive(true);
        }
        // If tutorial has not been played, hide level buttons and shop
        else
        {
            levels.SetActive(false);
            shop.SetActive(false);
        }
    }

    // Set visibility of level buttons and shop
    public void IsTutorialPlayed(bool played)
    {
        tutorialPlayed = played;
        levels.SetActive(played);
        shop.SetActive(played);
    }
}