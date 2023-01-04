using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script adds scope functionality to a weapon
[RequireComponent(typeof(AudioSource))]
public class Scope : MonoBehaviour
{
    // Reference to the gun script
    public GunScript gunscript3;

    // Reference to the camera movement script
    public CamMovement camMovement;

    // Reference to the animator component
    public Animator animator;

    // Reference to the scope object
    public GameObject snotgunScope;

    // Reference to the full HUD object
    public GameObject fullHUD;

    // Reference to the crosshair object
    public GameObject CrosshairFolder;

    // Reference to the scope camera object
    public GameObject scopeCamera;

    // Reference to the raycasting camera object
    public GameObject rayCamera;

    // Boolean to track whether the scope is currently in use
    private bool CurrentlyScoped = false;

    // Reference to the audio source component
    public AudioSource audioSource;

    // Audio clip for when the scope is activated
    public AudioClip scopeIn;

    // Audio clip for when the scope is deactivated
    public AudioClip scopeOut;

    // Function that is called when the script is first enabled
    private void Start()
    {
        // Set the raycasting camera to active and the scope camera to inactive
        rayCamera.SetActive(true);
        scopeCamera.SetActive(false);
    }

    // Function that is called when the script is disabled
    private void OnDisable()
    {
        // If the scope is currently in use
        if (CurrentlyScoped)
        {
            // Set the CurrentlyScoped boolean to unscoped
            CurrentlyScoped = !CurrentlyScoped;
            animator.SetBool("Scoped", CurrentlyScoped);
            audioSource.PlayOneShot(scopeOut, 1f);
            snotgunScope.SetActive(false);
            fullHUD.SetActive(true);
            CrosshairFolder.SetActive(true);
            rayCamera.SetActive(true);
            scopeCamera.SetActive(false);
            gunscript3.damage = 75;
            gunscript3.fireRate = 1f;
            camMovement.currentSensitivity = camMovement.unscopedSensitivity;
        }

    }

    // Update is called once per frame
    // This method handles the logic for switching between scoped and unscoped modes
    public void Update()
    {
        // Check if the "Fire2" button (typically the right mouse button) has been pressed
        if (Input.GetButtonDown("Fire2"))
        {
            // Toggle the CurrentlyScoped flag
            CurrentlyScoped = !CurrentlyScoped;
            // Update the animator with the new scoped state
            animator.SetBool("Scoped", CurrentlyScoped);

            // If we are now scoped, start the OnScoped coroutine
            if (CurrentlyScoped)
                StartCoroutine(OnScoped());
            else
                // Otherwise, call the OnUnscoped method
                OnUnscoped();
        }

        // Check if the "Fire1" button (typically the left mouse button) has been pressed while scoped
        if (Input.GetButtonDown("Fire1") && CurrentlyScoped)
        {
            // Toggle the CurrentlyScoped flag
            CurrentlyScoped = !CurrentlyScoped;
            // Update the animator with the new scoped state
            animator.SetBool("Scoped", CurrentlyScoped);
            // Call the OnUnscoped method
            OnUnscoped();
        }

        // This method is called when the weapon is unscoped
        void OnUnscoped()
        {
            // Play the scope out sound effect
            audioSource.PlayOneShot(scopeOut, 1f);
            // Disable the scope overlay
            snotgunScope.SetActive(false);
            // Enable the full HUD
            fullHUD.SetActive(true);
            // Enable the crosshair
            CrosshairFolder.SetActive(true);
            // Enable the normal camera
            rayCamera.SetActive(true);
            // Disable the scope camera
            scopeCamera.SetActive(false);
            // Zoom out
            zoomOut();
        }

        // This coroutine is called when the weapon is scoped
        IEnumerator OnScoped()
        {
            // Play the scope in sound effect
            audioSource.PlayOneShot(scopeIn, 1f);
            // Wait for .1 seconds
            yield return new WaitForSeconds(.1f);
            // Disable the normal camera
            rayCamera.SetActive(false);
            // Enable the scope camera
            scopeCamera.SetActive(true);
            // Disable the crosshair
            CrosshairFolder.SetActive(false);
            // Disable the full HUD
            fullHUD.SetActive(false);
            // Enable the scope overlay
            snotgunScope.SetActive(true);
            // Zoom in
            zoomIn();
        }

        // This method is called when the weapon is zoomed in
        void zoomIn()
        {
            // Increase the damage and decrease the fire rate
            gunscript3.damage = 150;
            gunscript3.fireRate = 0.8f;
            // Set the current sensitivity to the scoped sensitivity
            camMovement.currentSensitivity = camMovement.scopeSensitivity;
        }

        // This method is called when the weapon is zoomed out
        void zoomOut()
        {
            // Decrease the damage and increase the fire rate
            gunscript3.damage = 75;
            gunscript3.fireRate = 1f;
            // Set the current sensitivity to the unscoped sensitivity
            camMovement.currentSensitivity = camMovement.unscopedSensitivity;
        }
    }
}
