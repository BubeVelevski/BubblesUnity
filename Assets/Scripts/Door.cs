using System;
using Unity.VisualScripting;
using UnityEngine;

public class Door : MonoBehaviour
{
    // private void OnCollisionEnter(Collision other)
    // {
    //     health--;
    //     Destroy(other.gameObject);
    // }

    private void OnTriggerEnter(Collider other)
    {
        // other.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
        Destroy(other.gameObject);
    }
    
}
