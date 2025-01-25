using System;
using Unity.VisualScripting;
using UnityEngine;

public class Door : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        //health--;
        Destroy(other.gameObject);
    }
    
}
