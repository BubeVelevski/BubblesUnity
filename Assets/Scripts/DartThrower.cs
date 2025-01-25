using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartThrower : MonoBehaviour
{
    private GameObject Dart;
    private float startTime;
    private float endTime;
    private float swipeDistance;
    private float swipeTime;
    private Vector2 startPos;
    private Vector2 endPos;
    
    public float minSwipeDist = 0;
    private float dartVelocity = 0;
    private float dartSpeed = 0;
    public float maxDartSpeed = 350;
    private Vector3 angle;
    
    private bool thrown;
    private bool holding;
    private Vector3 newPosition;
    private Rigidbody rb;
    
    private void Start()
    {
        SetupDart();
    }
    void SetupDart()
    {
        GameObject _dart = GameObject.FindGameObjectWithTag("Player");
        Dart = _dart;
        rb = Dart.GetComponent<Rigidbody>();
        ResetDart();
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
        thrown = false;
        holding = false;
        rb.linearVelocity = Vector3.zero;
        rb.useGravity = false;
        Dart.transform.position = transform.position;
    }
    
    void PickUpDart()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane * 5f;
        newPosition = Camera.main.ScreenToWorldPoint(mousePos);
        Dart.transform.localPosition = Vector3.Lerp(Dart.transform.localPosition, newPosition, 80f * Time.deltaTime);
    }
    
    private void Update()
    {
        if (holding)
        {
            PickUpDart();
        }
        
        if (thrown)
        {
            return;
        }
    
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit _hit;
            if (Physics.Raycast(ray, out _hit, 100f))
            {
                if (_hit.transform == Dart.transform)
                {
                    startTime = Time.time;
                    startPos = Input.mousePosition;
                    holding = true;
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            endTime = Time.time;
            endPos = Input.mousePosition;
            swipeDistance = (endPos - startPos).magnitude;
            swipeTime = endTime - startTime;
            if (swipeTime < 0.5f && swipeDistance > 30f)
            {
                CalculateSpeed();
                CalculateAngle();
                rb.AddForce(new Vector3((angle.x * dartSpeed), (angle.y * dartSpeed / 3), (angle.z * dartSpeed * 2)));
                rb.useGravity = true;
                holding = false;
                thrown = true;
                Invoke("ResetDart", 4f);
            }
            else
            {
                ResetDart();
            }
        }
    }
    
    void CalculateSpeed()
    {
        if (swipeTime > 0)
        {
            dartVelocity = swipeDistance / (swipeDistance - swipeTime);
        }
    
        dartSpeed = dartVelocity * 40f;
        if (dartSpeed >= maxDartSpeed)
        {
            dartSpeed = maxDartSpeed;
        }
        if (dartSpeed <= maxDartSpeed)
        {
            dartSpeed += 40f;
        }
    
        swipeTime = 0;
    }
    
    void CalculateAngle()
    {
        angle = Camera.main.ScreenToWorldPoint(new Vector3(endPos.x, endPos.y + 50f, (Camera.main.nearClipPlane + 5f)));
    }
}
