using UnityEngine;

public class StationaryCamera : MonoBehaviour
{
    public float lookSpeed = 2f;       // Speed of mouse look
    public float lookUpLimit = -80f;  // Limit for looking up
    public float lookDownLimit = 80f; // Limit for looking down

    private float rotationX = 0f;     // Vertical rotation value (up/down)
    private float rotationY = 0f;     // Horizontal rotation value (left/right)

    void Start()
    {
        // Lock and hide the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * lookSpeed; // Horizontal mouse movement
        float mouseY = Input.GetAxis("Mouse Y") * lookSpeed; // Vertical mouse movement

        // Update rotation values
        rotationY += mouseX;             // Horizontal rotation (left/right)
        rotationX -= mouseY;             // Vertical rotation (up/down)
        rotationX = Mathf.Clamp(rotationX, lookUpLimit, lookDownLimit); // Clamp vertical rotation

        // Apply the rotation to the camera
        transform.rotation = Quaternion.Euler(rotationX, rotationY, 0);
    }
}