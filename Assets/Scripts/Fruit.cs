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

    /// <summary>
    /// Create a sliced fruit whenever a fruit is sliced.
    /// </summary>
    public void CreateSlicedFruit()
    {
        // Create a sliced fruit
        GameObject inst = Instantiate(slicedFruitPrefab, transform.position, transform.rotation);

        // Playing the sliced fruit sound
        FindObjectOfType<GameManager>().PlayRandomSliceSound();

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

        // Add 1 to the score.
        FindObjectOfType<GameManager>().IncreaseScore();

        // Destroy the unsliced fruit.
        Destroy(gameObject);

        // Destroy fruit slices after 5 seconds
        Destroy(inst.gameObject, 5);
    }

    /// <summary>
    /// When the blade cuts the fruit (When a collider enters the trigger),
    /// cut the fruit
    /// </summary>
    /// <param name="collision"></param>    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Get the blade component of the collision (If the blade collided with the fruit)
        Blade blade = collision.GetComponent<Blade>();

        // If there's no blade
        if(!blade)
        {
            // do nothing
            return;
        }

        CreateSlicedFruit();
    }
}
