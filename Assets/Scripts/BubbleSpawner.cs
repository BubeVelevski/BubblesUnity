using System.Collections.Generic;
using UnityEngine;

public class BubbleSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> listOfBubbles;
    [SerializeField] private float spawnInterval;

    void Start()
    {
        InvokeRepeating("SpawnBubble", 0, spawnInterval);
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