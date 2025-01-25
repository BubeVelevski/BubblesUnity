using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> listOfBubbles;
    [SerializeField] private float spawnInterval;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("SpawnBubble", spawnInterval, spawnInterval);
    }

    private void SpawnBubble()
    {
        Vector3 spawnPosition = transform.position;
        if (listOfBubbles != null && listOfBubbles.Count > 0)
        {
            int randomBubble = Random.Range(0, listOfBubbles.Count);
            Instantiate(listOfBubbles[randomBubble], spawnPosition, Quaternion.identity, transform);
        }
    }
}
