using System;
using UnityEngine;
using UnityEngine.Serialization;

public class DartScript : MonoBehaviour
{
    private Camera camera;
    float startTime, endTime, swipeDistance, swipeTime;
    private Vector2 startPos;
    private Vector2 endPos;

    public float MinSwipDist = 0;
    private float dartVelocity = 0;
    private float dartSpeed = 0;
    public float maxDartSpeed = 350;
    private Vector3 angle;

    private bool thrown, holding;
    private Vector3 newPosition, resetPos;
    Rigidbody rb;

    public GameObject test;

    void Start()
    {
        camera = Camera.main;
        rb = GetComponent<Rigidbody>();
        resetPos = transform.position;
    }

    void ResetDart()
    {
        angle = Vector3.zero;
        endPos = Vector2.zero;
        startPos = Vector2.zero;
        dartSpeed = 0;
        startTime = 0;
        endTime = 0;
        swipeDistance = 0;
        swipeTime = 0;
        thrown = holding = false;
        rb.linearVelocity = Vector3.zero;
        rb.useGravity = false;
        transform.position = resetPos;
    }

    private void OnMouseDrag()
    {
        PickupDart();
    }

    private void OnMouseUp()
    {
        CalculateSpeed();
        CalculateAngle();
        rb.AddForce(new Vector3(0, 0, dartSpeed * 2));
        Invoke("ResetDart", 1f);
    }


    void PickupDart()
    {
        var mousePos = Input.mousePosition;
        mousePos.z = camera.nearClipPlane * 5f;
        newPosition = camera.ScreenToWorldPoint(mousePos);
        transform.localPosition = Vector3.Lerp(transform.localPosition, newPosition, 80f * Time.deltaTime);
    }

    private void CalculateAngle()
    {
        angle = camera.ScreenToWorldPoint(new Vector3(endPos.x, endPos.y + 50f, camera.nearClipPlane + 5));
    }

    void CalculateSpeed()
    {
        if (swipeTime > 0)
            dartVelocity = swipeDistance / (swipeDistance - swipeTime);

        dartSpeed = dartVelocity * 40;

        if (dartSpeed <= maxDartSpeed)
        {
            dartSpeed = maxDartSpeed;
        }

        swipeTime = 0;
    }

    private void OnCollisionEnter(Collision other)
    {
        // other.gameObject.GetComponent<MeshRenderer>().material.color = Color.blue;
        Destroy(other.gameObject);
    }
}