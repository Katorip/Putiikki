using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopButtonInfo : MonoBehaviour
{
    // Public variables
    public int ItemID;
    public ShopButtonInfo shopButtonInfo;
    public Text PriceText;
    public Text nameText;
    public GameObject ShopManager;
    public string infoText;
    public string[] infoTexts = new string[3];
    public string[] nameTexts = new string[3];
    public string endNotes;
    public Image deviceImage;
    public Sprite[] deviceSprites;

    void Start()
    {
        if (shopButtonInfo != null)
        {
            if (shopButtonInfo.ItemID == 2 && IoTDeviceManager.priceTagCount < 3)
            {
                shopButtonInfo.SetNextDevice(IoTDeviceManager.priceTagCount);
            }

            if (shopButtonInfo.ItemID == 4 && IoTDeviceManager.lightingCount < 2)
            {
                shopButtonInfo.SetNextDevice(IoTDeviceManager.lightingCount);
            }
        }
    }

    void Update()
    {
        UpdatePriceText();
    }

    private void UpdatePriceText()
    {
        PriceText.text = ShopManager.GetComponent<ShopManager>().shopItems[2, ItemID].ToString();
    }

    // Set next devices info in to the shop
    public void SetNextDevice(int i)
    {
        infoText = infoTexts[i];
        nameText.text = nameTexts[i];
        deviceImage.sprite = deviceSprites[i];
    }
}