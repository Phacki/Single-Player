using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// MonoBehaviour script that handles weapon swapping in the game
public class WeaponSwapV2 : MonoBehaviour
{
    // Opacity value for when a weapon is selected
    public float opacitySelected = 1;

    // Opacity value for when a weapon is unselected
    public float opacityUnselected = 0.25f;

    // Reference to the gun script
    public GunScript gScript = new GunScript();

    // Integer representing the currently selected weapon (0 = primary weapon, 1 = secondary weapon, 2 = stim)
    int itemHeld = 1;

    // Integer representing the currently selected class
    int classNo;

    // GameObject references for the different primary weapons
    public GameObject assaultRifle;
    public GameObject subMachineGun;
    public GameObject snotGun;

    // GameObject references for the secondary weapon and stim item
    public GameObject secondary;
    public GameObject stimShot;

    // GameObject references for the secondary weapon and stim item overlays
    public GameObject secondaryOverlay;
    public GameObject stimOverlay;

    // CanvasGroup for the primary weapons
    public CanvasGroup primarys;

    // Function that is called when the script is first enabled
    private void Start()
    {
        // Set the alpha value of the primary weapons CanvasGroup to the unselected value
        primarys.alpha = opacityUnselected;
    }

    // Function that is called every frame
    void Update()
    {
        // If the player pressed the "1" key
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // If the currently selected weapon is not the primary weapon
            if (itemHeld != 0)
            {
                // Set the alpha value of the primary weapons CanvasGroup to the selected value
                primarys.alpha = opacitySelected;

                // Deactivate the secondary weapon and stim item overlays
                secondaryOverlay.SetActive(false);
                stimOverlay.SetActive(false);

                // Set the itemHeld variable to 0 (primary weapon)
                itemHeld = 0;

                // Call the checkSwitch function
                checkSwitch();
            }
        }

        // If the player pressed the "2" key
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            // If the currently selected weapon is not the secondary weapon
            if (itemHeld != 1)
            {
                // Set the alpha value of the primary weapons CanvasGroup to the unselected value
                primarys.alpha = opacityUnselected;

                // Activate the secondary weapon overlay
                secondaryOverlay.SetActive(true);

                // Deactivate the stim item overlay
                stimOverlay.SetActive(false);

                // Call the checkSwitch function
                checkSwitch();

                // Set the itemHeld variable to 1 (secondary weapon)
                itemHeld = 1;

                // Call the checkSwitch function
                checkSwitch();
            }
        }

        // If the player pressed the "3" key
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            // If the currently selected weapon is not the stim item
            if (itemHeld != 2)
            {
                // Set the alpha value of the primary weapons CanvasGroup to the unselected value
                primarys.alpha = opacityUnselected;

                // Deactivate the secondary weapon overlay
                secondaryOverlay.SetActive(false);

                // Activate the stim item overlay
                stimOverlay.SetActive(true);

                // Set the itemHeld variable to 2 (stim item)
                itemHeld = 2;

                // Call the checkSwitch function
                checkSwitch();
            }
        }
    }

    // Function that activates or deactivates the different weapons based on the PorS parameter
    void gunSwitch(int PorS)
    {
        // Set the classNo variable to the currently selected class
        classNo = gScript.classSelected;

        // If the PorS parameter is 0 (primary weapon)
        if (PorS == 0)
        {
            // Switch statement to activate the appropriate primary weapon based on the currently selected class
            switch (classNo)
            {
                case 0:
                    // Activate the assault rifle
                    assaultRifle.SetActive(true);
                    break;
                case 1:
                    // Activate the submachine gun
                    subMachineGun.SetActive(true);
                    break;
                case 2:
                    // Activate the snot gun
                    snotGun.SetActive(true);
                    break;
            }

            // Deactivate the secondary weapon and stim item
            secondary.SetActive(false);
            stimShot.SetActive(false);

        }

        if (PorS == 1)
        {
            // Switch statement to deactivate the appropriate primary weapon based on the currently selected class
            switch (classNo)
            {
                case 0:
                    // Deactivate the assault rifle
                    assaultRifle.SetActive(false);
                    break;
                case 1:
                    // Deactivate the submachine gun
                    subMachineGun.SetActive(false);
                    break;
                case 2:
                    // Deactivate the snot gun
                    snotGun.SetActive(false);
                    break;
            }

            // Activate the secondary weapon and deactivate the stim item
            secondary.SetActive(true);
            stimShot.SetActive(false);

        }

        // If the PorS parameter is 2 (stim item)
        if (PorS == 2)
        {
            // Switch statement to deactivate the appropriate primary weapon based on the currently selected class
            switch (classNo)
            {
                case 0:
                    // Deactivate the assault rifle
                    assaultRifle.SetActive(false);
                    break;
                case 1:
                    // Deactivate the submachine gun
                    subMachineGun.SetActive(false);
                    break;
                case 2:
                    // Deactivate the snot gun
                    snotGun.SetActive(false);
                    break;
            }

            // Deactivate the secondary weapon and activate the stim item
            secondary.SetActive(false);
            stimShot.SetActive(true);
        }
    }

    // Function to check which item is currently held and call the gunSwitch function with the appropriate parameter
    void checkSwitch()
    {
        switch (itemHeld)
        {
            case 0:
                // Call gunSwitch with 0 (primary weapon)
                gunSwitch(0);
                break;
            case 1:
                // Call gunSwitch with 1 (secondary weapon)
                gunSwitch(1);
                break;
            case 2:
                // Call gunSwitch with 2 (stim item)
                gunSwitch(2);
                break;
        }
    }
}
