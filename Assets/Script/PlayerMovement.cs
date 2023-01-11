using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    // Manages these attributes of the Player:
    //- Determines if player is alive or not
    public bool playerDead;
    // Manages these attributes of the Player:
    //- health
    //- movement speed
    //- gravity and limits the height of the players thrust from the floor when jumping
    public float Health = 150f;
    public float MaxHealth = 150f;
    public float walkPace = 10f;
    public float dropForce = -10f;
    public float thrustPower = 3f;

    // Sets private variables to check for players contact with floor
    [SerializeField]
    private float floorTolerance = 0.5f;
    [SerializeField]
    private Transform floorDetect;
    [SerializeField]
    private LayerMask floorLayer;

    // Links with main player to :
    //- allow control of the game object
    //- alter the velocity of its "Vector3" allowing it to change its position
    //- therefore allowing it to move 
    public GameObject Player;
    public Image HealthFill;
    public TextMeshProUGUI CurrentHealth;
    private Vector3 speed;
    //Creates a bool to save the status of the characters relationship to the floor regularly and locally to the script
    private bool onFloor;
    private CharacterController player;
    //Saves players kills
    public int kills = 0;
    [SerializeField]
    GameObject deathScreen;

    // Activated whenever Script is enabled:
    //- Sets player as short cut to alter CharacterController put on the game object for player
    //- locks players cursor to screen and makes it invisible to allow full 360+ head movement
    private void Awake()
    {
        player = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Activated every fixed frame of framerate, allowing more frequent updates:
    //- Sets player as short cut to alter CharacterController put on the game object for player
    private void Update()
    {
        //calls subroutines for gravity and movement of the player as it is input
        Gravity();
        Movement();
        HealthBar();
    }

    private void Movement()
    {
        //Sets value of players position horizontally and vertically on the axis as floats
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");


        Vector3 move = transform.right * horizontal + transform.forward * vertical;

        //Sets speed of player
        player.Move(Time.deltaTime * walkPace * move);

        //Lets player jump if on floor + lets player climb items on floorLayer
        if (Input.GetButtonDown("Jump") && onFloor || Input.GetButton("Jump") && onFloor)
        {
            speed.y = Mathf.Sqrt(thrustPower * -1f * dropForce);
        }
    }

    private void Gravity()
    {
        //Sets gravity therefore allowing the code to alter the velocity at which player drops to floor when "OnFloor"
        //is false

        onFloor = Physics.CheckSphere(floorDetect.position, floorTolerance, floorLayer);

		if (onFloor && speed.y < 0)
		{
			speed.y = -2f;
		}
		speed.y += dropForce * Time.deltaTime;
		player.Move(speed * Time.deltaTime);
	}
	//Subroutine called when player health is less than or equal to zero, causing players death
	public void Die()
	{
		Destroy(Player);
        Cursor.lockState = CursorLockMode.None;
        deathScreen.SetActive(true);
    }
    //Updates HealthBar GUI
	public void HealthBar()
	{
		CurrentHealth.text = Health + "";
		HealthFill.fillAmount = Health / MaxHealth;
	}
}
