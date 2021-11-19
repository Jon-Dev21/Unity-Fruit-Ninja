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
        Instantiate(slicedFruitPrefab, transform.position, transform.rotation);

        // Destroy the unsliced fruit.
        Destroy(gameObject);
    }
}
