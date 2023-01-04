using UnityEngine;
using System.Collections;
using TMPro;

[RequireComponent(typeof(AudioSource))]
public class GunScript : MonoBehaviour
{
    // Fields for gun attributes
    public float damage = 10f;
    public float fireRate = 15f;
    public bool Automatic = false;
    public double magCapacity = 30;
    public double magCounter = 30;
    public double ammo = double.PositiveInfinity;
    public float timeReload = 1f;
    private bool isReloading = false;
    public Animator animator;
    public int classSelected = 5;
    public bool FMJ;
    public float multiplier = 2;

    // Fields for player character movement and behavior
    public PlayerMovement player;
    public float walkPaceSet = 10f;
    public float dropForceSet = -20f;
    public float thrustPowerSet = 3f;
    public TextMeshProUGUI currentMag;

    // Fields for other objects in the scene
    public Camera mainCamera;
    public ParticleSystem muzzleFlash;
    public GameObject objHit;
    public GameObject AR;
    public GameObject SMG;
    public GameObject Snotgun;
    public GameObject parentClassess;
    public GameObject DotCH;
    public GameObject CrossCH;
    public GameObject PlusCH;
    public GameObject parentCrosshairs;
    public GameObject dotOverlay;
    public GameObject crossOverlay;
    public GameObject plusOverlay;
    public GameObject AROverlay;
    public GameObject SMGOverlay;
    public GameObject SnotGunOverlay;
    public GameObject ARCharacter;
    public GameObject SMGCharacter;
    public GameObject SnotGunCharacter;
    public GameObject VSTEXT;

    public LayerMask wallBang;
    public AudioSource audioSource;
    public AudioSource reloadSource;
    public AudioClip shootSound;
    public AudioClip equipSound;
    public AudioClip reloadSound;
    private float FireInterval;
    [SerializeField]
    private float shotVolume = 1f;

    // Unity event function called when the object is enabled
    private void OnEnable()
    {
        // Reset some gun-related fields and variables
        isReloading = false;
        animator.SetBool("CurrentlyReloading", false);
        animator.SetBool("Healing", false);
        FireInterval = Time.time + 1f / fireRate;

        // Play an equip sound
        audioSource.PlayOneShot(equipSound, 0.5f);

        // Set the player character's movement properties
        player.walkPace = walkPaceSet;
        player.dropForce = dropForceSet;
        player.thrustPower = thrustPowerSet;
    }

    // Unity event function called every frame
    void Update()
    {
        // Update the displayed magazine count
        currentMag.text = magCounter + "";

        // Reset the "fire" animation trigger if the gun is not currently firing
        if (Time.time >= FireInterval)
        {
            animator.SetInteger("fire", -1);
        }

        // If the gun is currently reloading, return early
        if (isReloading == true)
        {
            return;
        }

        // If the magazine is empty, start reloading
        if (magCounter <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        // If the gun is automatic and the player is holding down the fire button, start firing
        if (Automatic == true)
        {
            if (Input.GetButton("Fire1") && Time.time >= FireInterval)
            {
                animator.SetInteger("fire", 2);
                FireInterval = Time.time + 1f / fireRate;
                Shoot();
            }
        }

        // If the gun is not automatic and the player just pressed the fire button, fire once
        if (Automatic == false)
        {
            if (Input.GetButtonDown("Fire1") && Time.time >= FireInterval)
            {
                animator.SetInteger("fire", 2);
                FireInterval = Time.time + 1f / fireRate;
                Shoot();
            }
        }

        // If the player presses the reload key, start reloading
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
            return;
        }
    }
    //Reload function
    IEnumerator Reload()
    {
        // Play the reload sound
        reloadSource.PlayOneShot(reloadSound, 0.4f);

        // Set the reloading flag to true
        isReloading = true;

        // Print a message to the console
        Debug.Log("Reloading");

        // Set the "CurrentlyReloading" animation trigger to true
        animator.SetBool("CurrentlyReloading", true);

        // Wait for the time defined in the "timeReload" variable
        yield return new WaitForSeconds(timeReload);

        // Set the "CurrentlyReloading" animation trigger to false
        animator.SetBool("CurrentlyReloading", false);

        // If the player has any ammo remaining
        if (ammo >= 1)
        {
            // If there is enough ammo to fully refill the magazine, use it to do so
            if (ammo >= (magCapacity - magCounter))
            {
                ammo -= (magCapacity - magCounter);
                magCounter = magCapacity;
            }
            // Otherwise, refill the magazine as much as possible with the remaining ammo
            else
            {
                magCounter += ammo;
                ammo = 0;

            }
        }
        // If the player has no ammo remaining, play the "GunReload" animation
        else
        {
            animator.Play("GunReload");
        }

        // Set the reloading flag to false
        isReloading = false;
    }

    // This function handles the shooting logic for the gun.
    void Shoot()
    {
        // Play the shooting sound effect and show the muzzle flash.
        audioSource.PlayOneShot(shootSound, shotVolume);
        muzzleFlash.Play();

        // Decrement the number of bullets left in the magazine.
        magCounter--;

        // Raycast to check if the gun has hit anything.
        RaycastHit hit;
        if (FMJ == true)
        {
            // If FMJ is enabled, the gun will ignore all objects in the wallBang layer.
            if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, (Mathf.Infinity), ~wallBang))
            {
                Debug.Log(hit.transform.name);

                // If the hit object has the AItakeDamage component, apply damage to it.
                AItakeDamage target = hit.transform.GetComponent<AItakeDamage>();
                if (target != null)
                {
                    target.TakeDamage(damage);
                }

                // Create a bullet impact effect at the point of impact.
                GameObject hitobj = Instantiate(objHit, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(hitobj, 2f);

                // Check if the hit object is one of the gun pickup objects and equip it if it is.
                EquipAR(hit.transform.name);
                EquipSMG(hit.transform.name);
                EquipSnotgun(hit.transform.name);
                EquipDotCH(hit.transform.name);
                EquipCrossCH(hit.transform.name);
                EquipPlusCH(hit.transform.name);
            }
        }
        else if (FMJ == false)
        {
            // If FMJ is disabled, the gun will check for all objects.
            if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit))
            {
                Debug.Log(hit.transform.name);

                // If the hit object has the AItakeDamage component, apply damage to it.
                AItakeDamage target = hit.transform.GetComponent<AItakeDamage>();
                if (target != null)
                {
                    target.TakeDamage(damage);
                }

                // Create a bullet impact effect at the point of impact.
                GameObject hitobj = Instantiate(objHit, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(hitobj, 2f);

                // Check if the hit object is one of the gun pickup objects and equip it if it is.
                EquipAR(hit.transform.name);
                EquipSMG(hit.transform.name);
                EquipSnotgun(hit.transform.name);
                EquipDotCH(hit.transform.name);
                EquipCrossCH(hit.transform.name);
                EquipPlusCH(hit.transform.name);
            }
        }
    }

    // These functions handle the logic for equipping a new gun.
    public void EquipAR(string name)
    {
        if (name == AR.name)
        {
            classSelected = 0;
            Debug.Log("AR equiped");
            parentClassess.SetActive(false);
            AROverlay.SetActive(true);
            ARCharacter.SetActive(true);
            VSTEXT.SetActive(true);
        }
    }

    public void EquipSMG(string name)
    {
        if (name == SMG.name)
        {
            classSelected = 1;
            Debug.Log("SMG equiped");
            parentClassess.SetActive(false);
            SMGOverlay.SetActive(true);
            SMGCharacter.SetActive(true);
            VSTEXT.SetActive(true);
        }
    }

    public void EquipSnotgun(string name)
    {
        if (name == Snotgun.name)
        {
            classSelected = 2;
            Debug.Log("Snotgun equiped");
            parentClassess.SetActive(false);
            SnotGunOverlay.SetActive(true);
            SnotGunCharacter.SetActive(true);
            VSTEXT.SetActive(true);
        }
    }

    public void EquipDotCH(string name)
    {
        if (name == DotCH.name)
        {
            dotOverlay.SetActive(true);
            parentCrosshairs.SetActive(false);
        }
    }

    public void EquipCrossCH(string name)
    {
        if (name == CrossCH.name)
        {
            crossOverlay.SetActive(true);
            parentCrosshairs.SetActive(false);
        }
    }

    public void EquipPlusCH(string name)
    {
        if (name == PlusCH.name)
        {
            plusOverlay.SetActive(true);
            parentCrosshairs.SetActive(false);
        }
    }
}
