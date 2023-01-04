using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StimScript : MonoBehaviour
{
    // Reference to the player movement script
    public PlayerMovement player = new PlayerMovement();

    // Reference to the animator component
    public Animator animator;

    // Reference to the UI text element that displays the current stim amount
    public TextMeshProUGUI currentMag;

    // The starting number of stims the player has
    public int stimAmount = 1;

    // Flag to keep track of whether the player is currently using a stim
    public bool isHealing = false;

    // Called when the script is enabled
    private void OnEnable()
    {
        // Play the "GunIdle" animation
        animator.Play("GunIdle");

        // Reset the isHealing flag and the corresponding animator parameter
        isHealing = false;
        animator.SetBool("Healing", false);

        // Reset the player's movement variables to their default values
        player.walkPace = 15f;
        player.dropForce = -20f;
        player.thrustPower = 5f;
    }

    // Called when the script is disabled
    private void OnDisable()
    {
        // Play the "GunIdle" animation
        animator.Play("GunIdle");

        // Reset the isHealing flag and the corresponding animator parameter
        isHealing = false;
        animator.SetBool("Healing", false);
    }

    // Update is called once per frame
    void Update()
    {
        // Update the currentMag UI text element to show the current stim amount
        currentMag.text = stimAmount + "";

        // If the player is currently using a stim, don't do anything else in this function
        if (isHealing == true)
        {
            return;
        }

        //if (Input.GetKeyDown(KeyCode.Alpha1) || (Input.GetKeyDown(KeyCode.Alpha2)) || (Input.GetKeyDown(KeyCode.Alpha3)))
        //{
        //    Debug.Log("Healing set to false");
        //    animator.SetBool("Healing", false);
        //    StopCoroutine(Delay());
        //}

        // If the player presses the Fire2 button (e.g. the right mouse button or the "Ctrl" key)
        if (Input.GetButtonDown("Fire2"))
        {
            // If the player's health is less than 150 and they are not currently using a stim
            if (player.Health < 150 && !isHealing)
            {
                // If the player has at least one stim remaining
                if (stimAmount > 0)
                {
                    // Start the stimHit function
                    stimHit();
                }
            }
        }
    }

    // Function that is called when the player uses a stim
    void stimHit()
    {
        // If the player is not currently using a stim
        if (isHealing == false)
        {
            // Start the Delay coroutine
            StartCoroutine(Delay());
        }
        else
        {
            return;
        }
    }

    IEnumerator Delay()
    {
        // Set the "Healing" animator parameter to true, which will trigger the "Healing" animation
        animator.SetBool("Healing", true);

        // Wait for 2 seconds
        yield return new WaitForSeconds(2f);

        // Set the "Healing" animator parameter to false, which will stop the "Healing" animation
        animator.SetBool("Healing", false);

        // Call the stimHeal function
        stimHeal();
    }

    // Function that is called when the healing animation finishes
    void stimHeal()
    {
        // Get the current value of the player's health
        float health = player.Health;

        // Use a switch statement to determine the appropriate action based on the current health value
        switch (health)
        {
            // If the player's health is less than 100
            case < 100:
                // Increase the player's health by 50
                player.Health += 50;
                break;

            // If the player's health is greater than or equal to 100
            case >= 100:
                // Set the player's health to 150
                player.Health = 150;
                break;
        }

        // Log a message to the console
        Debug.Log("+50 hp added");

        // If the player's health is less than or equal to 100
        if (player.Health <= 100)
        {
            // Increase the player's health by 50
            player.Health += 50;
        }
        else if (player.Health <= 100)
        {
            // This else if block does not appear to be doing anything, as it has the same condition as the previous if block
        }

        // Decrement the stimAmount variable by 1
        stimAmount--;
    }
}
