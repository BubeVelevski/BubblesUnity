using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class DartThrower : MonoBehaviour
{
    public GameObject hoop;
    private Rigidbody rigidbody;
    private Vector3 screenPoint;
    private Vector3 offset;
    private Vector3 oldMouse;
    private Vector3 mouseSpeed;
    public float speed = 5;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void OnMouseDown()
    {
        oldMouse = Input.mousePosition;
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }


    void OnMouseDrag()
    {
        var curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) - offset;
        transform.position = curPosition;

    }

    void OnMouseUp()
    {
        mouseSpeed = (oldMouse - Input.mousePosition);
        rigidbody.AddForce(mouseSpeed * speed * -1, ForceMode.Force);

    }
}
