using UnityEngine;

public class DartScript : MonoBehaviour
{
    float startTime, endTime, swipeDistance, swipeTime;
    private Vector2 startPos;
    private Vector2 endPos;
 
    public float MinSwipDist = 0;
    private float DartVelocity = 0;
    private float DartSpeed = 0;
    public float MaxDartSpeed = 350;
    private Vector3 angle;
 
    private bool thrown, holding;
    private Vector3 newPosition, resetPos;
    Rigidbody rb;
 
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        resetPos = transform.position;
        ResetDart();
    }
 
    private void OnMouseDown()
    {
        startTime = Time.time;
        startPos = Input.mousePosition;
        holding = true;
    }
 
    private void OnMouseDrag()
    {
        PickupDart();
    }
 
    private void OnMouseUp()
    {
        endTime = Time.time;
        endPos = Input.mousePosition;
        swipeDistance = (endPos - startPos).magnitude;
        swipeTime = endTime - startTime;
 
        if (swipeTime < 0.5f && swipeDistance > 30f)
        {
            //throw ball
            CalculateSpeed();
            CalculateAngle();
            rb.AddForce(new Vector3((angle.x * DartSpeed), (angle.y * DartSpeed / 3), (angle.z * DartSpeed) * 2));
            rb.useGravity = true;
            holding = false;
            thrown = true;
            Invoke("ResetDart", 4f);
        }
        else
            ResetDart();
    }
 
    void ResetDart()
    {
        angle = Vector3.zero;
        endPos = Vector2.zero;
        startPos = Vector2.zero;
        DartSpeed = 0;
        startTime = 0;
        endTime = 0;
        swipeDistance = 0;
        swipeTime = 0;
        thrown = holding = false;
        rb.linearVelocity = Vector3.zero;
        rb.useGravity = false;
        transform.position = resetPos;
    }
 
    void PickupDart()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane * 5f;
        newPosition = Camera.main.ScreenToWorldPoint(mousePos);
        transform.localPosition = Vector3.Lerp(transform.localPosition, newPosition, 80f * Time.deltaTime);
    }
 
    private void Update()
    {
        //if (holding)
            //PickupBall();
    }
 
    private void CalculateAngle()
    {
        angle = Camera.main.ScreenToWorldPoint(new Vector3(endPos.x, endPos.y + 50f, (Camera.main.nearClipPlane + 5)));
    }
 
    void CalculateSpeed()
    {
        if (swipeTime > 0)
            DartVelocity = swipeDistance / (swipeDistance - swipeTime);
 
        DartSpeed = DartVelocity * 40;
 
        if (DartSpeed <= MaxDartSpeed)
        {
            DartSpeed = MaxDartSpeed;
        }
        swipeTime = 0;
    }
}
