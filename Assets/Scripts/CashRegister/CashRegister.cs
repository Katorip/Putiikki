// Manages cash register functions

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashRegister : MonoBehaviour, IClickable
{
    // Public variables
    public CashPlayerTarget cashPlayerTarget;
    public CashCustomerTarget cashCustomerTarget;
    public MoneyManager moneyManager;
    public WinCondition winCondition;
    public AudioSource buySound;

    // When player clicks the cash register
    void IClickable.Click()
    {
        // If customer and player are both at the cash register
        if (cashPlayerTarget.playerTargetReached && cashCustomerTarget.customerTargetReached)
        {
            moneyManager.AddMoney();                // Collect money
            moneyManager.servedCustmers++;
            cashCustomerTarget.PaymentIsReady();    // Send customer outside of the shop
            winCondition.UpdateConditionInfo();

            // Play buy sound
            if (SettingsManager.soundOn)
            {
                buySound.Play();
            }
        }       
    }
}