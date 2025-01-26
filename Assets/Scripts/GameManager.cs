using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public HealthBar healthBar;
    private Door door;
    public int coinsCollected;
    public int points;
    public int maxHealth;
    public int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetSliderMaxHealth(maxHealth);
    }

    public void AddPoints(int amount)
    {
        points += amount;
    }

    public void AddCoins(int amount)
    {
        coinsCollected += amount * 10;
    }
    public void SubtractHealth(int amount)
    {
        currentHealth -= amount;
        healthBar.SetSliderHealth(currentHealth);
    }

    private void Update()
    {
        if (currentHealth == 0)
        {
            //GameOver();
        }
    }
}
