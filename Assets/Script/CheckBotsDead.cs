using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBotsDead : MonoBehaviour
{
    // References to the Player, ChaserPlayer, and Chaser game objects
    public GameObject Player;
    public GameObject ChaserPlayer;
    public GameObject Chaser;

    // This method is called when the MoveToGlitch function is called
    public void MoveToGlitch()
    {
        // Log a message to the console
        Debug.Log("Level 2");
        // Destroy the Player game object
        Destroy(Player);
        // Enable the ChaserPlayer and Chaser game objects
        ChaserPlayer.SetActive(true);
        StartCoroutine(SpawnGlitch());
        
    }

    public IEnumerator SpawnGlitch()
    {
        yield return new WaitForSeconds(2);
        Chaser.SetActive(true);
    }
    // This method is called every frame
    private void Update()
    {
        // Call the CheckChildObjects function
        CheckChildObjects();
    }

    // This method iterates through the child objects of this game object and checks if they are all disabled
    private void CheckChildObjects()
    {
        // Flag to track whether all child objects are disabled
        bool allDisabled = true;

        // Iterate through the child objects of this game object
        foreach (Transform child in transform)
        {
            // If a child object is enabled, set the flag to false and break out of the loop
            if (child.gameObject.activeSelf)
            {
                allDisabled = false;
                break;
            }
        }

        // If the flag is still true, call the MoveToGlitch function
        if (allDisabled)
        {
            MoveToGlitch();
        }
    }
}
