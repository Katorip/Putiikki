// Manages customers satisfaction rate

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SatisfactionManager : MonoBehaviour
{
    // Public variables
    public Customer customer;
    public CustomerTarget customerTarget;
    public WinCondition winCondition;
    public SAP2D.SAP2DAgent agent;
    public TMP_Text rateText;
    public TMP_Text wantedText;
    public GameObject wantedBox;
    public float satisfactionRate = 100.0f;
    public float satisfactionRateUpCount = 5.0f;
    public int waitInLineTime = 3;
    public string wantedRack;
    public bool hasClothes = true;
    public bool waitingInLine = false;
    public bool getClothes = false;
    public bool needFitting;
    public bool waitingInFittingRoom = false;
    public bool decreaseSatisfaction = false;
    public bool hasVacuumCleaner = false;
    public int satisfactionDecreasingTime = 1;

    public string fruit;
    public string wantedColor;
    private List<string> wantedColors = new List<string>();
    public Sprite[] colorSprites = new Sprite[4];
    public SpriteRenderer wantedColorSprite;

    // Private variables
    private bool nextToRack = false;
    private bool waiting = false;

    void Start()
    {
        // If player has bought lighting devices, decrease satisfaction decreasing time
        if (IoTDeviceManager.lightingCount > 1)
        {
            satisfactionDecreasingTime += IoTDeviceManager.lightingCount;
        }

        // If player has bought robot vacuum cleaner, set bool to true
        if (IoTDeviceManager.robotVacuumCleanerDevice)
        {
            hasVacuumCleaner = true;
        }

        // Set win condition script
        winCondition = GameObject.FindGameObjectWithTag("Win").transform.gameObject.GetComponent<WinCondition>();

        // Set available cloth colors to the list
        wantedColors.Add("Blue");
        wantedColors.Add("Turquoise");
        wantedColors.Add("Red");
        wantedColors.Add("Yellow");

        // Choose randomly what color customer is looking for and remove it from the list of available colors
        int sizeIndex = UnityEngine.Random.Range(0, wantedColors.Count);
        wantedColor = wantedColors[sizeIndex];
        wantedColors.RemoveAt(sizeIndex);

        SetWantedSprite();

        // Choose randomly if customer wants to fit the cloth
        needFitting = UnityEngine.Random.value > 0.5f;

        // Set texts and text box
        UpdateRateText();
        wantedBox.SetActive(false);
        wantedText.text = wantedColor;
    }

    // Update is called once per frame
    void Update()
    {
        // If customer is next to the rack and there are no clothes, decrease satisfactionrate
        if (!hasClothes && !agent.isMoving && !getClothes)
        {
            decreaseSatisfaction = true;

            if (!nextToRack)
            {
                StartCoroutine(SatisfactionDown());
                nextToRack = true;
            }
        }

        // If customer has to wait in line or fitting room, decrease satisfaction rate
        if (waitingInLine || waitingInFittingRoom)
        {
            decreaseSatisfaction = true;

            if (!waiting)
            {
                StartCoroutine(SatisfactionDown());
                waiting = true;
            }

        }

        // Stop decreasing satisfaction rate if player has bought robot vacuum cleaner and satisfaction rate is too low
        if (hasVacuumCleaner)
        {
            if (satisfactionRate <= 10)
            {
                decreaseSatisfaction = false;
                waitingInLine = false;
            }          
        }

        // If customer don't want to wait anymore, send customer to the exit
        if (satisfactionRate <= 0)
        {
            hasClothes = true;
            getClothes = true;
            waitingInLine = false;
            waitingInFittingRoom = false;
            satisfactionRate -= 10;
            customerTarget.ExitTheStore();
        }
    }

    // Increase satisfaction rate and send customer to the next location
    public void SatisfactionUp()
    {
        satisfactionRate += satisfactionRateUpCount;
        getClothes = true;
        decreaseSatisfaction = false;
        waiting = false;
        UpdateRateText();

        // Send customer to the fitting room
        if (needFitting)
        {
            customerTarget.GoToFittingRoom();
            StartCoroutine(GoWaitInFitLine());
        }
        // Send customer to the cash register
        else
        {
            customerTarget.GoToCashRegister();
            StartCoroutine(GoWaitInLine());
        }
    }
    
    public IEnumerator SatisfactionDown()
    {
        while (decreaseSatisfaction)
        {
            yield return new WaitForSeconds(satisfactionDecreasingTime);

            satisfactionRate--;
            UpdateRateText();
        }
    }

    // Update satisfaction rate text
    public void UpdateRateText()
    {
        rateText.text = satisfactionRate.ToString("0");
    }

    // Increase satisfaction rate and send customer outside of the store
    public void SatisfactionFinal()
    {
        satisfactionRate = satisfactionRate + satisfactionRateUpCount;
        decreaseSatisfaction = false;
        UpdateRateText();
        customerTarget.ExitTheStore();
        winCondition.currentSatisfaction += satisfactionRate;
        winCondition.UpdateConditionInfo();
    }

    // Wait in line before before decreasing satisfaction rate
    IEnumerator GoWaitInLine()
    {
        yield return new WaitForSeconds(waitInLineTime);
        waitingInLine = true;
    }

    // Wait in fittingroom line before before decreasing satisfaction rate
    IEnumerator GoWaitInFitLine()
    {
        yield return new WaitForSeconds(waitInLineTime);
        waitingInFittingRoom = true;
    }

    // Choose randomly next color customer wants to fit
    public void ChangeWantedSize()
    {
        // Do it only if customer has already picked up cloth
        if (getClothes == true)
        {
            int sizeIndex = UnityEngine.Random.Range(0, wantedColors.Count);
            wantedColor = wantedColors[sizeIndex];
        }
    }

    // Set wanted color sprite
    private void SetWantedSprite()
    {
        switch (wantedColor)
        {
            case "Blue":
                wantedColorSprite.sprite = colorSprites[0];
                break;

            case "Turquoise":
                wantedColorSprite.sprite = colorSprites[1];
                break;

            case "Red":
                wantedColorSprite.sprite = colorSprites[2];
                break;

            case "Yellow":
                wantedColorSprite.sprite = colorSprites[3];
                break;
        }
    }
}