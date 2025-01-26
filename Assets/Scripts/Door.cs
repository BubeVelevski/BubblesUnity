using System;
using Unity.VisualScripting;
using UnityEngine;

public class Door : MonoBehaviour
{
    private GameManager gm;
    // private void OnCollisionEnter(Collision other)
    // {
    //     health--;
    //     Destroy(other.gameObject);
    // }

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Normal")
        {
            gm.SubtractHealth(1);
            // other.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "Heavy")
        {
            gm.SubtractHealth(2);
            // other.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
            Destroy(other.gameObject);
        }
    }
}
