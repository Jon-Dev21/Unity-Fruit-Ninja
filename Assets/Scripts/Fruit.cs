using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This script will be used for when the fruit is cut.
/// </summary>
public class Fruit : MonoBehaviour
{
    // Stores a prefab for the sliced fruit
    public GameObject slicedFruitPrefab;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CreateSlicedFruit();
        }
    }

    /// <summary>
    /// Create a sliced fruit whenever a fruit is sliced.
    /// </summary>
    public void CreateSlicedFruit()
    {
        // Create a sliced fruit
        GameObject inst = Instantiate(slicedFruitPrefab, transform.position, transform.rotation);

        // Add a Rigid body that does an explotion.

        // Using RigidBody Array in order to store the 2 sliced pieces of the fruit.
        Rigidbody[] SlicedRigidBodies = inst.transform.GetComponentsInChildren<Rigidbody>();

        // Iterate through the sliced fruit pieces.
        foreach (Rigidbody fruitSlice in SlicedRigidBodies)
        {
            // Add a random rotation to the sliced piece.
            fruitSlice.transform.rotation = Random.rotation;

            // Add an explosion for the pieces to go to different locations.
            // AddExplosionForce(explosionForce, explosionPosition, explosionRadius) 
            // The fruit will explode with a force between 500 and 1000. The explosion
            // Will appear in the position of the current fruit. The explosion radius will be 5.
            fruitSlice.AddExplosionForce(Random.Range(500,1000), transform.position, 5);
            
        }

        // Destroy the unsliced fruit.
        Destroy(gameObject);

        // Destroy fruit slices after 5 seconds
        Destroy(inst.gameObject, 5);
    }
}
