// Manages customer funcitons at the rack

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RackTargetManager : MonoBehaviour
{
    // Public variables   
    public ShelfManager shelfManager;  
    public TMP_Text rackIndexText;
    public AudioSource correctSound;
    public AudioSource errorSound;
    public string rackTargetIndex = "";

    // Private variables
    private SatisfactionManager satisfactionManager;
    private GameObject customer;
    
    private void Start()
    {
        // Set index numbers to the rack
        rackTargetIndex = shelfManager.rackShape;
        rackIndexText.text = rackTargetIndex;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        // If customer gets in front of the rack
        if (col.gameObject.tag == "Customer" && satisfactionManager == null)
        {
            // Get that customers satisfactionmanager
            satisfactionManager = col.gameObject.GetComponentInChildren<SatisfactionManager>();
            customer = col.gameObject;

            if (satisfactionManager.customerTarget.transform.position == transform.position)
            {
                // If customer has not picked up any clothes
                if (!satisfactionManager.getClothes)
                {
                    transform.gameObject.tag = "Untagged";              // This rack is now occupied
                    satisfactionManager.wantedBox.SetActive(true);      // Show which color customer wants                  
                    satisfactionManager.wantedRack = rackTargetIndex;   // Set which rack customer picked

                    // If there are no clothes, decrease satisfaction rate
                    if (shelfManager.rackColor != satisfactionManager.wantedColor)
                    {
                        satisfactionManager.hasClothes = false;
                    }

                    // If there is clothes, stop decreasing satisfaction rate
                    else
                    {
                        AddClothes();
                    }
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        // When customer leaves from rack
        if (col.gameObject.tag == "Customer" && col.gameObject == customer)
        {                     
            satisfactionManager.wantedBox.SetActive(false);

            // If there are clothes, remove one
            if (shelfManager.rackColor == satisfactionManager.wantedColor)
            {
                shelfManager.RemoveClothes();
            }

            satisfactionManager = null;
            customer = null;
            transform.gameObject.tag = "TargetPoint"; // This rack is nolonger occupied
        }
    }

    // Player has added clothes to the rack
    public void AddClothes()
    {
        // Play sound based on what player placed in the rack
        if (satisfactionManager != null && shelfManager.rackColor == satisfactionManager.wantedColor)
        {
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

        StartCoroutine(WaitBeforeGettingCloth());
    }

    // Customer waits before getting the cloth
    IEnumerator WaitBeforeGettingCloth()
    {
        yield return new WaitForSeconds(1);

        if (satisfactionManager != null && shelfManager.rackColor == satisfactionManager.wantedColor)
        {
            satisfactionManager.hasClothes = true;
            satisfactionManager.SatisfactionUp();
        }
    }
}