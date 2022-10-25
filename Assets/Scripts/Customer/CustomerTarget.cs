// Manages customer behaviour

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerTarget : MonoBehaviour
{
    // Public variables
    public CashLine cashLine;
    public FittingRoomLine fittingRoomLine;
    public SpeechBubbleManager speechBubbleManager;
    public GameObject[] targetPositions;
    public GameObject[] fittingRooms;
    public ShelfManager shelfManager;
    public Transform cashTarget;
    public Transform exitPoint;
    public Transform fittingRoom;
    public int lineOrder = 0;
    public int fitRoomLineorder = 0;
    public string customerShape;
    public int fitRoomIndex = 4;

    // Private variables
    private bool isInLine = false;
    private bool isInFitLine = false;
    private bool foundFitRoom = false;
    private Transform fitTarget = null;
    private FittingRoomManager fittingRoomManager;

    void Start()
    {
        // Get all racks
        targetPositions = GameObject.FindGameObjectsWithTag("Rack");

        // Send customer to the right shape rack
        for (int i = 0; i < targetPositions.Length; i++)
        {
            if (targetPositions[i].transform.gameObject.GetComponent<ShelfManager>().rackShape == customerShape)
            {
                transform.position = targetPositions[i].transform.GetChild(0).position;
                speechBubbleManager.shelfManager = targetPositions[i].transform.gameObject.GetComponent<ShelfManager>();
            }
        }

        // Get fitting room target and fitting room line component
        fittingRoomLine = GameObject.FindGameObjectWithTag("FitLine").transform.gameObject.GetComponent<FittingRoomLine>();
        fittingRoom = GameObject.FindGameObjectWithTag("Fit").transform;
        fittingRooms = GameObject.FindGameObjectsWithTag("Fit");

        // Get cashregister target and cashline component
        cashLine = GameObject.FindGameObjectWithTag("CashLine").transform.gameObject.GetComponent<CashLine>();
        cashTarget = GameObject.FindGameObjectWithTag("Cash").transform.GetChild(1);

        // Get Spawnpoint
        exitPoint = GameObject.FindGameObjectWithTag("Exit").transform;
    }

    // Send customer to the fitting room
    public void GoToFittingRoom()
    {
        if (!isInFitLine)
        {
            fitRoomLineorder = fittingRoomLine.AddToLineList(gameObject.name);
            GetFitLineOrder(fitRoomLineorder);
            isInFitLine = true;
        }        
    }

    // Send customer to the cash register
    public void GoToCashRegister()
    {
        if (isInFitLine)
        {
            fittingRoomLine.RemoveFromLineList(gameObject.name);
        }

        if (!isInLine)
        {
            lineOrder = cashLine.AddToLineList(gameObject.name);    // Add customer to the cashline
            GetCashLineOrder(lineOrder);                            // Get customers line order
            isInLine = true;
        }
    }

    // Send customer outside of the shop
    public void ExitTheStore()
    {
        // If customer is in line, remove from it
        if (isInLine)
        {
            cashLine.RemoveFromLineList(gameObject.name);           // Remove customer from cashline
        }
        
        transform.position = exitPoint.position;
    }

    // Get customers cashlineorder and send to the line
    public void GetCashLineOrder(int orderNumber)
    {
        lineOrder = orderNumber;

        if (lineOrder == 0)
        {
            transform.position = cashTarget.position;
        }
        else
        {
            transform.position = cashTarget.position + Vector3.up * (lineOrder + 1);
        }
    }

    // Get customers fitting room lineorder and send to the line
    public void GetFitLineOrder(int orderNumber)
    {
        fitRoomLineorder = orderNumber;

        // Reset fittingroom manager
        fittingRoomManager = null;

        int i = 0;

        // Do if customer hasn't found free fitting room
        while (!foundFitRoom)
        {
            fittingRoomManager = fittingRooms[i].transform.gameObject.GetComponent<FittingRoomManager>();

            // If fitting room is not occupied
            if (!fittingRoomManager.isOccupied)
            {
                fitTarget = fittingRooms[i].transform;  // Get fitting rooms transform
                fitRoomIndex = fittingRoomManager.index;
                foundFitRoom = true;                    // Stop while loop
                fittingRoomManager.isOccupied = true;
            }

            // If fitting room is occupied
            else
            {
                fitTarget = null;
            }

            i++;

            // Stop if there is no more fitting rooms left
            if (i >= fittingRooms.Length)
            {
                foundFitRoom = true;
            }
        }
        
        // Send customer to the free fitting room
        if (fitTarget != null)
        {
            transform.position = fitTarget.position;
        }

        // Send customer to the fitting room line
        else
        {
            transform.position = fittingRoom.position + Vector3.down * (fitRoomLineorder + 1);
            foundFitRoom = false;
        }

        fittingRoomManager = null;
    }
}
