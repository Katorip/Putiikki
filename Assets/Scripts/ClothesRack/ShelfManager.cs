// Manages clothes racks

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShelfManager : MonoBehaviour, IClickable
{
    // Public variables
    public RackTargetManager rackTargetManager;
    public PlayerInventory playerInventory;
    public TMP_Text colorInRackText;
    public AudioSource correctSound;
    public AudioSource errorSound;
    public bool playerTargetReached = false;
    public string rackShape;
    public string rackColor;
    public SpriteRenderer wantedColorIcon;
    public Sprite[] wantedColorSprites = new Sprite[4];
    public bool isOccupied;

    private void Start()
    {
        // Initialize shelf data and randomly choose first cloth color 
        colorInRackText.text = "";
        wantedColorIcon.sprite = wantedColorSprites[UnityEngine.Random.Range(0, wantedColorSprites.Length)];
        rackColor = wantedColorIcon.sprite.name;
        isOccupied = false;
    }

    // When player clicks the clothesrack when next to it
    void IClickable.Click()
    {
        ClickToAddClothes();
    }

    // Add clothes to the rack
    public void AddClothes(string color)
    {
        rackColor = color;
        colorInRackText.text = color;

        switch (rackColor)
        {
            case "Blue":
                wantedColorIcon.sprite = wantedColorSprites[0];
                break;

            case "Turquoise":
                wantedColorIcon.sprite = wantedColorSprites[1];
                break;

            case "Red":
                wantedColorIcon.sprite = wantedColorSprites[2];
                break;

            case "Yellow":
                wantedColorIcon.sprite = wantedColorSprites[3];
                break;
        }
    }

    // Remove certain color of cloth from the rack
    public void RemoveClothes()
    {
        rackColor = "";
        colorInRackText.text = "";
        wantedColorIcon.sprite = null;
    }

    public void ClickToAddClothes()
    {
        if (playerTargetReached && rackShape == playerInventory.shapeRack)
        {
            AddClothes(playerInventory.colorRack);
            rackTargetManager.AddClothes();
            playerInventory.ResetInventory();
        }
        else if (playerTargetReached && rackShape != playerInventory.shapeRack)
        {
            if (SettingsManager.soundOn)
            {
                errorSound.Play();
            }
        }
    }
}
