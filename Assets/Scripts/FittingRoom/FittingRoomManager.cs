// Manages fitting room functions

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FittingRoomManager : MonoBehaviour//, IClickable
{
    // Public variables
    public SatisfactionManager satisfactionManager;
    public FittingRoomPlayerTarget fittingRoomPlayerTarget;
    public PlayerInventory playerInventory;
    private CustomerTarget customerTarget;
    public GameObject helpSign;
    public GameObject infoBox;
    public GameObject helpMessageButton;
    public GameObject viewportContent;
    public TMP_Text infoText;
    public SpriteRenderer spriteRenderer;
    public SpriteRenderer colorIconSprite;
    public Sprite curtainsCloseSprite;
    public Sprite curtainsOpenSprite;
    public AudioSource correctSound;
    public AudioSource errorSound;
    public bool customerNeedsHelp = false;
    public bool isOccupied = false;
    public int index;
    public Sprite[] wantedColorApple = new Sprite[4];
    public Sprite[] wantedColorHourglass = new Sprite[4];
    public Sprite[] wantedColorLemon = new Sprite[4];
    public Sprite[] wantedColorPear = new Sprite[4];

    // Private variables
    private GameObject customer;
    private GameObject copy;

    void Start()
    {
        helpSign.SetActive(false);
        infoBox.SetActive(false);
        infoText.text = "";
        colorIconSprite.sprite = null;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        // If customer goes to the fitting room and it is not occupied
        if (col.gameObject.tag == "Customer" && customer == null)
        {
            customer = col.gameObject;
            customerTarget = customer.transform.parent.gameObject.GetComponentInChildren<CustomerTarget>();        

            if (customerTarget.fitRoomIndex == index)
            {
                isOccupied = true;
                satisfactionManager = customer.GetComponentInChildren<SatisfactionManager>();    

                // If customer has cloth to fit
                if (satisfactionManager.needFitting && satisfactionManager.getClothes)
                {
                    satisfactionManager.ChangeWantedSize();             // Change new color that customer wants
                    satisfactionManager.waitingInFittingRoom = true;    // Decrease satisfaction rate
                    spriteRenderer.sprite = curtainsCloseSprite;

                    // If player is already next to the fitting room, update text
                    if (fittingRoomPlayerTarget.playerTargetReached)
                    {
                        GetWantedColorSprite(satisfactionManager.wantedColor, satisfactionManager.fruit);
                    }

                    StartCoroutine(FitClothes());
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        // When customer leaves the fitting room
        if (col.gameObject.tag == "Customer" && col.gameObject == customer)
        {            
            satisfactionManager = null;
            helpSign.SetActive(false);
            isOccupied = false;
            customer = null;
            customerTarget = null;
            infoText.text = "";
            colorIconSprite.sprite = null;
            spriteRenderer.sprite = curtainsOpenSprite;

            if (IoTDeviceManager.fittingRoomDevice)
            {
                ShowInfoBox(false);
            }          
        }
    }

    // Tell player that customer needs help after fitting the cloth
    IEnumerator FitClothes()
    {
        yield return new WaitForSeconds(2);

        // Do it only if customer are still in the fitting room
        if (isOccupied)
        {
            customerNeedsHelp = true;
            helpSign.SetActive(true);
            GetWantedColorSprite(satisfactionManager.wantedColor, satisfactionManager.fruit);

            if (IoTDeviceManager.fittingRoomDevice)
            {
                ShowInfoBox(true);
            }
        }
    }

    // Set text and info box visibility to what customer wants
    public void ShowInfoBox(bool show)
    {
        infoBox.SetActive(show);       
    }

    public void ClickFittingRoom()
    {
        // If customer needs help in fittingroom and player is in front of the fitting room
        if (customerNeedsHelp && fittingRoomPlayerTarget.playerTargetReached)
        {
            // If player has right cloth in inventory
            if (playerInventory.shapeRack == satisfactionManager.wantedRack && playerInventory.colorRack == satisfactionManager.wantedColor)
            {
                isOccupied = false;
                satisfactionManager.needFitting = false;
                satisfactionManager.waitingInFittingRoom = false;
                satisfactionManager.SatisfactionUp();
                playerInventory.ResetInventory();

                if (SettingsManager.soundOn)
                {
                    correctSound.Play();
                }
            }
            else
            {
                if (SettingsManager.soundOn)
                {
                    errorSound.Play();
                }
            }
        }
    }

    private void GetWantedColorSprite(string color, string shape)
    {
        // If shape is apple
        if (shape == "Apple")
        {
            switch (color)
            {
                case "Blue":
                    colorIconSprite.sprite = wantedColorApple[0];
                    break;

                case "Turquoise":
                    colorIconSprite.sprite = wantedColorApple[1];
                    break;

                case "Red":
                    colorIconSprite.sprite = wantedColorApple[2];
                    break;

                case "Yellow":
                    colorIconSprite.sprite = wantedColorApple[3];
                    break;
            }
        }

        // If shape is lemon
        else if (shape == "Lemon")
        {
            switch (color)
            {
                case "Blue":
                    colorIconSprite.sprite = wantedColorLemon[0];
                    break;

                case "Turquoise":
                    colorIconSprite.sprite = wantedColorLemon[1];
                    break;

                case "Red":
                    colorIconSprite.sprite = wantedColorLemon[2];
                    break;

                case "Yellow":
                    colorIconSprite.sprite = wantedColorLemon[3];
                    break;
            }
        }

        // If shape is hourglass
        else if (shape == "Hourglass")
        {
            switch (color)
            {
                case "Blue":
                    colorIconSprite.sprite = wantedColorHourglass[0];
                    break;

                case "Turquoise":
                    colorIconSprite.sprite = wantedColorHourglass[1];
                    break;

                case "Red":
                    colorIconSprite.sprite = wantedColorHourglass[2];
                    break;

                case "Yellow":
                    colorIconSprite.sprite = wantedColorHourglass[3];
                    break;
            }
        }

        // If shape is pear
        else if (shape == "Pear")
        {
            switch (color)
            {
                case "Blue":
                    colorIconSprite.sprite = wantedColorPear[0];
                    break;

                case "Turquoise":
                    colorIconSprite.sprite = wantedColorPear[1];
                    break;

                case "Red":
                    colorIconSprite.sprite = wantedColorPear[2];
                    break;

                case "Yellow":
                    colorIconSprite.sprite = wantedColorPear[3];
                    break;
            }
        }
    }
}