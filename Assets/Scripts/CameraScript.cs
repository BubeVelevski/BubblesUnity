using System;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private float cameraSpeed;
    [Range(0f, 1f)] [SerializeField] private float cameraLimiterMin;
    [Range(0f, 1f)] [SerializeField] private float cameraLimiterMax;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void Update()
    {
        Vector2 mousePos = Input.mousePosition;
        Vector3 dir = Vector3.zero;
        if (mousePos.y > Screen.height * cameraLimiterMax)
        {
            dir = Vector3.forward;
        }
        else if (mousePos.y < Screen.height * cameraLimiterMin)
        {
            dir = Vector3.back;
        }
        if (mousePos.x > Screen.width * cameraLimiterMax)
        {
            dir = Vector3.right;
        }
        else if (mousePos.x < Screen.width * cameraLimiterMin)
        {
            dir = Vector3.left;
        }
        transform.position += dir * cameraSpeed * Time.deltaTime;
    }
}
