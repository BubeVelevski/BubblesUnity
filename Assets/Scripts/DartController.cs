using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(LineRenderer))]
public class DartController : MonoBehaviour
{
    public float maxLaunchPower = 30f; // Maximum launch power
    public float minLaunchPower = 10f; // Minimum launch power
    public float powerIncreaseRate = 5f; // Rate at which the power increases
    public LineRenderer lineRenderer; // Line Renderer for the trajectory path
    private Rigidbody rb;
    private Camera mainCamera;

    private Vector3 aimStartPosition; // The position from where the dart is aimed
    private bool isAiming = false; // Track if the player is aiming
    private float launchPower = 10f; // Current launch power
    private float originalFieldOfView; // Original camera FOV
    private float zoomInFOV = 30f; // FOV when aiming (zoomed in)

    private CameraMovement cameraMovement; // Reference to CameraMovement script

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        lineRenderer = GetComponent<LineRenderer>();
        mainCamera = Camera.main;
        cameraMovement = mainCamera.GetComponent<CameraMovement>(); // Get the CameraMovement script

        // Line Renderer setup
        lineRenderer.positionCount = 0; // Initially no line
        lineRenderer.useWorldSpace = true; // Set to world space

        originalFieldOfView = mainCamera.fieldOfView;
    }

    private void Update()
    {
        // Start aiming (right mouse button down)
        if (Input.GetMouseButtonDown(1))
        {
            isAiming = true;
            aimStartPosition = transform.position; // The dart's current position
            rb.isKinematic = true; // Disable physics while aiming
            lineRenderer.positionCount = 0; // Clear the line when aiming starts

            // Inform camera to stop rotating during aiming
            cameraMovement.SetAiming(true);
        }

        // Update aiming trajectory (right mouse button held)
        if (isAiming && Input.GetMouseButton(1))
        {
            // Increase launch power based on how long the mouse button is held
            launchPower = Mathf.Clamp(minLaunchPower + Time.timeSinceLevelLoad * powerIncreaseRate, minLaunchPower, maxLaunchPower);

            // Draw trajectory path
            Vector3 direction = mainCamera.transform.forward; // Use camera's forward direction
            direction.y = 0; // Ignore the vertical component of the forward direction (no Y-axis)
            DrawPath(transform.position, direction * launchPower);

            // Zoom in and slow down camera while aiming
            mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, zoomInFOV, Time.deltaTime * 5f);
        }

        // Launch dart (right mouse button released)
        if (isAiming && Input.GetMouseButtonUp(1))
        {
            isAiming = false;
            rb.isKinematic = false; // Re-enable physics

            // Apply force to the dart based on launch power and direction
            Vector3 launchDirection = mainCamera.transform.forward; // Launch in the direction the camera is facing
            launchDirection.y = 0; // Remove the vertical component to make it flat
            rb.AddForce(launchDirection * launchPower, ForceMode.Impulse);

            // Reset camera FOV and movement speed
            mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, originalFieldOfView, Time.deltaTime * 5f);

            // Clear the trajectory line
            ClearPath();

            // Inform camera to resume rotation
            cameraMovement.SetAiming(false);
        }
    }

    // Calculate the world position of the mouse click
    private Vector3 GetMouseWorldPosition()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            return hit.point;
        }
        return Vector3.zero;
    }

    // Draw the trajectory path with the Line Renderer
    private void DrawPath(Vector3 startPosition, Vector3 velocity)
    {
        int resolution = 30; // Number of points in the path
        float timeStep = 0.1f; // Time interval between points
        float gravity = Physics.gravity.y; // Gravity value

        lineRenderer.positionCount = resolution;
        Vector3[] points = new Vector3[resolution];

        for (int i = 0; i < resolution; i++)
        {
            float time = i * timeStep;
            points[i] = CalculatePoint(startPosition, velocity, time, gravity);
        }

        lineRenderer.SetPositions(points);
    }

    // Clear the trajectory line when not aiming
    private void ClearPath()
    {
        lineRenderer.positionCount = 0;
    }

    // Calculate the position of a point on the trajectory based on physics
    private Vector3 CalculatePoint(Vector3 startPosition, Vector3 velocity, float time, float gravity)
    {
        float x = startPosition.x + velocity.x * time;
        float y = startPosition.y + velocity.y * time + 0.5f * gravity * time * time;
        float z = startPosition.z + velocity.z * time;

        return new Vector3(x, y, z);
    }
}
