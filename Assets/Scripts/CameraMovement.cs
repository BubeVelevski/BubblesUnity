using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;  // The object that the camera will follow (your dart character)
    public float rotationSpeed = 5f; // Speed of camera rotation
    public float zoomSpeed = 2f; // Zoom speed for the camera
    public float minZoom = 5f; // Minimum zoom level
    public float maxZoom = 20f; // Maximum zoom level
    public float defaultDistance = 10f; // Default distance between camera and character
    private float currentZoom; // The current zoom level

    private float currentRotationX = 0f;
    private float currentRotationY = 0f;

    private Camera mainCamera;

    private bool isAiming = false; // Track if the player is aiming

    private void Start()
    {
        mainCamera = Camera.main;
        currentZoom = defaultDistance; // Set the initial zoom distance
    }

    private void Update()
    {
        if (!isAiming)
        {
            // Get mouse input for normal camera rotation
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            // Update camera rotation based on mouse movement (X-axis only)
            currentRotationX += mouseX * rotationSpeed;
            // Restrict vertical movement (Y-axis)
            currentRotationY = Mathf.Clamp(currentRotationY - mouseY * rotationSpeed, -30f, 30f); // Limit vertical camera rotation

            // Apply the calculated rotation to the camera's position
            Quaternion rotation = Quaternion.Euler(currentRotationY, currentRotationX, 0);
            Vector3 direction = rotation * Vector3.back; // Look in the direction the camera is facing
            Vector3 targetPosition = target.position + direction * currentZoom;

            // Apply smooth zoom based on mouse scroll input
            currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
            currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

            // Apply the new camera position and look at the target
            mainCamera.transform.position = targetPosition;
            mainCamera.transform.LookAt(target.position);
        }
    }

    // Set whether the player is aiming (prevents camera rotation while aiming)
    public void SetAiming(bool aiming)
    {
        isAiming = aiming;
    }
}
