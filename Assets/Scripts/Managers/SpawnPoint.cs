using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    // Public variables
    public GameObject[] customerPrefabs;
    public float spawnTime = 1.0f;
    public int customerCount = 3;

    // Private variables
    private GameObject customerPrefab;
    private int i = 0;

    void Start()
    {
        // If player has bought lighting devices, increase customer count
        customerCount += IoTDeviceManager.lightingCount;

        // If player has bought robot assistant, max. customer count is 9
        if (IoTDeviceManager.robotAssistantDevice)
        {
            customerCount = 9;
        }

        // If customer count is more than 5, increase spawn time
        if (customerCount > 5)
        {
            spawnTime = 5;
        }

        // Start spawning customers
        StartCoroutine(SpawnCustomer());
    }

    // Spawn customers
    IEnumerator SpawnCustomer()
    {
        // Select one of the customer prefabs randomly
        customerPrefab = customerPrefabs[Random.Range(0, customerPrefabs.Length)];

        // Instantiate that customer prefab to spawn point
        GameObject newCustomer = Instantiate(customerPrefab, transform.position, transform.rotation);

        // Set new name to the customer target object
        newCustomer.transform.GetChild(1).name = "Target" + i;
        i++;

        // Wait before spawning new customer
        yield return new WaitForSeconds(spawnTime);

        // Spawn new customer if there are still customers left
        if (i < customerCount)
        {
            StartCoroutine(SpawnCustomer());
        }       
    }
}