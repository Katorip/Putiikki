using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Manage cashline
public class CashLine : MonoBehaviour
{
    // Private variables
    private List<string> cashLineObjects = new List<string>();
    private int lineIndex = 0;

    // Add new customer in cashline
    public int AddToLineList(string lineObject)
    {
        cashLineObjects.Add(lineObject);
        GetLineOrderIndex(lineObject);
        return lineIndex;
    }

    // Remove customer from cashline
    public void RemoveFromLineList(string lineObject)
    {
        cashLineObjects.Remove(lineObject);

        // Give to the customers new line order numbers
        foreach (string line in cashLineObjects)
        {
            lineIndex = cashLineObjects.IndexOf(line);
            GameObject.Find(line).GetComponent<CustomerTarget>().GetCashLineOrder(lineIndex);
        }
    }

    // Get customers line order index
    public void GetLineOrderIndex(string lineObject)
    {
        lineIndex = cashLineObjects.IndexOf(lineObject);
    }
}
