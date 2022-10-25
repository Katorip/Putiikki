// Manages speech bubbles

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechBubbleManager : MonoBehaviour, IClickable
{
    public ShelfManager shelfManager;

    void IClickable.Click()
    {
        if (shelfManager != null)
        {
            shelfManager.ClickToAddClothes();
        }        
    }
}
