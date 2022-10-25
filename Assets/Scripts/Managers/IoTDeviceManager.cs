// Manages IoT Devices that player can buy

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IoTDeviceManager : MonoBehaviour
{
    // Static variables
    public static bool fittingRoomDevice;
    public static bool robotVacuumCleanerDevice;
    public static bool storageDevice;
    public static bool priceTagDevice;
    public static bool lightingDevice;
    public static bool robotAssistantDevice;
    public static int priceTagCount;
    public static int lightingCount;

    // Public variables
    public Button[] shopButtons;
    public GameObject[] questionMarkImages;
    public GameObject sfrQuestionMark;
    public GameObject siQuestionMark;
    public GameObject rfidQuestionMark;
    public bool hasButtons;
    public bool isMainMenu;

    private void Start()
    {
        // If scene is Main Menu
        if (isMainMenu)
        {
            CheckShopState();
        }
        
        // If there is buttons
        if (hasButtons)
        {
            DisableButtons();
        }

        if (priceTagCount < 2 && isMainMenu)
        {
            rfidQuestionMark.SetActive(false);
        }
    }

    // Disable certain button
    public void DisableButton(int number)
    {
        int i = number - 1;
        shopButtons[i].interactable = false;
        shopButtons[i].gameObject.transform.GetChild(0).gameObject.SetActive(false);
        shopButtons[i].gameObject.transform.GetChild(1).GetComponent<Image>().color = new Color32(255, 255, 225, 100);
    }

    // Disable bought device buttons
    public void DisableButtons()
    {
        if (fittingRoomDevice)
        {
            shopButtons[0].interactable = false;
            shopButtons[0].gameObject.transform.GetChild(0).gameObject.SetActive(false);
            shopButtons[0].gameObject.transform.GetChild(1).GetComponent<Image>().color = new Color32(255, 255, 225, 100);
            Destroy(sfrQuestionMark);
        }

        if (priceTagDevice)
        {
            shopButtons[1].interactable = false;
            shopButtons[1].gameObject.transform.GetChild(0).gameObject.SetActive(false);
            shopButtons[1].gameObject.transform.GetChild(1).GetComponent<Image>().color = new Color32(255, 255, 225, 100);
        }

        if (storageDevice)
        {
            shopButtons[2].interactable = false;
            shopButtons[2].gameObject.transform.GetChild(0).gameObject.SetActive(false);
            shopButtons[2].gameObject.transform.GetChild(1).GetComponent<Image>().color = new Color32(255, 255, 225, 100);
            Destroy(siQuestionMark);
        }

        if (lightingDevice)
        {
            shopButtons[3].interactable = false;
            shopButtons[3].gameObject.transform.GetChild(0).gameObject.SetActive(false);
            shopButtons[3].gameObject.transform.GetChild(1).GetComponent<Image>().color = new Color32(255, 255, 225, 100);
        }

        if (robotVacuumCleanerDevice)
        {
            shopButtons[4].interactable = false;
            shopButtons[4].gameObject.transform.GetChild(0).gameObject.SetActive(false);
            shopButtons[4].gameObject.transform.GetChild(1).GetComponent<Image>().color = new Color32(255, 255, 225, 100);
        }

        if (robotAssistantDevice)
        {
            shopButtons[5].interactable = false;
            shopButtons[5].gameObject.transform.GetChild(0).gameObject.SetActive(false);
            shopButtons[5].gameObject.transform.GetChild(1).GetComponent<Image>().color = new Color32(255, 255, 225, 100);
        }
    }

    // Check if all devices can be purchached
    public void CheckShopState()
    {
        // If player has bought all price tag devices and lighting devices
        if (priceTagCount >= 3 && lightingCount >= 2)
        {
            // Destory question mark images
            for (int i = 0; i < questionMarkImages.Length; i++)
            {
                Destroy(questionMarkImages[i]);
            }

            // If player has bought storage device also, enable SFR button
            if (storageDevice && !fittingRoomDevice)
            {
                Destroy(sfrQuestionMark);
                shopButtons[0].interactable = true;
            }

            // Set button interactable if player hasn't bought device already
            if (!robotVacuumCleanerDevice)
            {
                shopButtons[4].interactable = true;
            }

            // Set button interactable if player hasn't bought device already
            if (!robotAssistantDevice)
            {
                shopButtons[5].interactable = true;
            }           
        }
        // Else player can't press buttons
        else
        {
            shopButtons[4].interactable = false;
            shopButtons[5].interactable = false;
            shopButtons[0].interactable = false;
        }

        // If player has bought all lighting devices
        if (lightingCount >= 2 && !storageDevice)
        {
            Destroy(siQuestionMark);
            shopButtons[2].interactable = true;
        }
        // If not, disable Smart Inventory button
        else
        {
            shopButtons[2].interactable = false;
        }

        // If player has bought 2 pricetag devices and Smart Inventory
        if (priceTagCount >= 2 && storageDevice && !priceTagDevice)
        {
            Destroy(rfidQuestionMark);
            shopButtons[1].interactable = true;
        }
        else if (priceTagCount >= 2 && !storageDevice)
        {
            rfidQuestionMark.SetActive(true);
            shopButtons[1].interactable = false;
        }
    }
}