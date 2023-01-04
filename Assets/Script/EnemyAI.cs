using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    // The NavMeshAgent component that handles movement for the enemy
    public NavMeshAgent agent;
    // A reference to the player game object
    GameObject player;
    // The amount of damage that the enemy can deal to the player
    public float damage = 10;
    // A reference to the PlayerMovement script on the player game object
    public PlayerMovement controller;

    // This method is called when the enemy game object is enabled
    void OnEnable()
    {
        // Find the player game object
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // This method is called once per frame
    void Update()
    {
        // Set the destination of the NavMeshAgent to the player's position
        agent.SetDestination(player.transform.position);
    }

    // This method is called when the enemy collides with another collider
    void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is with the player
        if (collision.transform.CompareTag("Player"))
        {
            // Decrease the player's health by the enemy's damage
            controller.Health -= damage;
            // Check if the player's health is less than or equal to 0
            if (controller.Health <= 0f)
            {
                // If so, call the player's Die method
                controller.Die();
            }
        }
    }
}

