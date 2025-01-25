using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Door door;
    public int coinsCollected;
    public int points;
    public int health;

    private void Start()
    {
        door = FindObjectOfType<Door>();
    }

    public void AddPoints(int amount)
    {
        points += amount;
        Debug.Log($"Points: {points}");
    }

    public void AddCoins(int amount)
    {
        coinsCollected += amount;
        Debug.Log($"Coins Collected: {coinsCollected}");
    }
    public void SubtractHealth(int amount)
    {
        health -= amount;
        Debug.Log($"Health: {health - amount}");
    }
}
