using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Public variables
    public SpawnPoint spawnPoint;
    public Text satisfactionText;

    // Private variables
    private int customerSatisfactionMaxRate;
    private float customerSatisfactionRates = 0;

    void Start()
    {
        customerSatisfactionMaxRate = spawnPoint.customerCount * 100;
        UpdateSatisfactionText();
    }

    // Update player UI satisfaction text
    private void UpdateSatisfactionText()
    {
        float average = customerSatisfactionRates / spawnPoint.customerCount;

        satisfactionText.text = "Customer satisfaction: " + average.ToString("0");
    }

    // Increase total satisfaction
    public void AddCustomerSatisfaction(float rate)
    {
        customerSatisfactionRates += rate;
        UpdateSatisfactionText();
    }
}
