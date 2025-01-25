using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class NormalBubble : MonoBehaviour
{
    private GameManager gm;
    public BubbleData Data;
   
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GameObject destination;
    private int _hp;
    
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        rb = GetComponent<Rigidbody>();
        Invoke("Babl", Data.bablInterval);
    }

    private void Babl()
    {
        FlyToDoor();
        Invoke("Babl", Data.bablInterval);
    }
        
    private void FlyToDoor()
    {
        Vector3 dir = (destination.transform.position - transform.position).normalized;
        rb.AddForce(dir * Data.flyStrength);
    }

    private void OnCollisionEnter(Collision collision)
    {
        gm.AddPoints(1);
        gm.AddCoins(1);
        Destroy(gameObject);
    }
}

[System.Serializable]
public class BubbleData
{
    public float flyStrength;
    public float bablInterval;
}