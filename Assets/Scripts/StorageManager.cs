using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageManager : MonoBehaviour
{
    // Public variables
    public GameObject storageUI;

    void Start()
    {
        if (IoTDeviceManager.storageDevice)
        {
            storageUI.SetActive(true);
    }
        else
        {
            storageUI.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!IoTDeviceManager.storageDevice)
        {
            if (col.gameObject.tag == "Player")
            {
                storageUI.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (!IoTDeviceManager.storageDevice)
        {
            if (col.gameObject.tag == "Player")
            {
                storageUI.SetActive(false);
            }
        }
    }
}
