using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script sets the blade object to follow the mouse pointer in a 2d space
/// </summary>
public class Blade : MonoBehaviour
{
    private Rigidbody2D bladeRigidBody;

    private void Awake()
    {
        // Initialize the blade rigid body.
        bladeRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        SetBladeToMouse();
    }

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
}
