// Manages exit point

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPointManager : MonoBehaviour
{
    // Public variables
    public SatisfactionManager satisfactionManager;
    public LevelLoadManager levelLoadManager;
    public WinCondition winCondition;
    public UIManager uiManager;
    public SpawnPoint spawnPoint;

    // Private variables
    private float customerSatisfactionRate;
    private int customerCount;

    private void Start()
    {
        customerCount = spawnPoint.customerCount;

        // If player has bought lighting devices, increase customer count
        customerCount += IoTDeviceManager.lightingCount;

        // If player has bought robot assistant, max. customer count is 9
        if (IoTDeviceManager.robotAssistantDevice)
        {
            customerCount = 9;
        }
    }

    // When customer goes to exit point
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Customer")
        {
            // Add customers satisfaction rate to the satisfaction counter
            satisfactionManager = col.gameObject.GetComponentInChildren<SatisfactionManager>();
            customerSatisfactionRate = satisfactionManager.satisfactionRate;
            uiManager.AddCustomerSatisfaction(customerSatisfactionRate);
            satisfactionManager = null;
            Destroy(col.gameObject);
            customerCount--;

            if (customerCount <= 0)
            {
                winCondition.countdownTime = 0;
            }
        }
    }
}
