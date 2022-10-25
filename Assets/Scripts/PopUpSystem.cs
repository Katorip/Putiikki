using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopUpSystem : MonoBehaviour, IClickable
{
    public ShelfManager shelfManager;
    public GameObject popUpBox;

    private void Start()
    {
        popUpBox.SetActive(false);
    }

    // Open pop up box
    public void PopUp()
    {
        popUpBox.SetActive(true);
    }

    // Close pop up box
    public void ClosePopUp()
    {
        popUpBox.SetActive(false);
    }

    // When player clicks the clothesrack when next to it
    void IClickable.Click()
    {
        shelfManager.ClickToAddClothes();
    }
}
