using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMovement : MonoBehaviour
{
    //- Camera sensitivity
    //- Scoped and Unscoped camera sensitivity (current sensitivity is a place holder which is set equal to which ever
    //  sensitivity is currently being utilized)
    // Declares variables for the sensitivity of the camera and the x and y axes.
    public float currentSensitivity = 100f;
    public float unscopedSensitivity = 100f;
    public float scopeSensitivity = 100f;

    // Creates a reference to the y axis transform. 
    [SerializeField]
    private Transform yAxis;

    // Declares a variable for the x axis. 
    private float xAxis = 0;

    // Activated whenever the script is enabled: 
    // - Locks the cursor to the screen and makes it invisible to allow full 360+ head movement. 
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Activated every frame: 
    // - Calls the mouseMovement() subroutine. 
    private void Update()
    {
        mouseMovement();
    }

    // Manages the rotation of the camera on the x and y axes using mouse input. 
    private void mouseMovement()
    {
        // Declares variables for the mouse input on the x and y axes, adjusted for sensitivity and time. 
        float mouseX = Input.GetAxis("Mouse X") * currentSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * currentSensitivity * Time.deltaTime;

        // Subtracts mouseY from xAxis and clamps the value between -90 and 90. 
        xAxis -= mouseY;
        xAxis = Mathf.Clamp(xAxis, -90f, 90f);

        // Rotates the camera's transform on the x axis based on xAxis. 
        transform.localRotation = Quaternion.Euler(xAxis, 0, 0);

        // Rotates the y axis transform on the y axis based on mouseX. 
        yAxis.Rotate(Vector3.up * mouseX);
    }
}

