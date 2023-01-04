using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// MonoBehaviour script that handles the player character taking damage
public class takeDamage : MonoBehaviour
{
    // Enumeration to represent the different parts of the player character's body that can be hit
    public enum CollisionType { head, body }
    
    // The current part of the player character's body that was hit by the bullet
    public CollisionType bulletcollision;

    // Reference to the player movement script
    public PlayerMovement controller;
    
    // Reference to the gun multiplier script
    public GunScript gunMultiplier;
    
    // Function to apply damage to the player character
    public void TakeDamage(float amount)
    {
        // If the player character was hit in the head
        if (bulletcollision == CollisionType.head)
        {
            // Set the gun multiplier to 2
            gunMultiplier.multiplier = 2;
        }
        // If the player character was hit in the body
        else if (bulletcollision == CollisionType.body)
        {
            // Set the gun multiplier to 1
            gunMultiplier.multiplier = 1;
        }

        // Decrease the player character's health by the specified amount, multiplied by the gun multiplier
        controller.Health -= amount * gunMultiplier.multiplier;
        
        // If the player character's health is less than or equal to 0
        if (controller.Health <= 0f)
        {
            // Call the player character's Die function
            controller.Die();
        }
    }
}

