// Manages if player is in front of rack or not

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RackTargetPlayerManager : MonoBehaviour
{
    // Public variables
    public ShelfManager shelfManager;
    public PopUpSystem popUpSystem;

    private void OnTriggerEnter2D(Collider2D col)
    {      
        // If player gets in front of rack
        if (col.gameObject.tag == "Player")
        {
            shelfManager.playerTargetReached = true;
            popUpSystem.PopUp();                        // Open rack's pop up box
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        // When player leaves from rack
        if (col.gameObject.tag == "Player")
        {
            shelfManager.playerTargetReached = false;
            popUpSystem.ClosePopUp();                   // Close rack's pop up box
        }
    }
}
