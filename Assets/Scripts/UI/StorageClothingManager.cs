using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageClothingManager : MonoBehaviour
{
    public PlayerInventory playerInventory;
    public string fruitRack = "";
    public string colorRack = "";

    public void GetColor(string color)
    {
        colorRack = color;
    }

    public void GetFruit(string fruit)
    {
        fruitRack = fruit;
        playerInventory.AddClothesToInventory(fruitRack, colorRack);
    }
}
