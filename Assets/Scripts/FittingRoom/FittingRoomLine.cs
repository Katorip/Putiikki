// Manages fitting room line

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FittingRoomLine : MonoBehaviour
{
    // Private variables
    private List<string> fitRoomLineObjects = new List<string>();
    private int lineIndex = 0;

    // Add new customer in fitting room line
    public int AddToLineList(string lineObject)
    {
        fitRoomLineObjects.Add(lineObject);
        GetLineOrderIndex(lineObject);
        return lineIndex;
    }

    // Remove customer from fitting room line
    public void RemoveFromLineList(string lineObject)
    {
        fitRoomLineObjects.Remove(lineObject);

        // Give to the customers new line order numbers
        foreach (string line in fitRoomLineObjects)
        {
            lineIndex = fitRoomLineObjects.IndexOf(line);
            GameObject.Find(line).GetComponent<CustomerTarget>().GetFitLineOrder(lineIndex);
        }
    }

    // Get customers line order index
    public void GetLineOrderIndex(string lineObject)
    {
        lineIndex = fitRoomLineObjects.IndexOf(lineObject);
    }
}
