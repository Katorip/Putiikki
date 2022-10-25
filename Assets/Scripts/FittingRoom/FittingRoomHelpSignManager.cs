using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FittingRoomHelpSignManager : MonoBehaviour, IClickable
{
    public FittingRoomManager fittingRoomManager;

    // When player clicks the fitting room
    void IClickable.Click()
    {
        fittingRoomManager.ClickFittingRoom();
    }
}
