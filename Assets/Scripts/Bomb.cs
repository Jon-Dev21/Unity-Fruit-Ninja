using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script handles when a bomb is hit by the blade.
/// </summary>
public class Bomb : MonoBehaviour
{
    /// <summary>
    /// Executes whenever a collision is detected. (When the blade hits the bomb)
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Blade blade = collision.GetComponent<Blade>();

        // If whatever hit the bomb was not the blade
        if (!blade)
        {
            // Do nothing
            return;
        }

        // Play the Bomb Explosion sound
        FindObjectOfType<GameManager>().PlayBombExplosionSound();

        // Execute the InvokeGameOver method.
        FindObjectOfType<GameManager>().InvokeGameOver();
    }
}
