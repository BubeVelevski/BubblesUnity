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
    public BubbleData Data;
    public int hp
    {
        get
        {
            return _hp;
        }
        set
        {
            _hp = value;
            hpSlider.maxValue = Data.hpMax;
            hpSlider.value = _hp;
            if (_hp <= 0)
            {
                Destroy(gameObject);
            } 
            else if (_hp > Data.hpMax) 
            {
                _hp = Data.hpMax;
            }
        }
    }
    [SerializeField] private Slider hpSlider;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GameObject destination = new GameObject();
    private int _hp;

    private void HealthCheck()
    {
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        hp = Data.hpMax;
        MoveFunction();
    }

    private void MoveFunction()
    {
        Vector3 dir = (destination.transform.position - transform.position).normalized;
        rb.AddForce(dir * Data.flyStrength);
    }
    


    // Update is called once per frame
    void Update()
    {
        HealthCheck();
    }
}

[System.Serializable]
public class BubbleData
{
    public float flyStrength;
    public int hpMax;
    public float SsearchRadius;
}