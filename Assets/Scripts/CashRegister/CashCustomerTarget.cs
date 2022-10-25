using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CashCustomerTarget : MonoBehaviour
{
    // Public variables
    public bool customerTargetReached = false;
    public SatisfactionManager satisfactionManager;
    public WinCondition winCondition;

    // When customer walks in front of cash register
    private void OnTriggerEnter2D(Collider2D col)
    {      
        if (col.gameObject.tag == "Customer")
        {
            satisfactionManager = col.gameObject.GetComponentInChildren<SatisfactionManager>();
            customerTargetReached = true;
        }
    }

    // When customer walks away from the cash register
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Customer")
        {
            winCondition.currentHappyCustomersCount += satisfactionManager.satisfactionRate;
            satisfactionManager = null;
            customerTargetReached = false;
        }
    }

    public void PaymentIsReady()
    {
        satisfactionManager.waitingInLine = false;
        satisfactionManager.SatisfactionFinal();
    }
}
