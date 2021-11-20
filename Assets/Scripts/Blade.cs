using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script sets the blade object to follow the mouse pointer in a 2d space
/// </summary>
public class Blade : MonoBehaviour
{
    // Minimum velocity of the blade
    public float minVelocity = 0.1f;

    // A 3D vector of the last position of the mouse.
    private Vector3 lastMousePosition;

    // A 3D vector of the mouse velocity
    private Vector3 mouseVelocity;

    // A 2D collider. Should only be active when the mouse is moving
    private Collider2D collider2d;

    // Rigid body of the blade.
    private Rigidbody2D bladeRigidBody;

    private void Awake()
    {
        // Initialize the blade rigid body.
        bladeRigidBody = GetComponent<Rigidbody2D>();

        // Initializing Collider
        collider2d = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {    
        // Setting the blade to the mouse position on each frame.
        SetBladeToMouse();
    }

    /// <summary>
    /// The code in this method prevents the fruit (or bomb)
    /// from being cut whenever the mouse is not moving.
    /// </summary>
    private void FixedUpdate()
    {
        // Enable and disable the collider2d if the mouse is moving.
        collider2d.enabled = IsMouseMoving();
    }

    /// <summary>
    /// Sets the blade position to the mouse's position.
    /// </summary>
    private void SetBladeToMouse()
    {
        // Getting the mouse position.
        // Since the mouse position is a 3d unit and our game is in 2d,
        // we need to set the z value to the distance from the camera.
        var mousePos = Input.mousePosition;
        mousePos.z = 10; // Distance of 10 units from the camera.
        
        // Setting the blade position to the mouse position.
        bladeRigidBody.position = Camera.main.ScreenToWorldPoint(mousePos);
    }

    /// <summary>
    /// Method that returns true if mouse is moving. 
    /// Else, returns false
    /// </summary>
    /// <returns></returns>
    private bool IsMouseMoving()
    {
        Vector3 currentMousePosition = transform.position;

        // Calculate how much the mouse has traveled
        float traveled = (lastMousePosition - currentMousePosition).magnitude;

        // Set the last mouse position to the current mouse position
        lastMousePosition = currentMousePosition;

        // If the mouse has traveled enough distance, return true.
        // Else, return false
        // The distance would be our minimum velocity in this case.
        if (traveled > minVelocity)
            return true;
        else
            return false;
    }
}
