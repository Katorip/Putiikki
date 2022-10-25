// Manges if player is at fitting room or not

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FittingRoomPlayerTarget : MonoBehaviour
{
    // Public variables
    public FittingRoomManager fittingRoomManager;
    public bool playerTargetReached;

    // When player get next to the fitting room
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" && fittingRoomManager.customerNeedsHelp)
        {
            playerTargetReached = true;

            if (!IoTDeviceManager.fittingRoomDevice)
            {
                fittingRoomManager.ShowInfoBox(true);
            }
        }
    }

    // When player leaves from the fitting room
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            playerTargetReached = false;

            if (!IoTDeviceManager.fittingRoomDevice)
            {
                fittingRoomManager.ShowInfoBox(false);
            }
        }
    }
}
