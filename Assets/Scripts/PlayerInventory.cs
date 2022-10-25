using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    // Public variables
    public MoneyManager moneyManager;
    public string shapeRack = "";
    public string colorRack = "";
    public Image collectedImage;
    public Sprite[] appleSprites;
    public Sprite[] lemonSprites;
    public Sprite[] hourglassSprites;
    public Sprite[] pearSprites;

    private void Start()
    {
        ResetInventory();
    }

    public void ResetInventory()
    {
        collectedImage.sprite = null;
        shapeRack = "";
        colorRack = "";
    }

    // Add clothes to the inventory and set inventory text
    public void AddClothesToInventory(string shape, string color)
    {
        if (moneyManager.GetMoney() > 0)
        {
            moneyManager.RemoveMoney();

            shapeRack = shape;
            colorRack = color;

            // If shape is apple
            if (shapeRack == "Apple")
            {
                switch (color)
                {
                    case "Blue":
                        collectedImage.sprite = appleSprites[0];
                        break;

                    case "Turquoise":
                        collectedImage.sprite = appleSprites[1];
                        break;

                    case "Red":
                        collectedImage.sprite = appleSprites[2];
                        break;

                    case "Yellow":
                        collectedImage.sprite = appleSprites[3];
                        break;
                }
            }

            // If shape is lemon
            else if (shapeRack == "Lemon")
            {
                switch (color)
                {
                    case "Blue":
                        collectedImage.sprite = lemonSprites[0];
                        break;

                    case "Turquoise":
                        collectedImage.sprite = lemonSprites[1];
                        break;

                    case "Red":
                        collectedImage.sprite = lemonSprites[2];
                        break;

                    case "Yellow":
                        collectedImage.sprite = lemonSprites[3];
                        break;
                }
            }

            // If shape is hourglass
            else if (shapeRack == "Hourglass")
            {
                switch (color)
                {
                    case "Blue":
                        collectedImage.sprite = hourglassSprites[0];
                        break;

                    case "Turquoise":
                        collectedImage.sprite = hourglassSprites[1];
                        break;

                    case "Red":
                        collectedImage.sprite = hourglassSprites[2];
                        break;

                    case "Yellow":
                        collectedImage.sprite = hourglassSprites[3];
                        break;
                }
            }

            // If shape is pear
            else if (shapeRack == "Pear")
            {
                switch (color)
                {
                    case "Blue":
                        collectedImage.sprite = pearSprites[0];
                        break;

                    case "Turquoise":
                        collectedImage.sprite = pearSprites[1];
                        break;

                    case "Red":
                        collectedImage.sprite = pearSprites[2];
                        break;

                    case "Yellow":
                        collectedImage.sprite = pearSprites[3];
                        break;
                }
            }
        }       
    }
}