using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script used to spawn fruits from the bottom of the screen.
/// </summary>
public class Spawner : MonoBehaviour
{
    // Variable for the fruit to spawn.
    public GameObject[] fruitsToSpawn;

    // Variable for the bomb to spawn.
    public GameObject BombToSpawn;

    // List of our fruit spawners.
    public Transform[] spawners;

    // Minimum wait time for the fruit to spawn
    public float minWait = .3f;

    // Maximum wait time for the fruit to spawn
    public float maxWait = 1f;

    // Minimum force for the fruit to be spawned with
    public float minForce = 10;

    // Maximum force for the fruit to be spawned with
    public float maxForce = 17;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnFruits());
    }

    private IEnumerator SpawnFruits()
    {
        // As long as this Coroutine runs, spawn fruits
        while (true)
        {
            // If you want to do something after an amount of time,
            // Use yield return
            yield return new WaitForSeconds(Random.Range(minWait, maxWait));

            // Assign the position of our fruit in a random position of the spawn places.
            Transform t = spawners[Random.Range(0, spawners.Length)];

            // Creating a 10% probability for the bomb to spawn.
            // Create a game object for the object to spawn.
            GameObject gameObj = null;

            // Creating a random probability from 0 to 100
            float probability = Random.Range(0, 100);

            // If the probability is below 20,
            if (probability <= 20)
            {
                // Assign the game object to spawn as the bomb.
                gameObj = BombToSpawn;
            } else
            {
                // Assign the game object to spawn as any of the fruits.
                gameObj = fruitsToSpawn[Random.Range(0, fruitsToSpawn.Length)];
            }

            // Creating the fruits or bomb object (Depending on which gameObj is chosen)
            GameObject fruit = Instantiate(gameObj, t.position, t.rotation);

            // Apply force to make the fruits fly to the air
            // (Force comes from our spawners in order for the fruits to have a specific rotation)
            fruit.GetComponent<Rigidbody2D>().AddForce(t.transform.up * Random.Range(minForce, maxForce), ForceMode2D.Impulse);

            // Destroy fruits after 5 seconds
            Destroy(fruit, 5);
        }
    }


    
}
