using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    // Public variables
    public MoneyManager moneyManager;
    public IoTDeviceManager ioTDeviceManager;
    public ShopButtonInfo shopButtonInfo;
    public SpawnPoint spawnPoint;
    public GameObject itemInfoBox;
    public GameObject buyButton;
    public AudioSource buttonSound;
    public AudioSource buySound;
    public int[,] shopItems = new int[5, 7];
    public string[] itemInfoTexts = new string[5];
    public float money;
    public Text moneyText;
    public Text debugText;
    public Text itemInfoText;

    // Private variables
    private int shopItemID;
    private int profitPercent;

    void Start()
    {
        itemInfoBox.SetActive(false);       // Close shop window
        money = ScoresManager.moneyAmount;  // Get money amount
        UpdateMoneyText();

        // ID's
        shopItems[1, 1] = 1;    // Fitting room device
        shopItems[1, 2] = 2;    // Price tag device
        shopItems[1, 3] = 3;    // Storage device
        shopItems[1, 4] = 4;    // Lighting device
        shopItems[1, 5] = 5;    // Robot vacuumcleaner device
        shopItems[1, 6] = 6;    // Robot assistant

        // Price
        shopItems[2, 1] = 1500; // Fitting room device
        shopItems[2, 2] = 1000; // Price tag device
        shopItems[2, 3] = 1000; // Storage device
        shopItems[2, 4] = 1500; // Lighting device
        shopItems[2, 5] = 500;  // Robot vacuumcleaner device
        shopItems[2, 6] = 0;    // Robot assistant

        // Set profit percent value
        SetProfitPercent();
    }

    // Get clicked items ID
    public void GetItemId()
    {
        // Get clicked buttons ShopButtonInfo script
        GameObject ButtonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;
        shopButtonInfo = ButtonRef.GetComponent<ShopButtonInfo>();
        
        // Get clicked buttons ID
        shopItemID = shopButtonInfo.ItemID;

        // Set new text to the shop window and open window and buy button
        itemInfoText.text = shopButtonInfo.infoText;

        if (shopItemID != 6)
        {
            buyButton.SetActive(true);
        }
        
        ShowItemInfoBox(true);
    }

    // Buy device
    public void Buy()
    {
        if (SettingsManager.soundOn)
        {
            // Play button sound
            buySound.Play();
        }

        // Get money amount
        money = moneyManager.GetMoney();

        // Check if we have enough money
        if (money >= shopItems[2, shopItemID])
        {
            // Remove money
            money -= shopItems[2, shopItemID];
            moneyManager.RemoveMoney((int)shopItems[2, shopItemID]);

            switch (shopItemID)
            {
                // Fitting room
                case 1:
                    IoTDeviceManager.fittingRoomDevice = true;
                    profitPercent++;
                    moneyManager.RaiseProfitMargin(profitPercent * 10);
                    ioTDeviceManager.DisableButton(shopItemID);
                    break;

                // Price tag
                case 2: 
                    // If there is more to buy
                    if (IoTDeviceManager.priceTagCount < 2)
                    {
                        // Raise profit margin and set new info text and price
                        IoTDeviceManager.priceTagCount++;
                        profitPercent++;
                        moneyManager.RaiseProfitMargin(profitPercent * 10);
                        shopButtonInfo.SetNextDevice(IoTDeviceManager.priceTagCount);

                        if (IoTDeviceManager.priceTagCount == 1)
                        {
                            shopItems[2, 2] = 2000;
                        }

                        if (IoTDeviceManager.priceTagCount == 2)
                        {
                            shopItems[2, 2] = 500;
                        }

                        ioTDeviceManager.CheckShopState();
                    }
                    // If everything has been bought
                    else
                    {
                        IoTDeviceManager.priceTagCount++;
                        profitPercent++;
                        moneyManager.RaiseProfitMargin(profitPercent * 10);
                        IoTDeviceManager.priceTagDevice = true;
                        ioTDeviceManager.DisableButton(shopItemID);
                        ioTDeviceManager.CheckShopState();
                    }

                    break;

                // Storage
                case 3: 
                    IoTDeviceManager.storageDevice = true;
                    ioTDeviceManager.DisableButton(shopItemID);
                    ioTDeviceManager.CheckShopState();
                    break;

                // Lighting
                case 4:
                    // If there is more to buy
                    if (IoTDeviceManager.lightingCount < 1)
                    {
                        IoTDeviceManager.lightingCount++;
                        shopButtonInfo.SetNextDevice(IoTDeviceManager.lightingCount);
                        shopItems[2, 4] = 2500;
                        ioTDeviceManager.CheckShopState();
                    }
                    // If everything has been bought
                    else
                    {
                        IoTDeviceManager.lightingCount++;
                        IoTDeviceManager.lightingDevice = true;
                        ioTDeviceManager.DisableButton(shopItemID);
                        ioTDeviceManager.CheckShopState();
                    }

                    break;

                // Robot vacuum cleaner
                case 5:
                    IoTDeviceManager.robotVacuumCleanerDevice = true;
                    ioTDeviceManager.DisableButton(shopItemID);
                    break;

                // Robot assistant
                case 6:
                    IoTDeviceManager.robotAssistantDevice = true;
                    ioTDeviceManager.DisableButton(shopItemID);
                    break;
            }

            // Disable buy button
            buyButton.SetActive(false);
            itemInfoText.text = shopButtonInfo.endNotes;
        }
    }

    private void UpdateMoneyText()
    {
        moneyText.text = money.ToString();
    }

    public void BonusMoney()
    {
        moneyManager.AddMoney(5);
        money = moneyManager.GetMoney();
    }

    // Open or close item info box
    public void ShowItemInfoBox(bool show)
    {
        // Play button sound
        if (SettingsManager.soundOn)
        {
            buttonSound.Play();
        }
        
        itemInfoBox.SetActive(show);
    }

    // Set profit percent value
    public void SetProfitPercent()
    {
        // Increase profit percent if fitting room device has been bought
        if (IoTDeviceManager.fittingRoomDevice == true)
        {
            profitPercent++;
        }

        // Increase profit percent by price tag device count
        profitPercent += IoTDeviceManager.priceTagCount;
    }
}