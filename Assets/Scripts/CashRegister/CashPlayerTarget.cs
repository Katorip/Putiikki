// Manages if palyer is at the cash register or not

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashPlayerTarget : MonoBehaviour
{
    // Public variables
    public bool playerTargetReached = false;

    // Check if player is next to the cash register
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            playerTargetReached = true;
        }
    }

    // Player is no longer next to a cash register
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            playerTargetReached = false;
        }
    }
}
