using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AItakeDamage : MonoBehaviour
{
    // This enum represents the different types of collisions that can occur
    public enum CollisionType { unarmored, armored }
    // The type of collision that this object is vulnerable to
    public CollisionType bulletcollision;
    // The game object that represents the AI bot
    public GameObject bot;
    // The starting health of the AI bot
    public float Health = 150f;

    // This method is called when the AI bot takes damage
    public void TakeDamage(float amount)
    {
        // Check if the AI bot is vulnerable to unarmored collisions
        if (bulletcollision == CollisionType.unarmored)
        {
            // If so, apply double the amount of damage
            Health -= amount * 2;
        }

        // Check if the AI bot is vulnerable to armored collisions
        if (bulletcollision == CollisionType.armored)
        {
            // If so, apply the normal amount of damage
            Health -= amount;
        }

        // Check if the AI bot's health is less than or equal to 0
        if (Health <= 0f)
        {
            // If so, deactivate the AI bot game object
            bot.SetActive(false);
        }
    }
}

