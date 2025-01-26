using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject bubbleSpanwer;
    public GameObject gameOverCanvas;
    public HealthBar healthBar;
    private Door door;
    public int coinsCollected;
    public int points;
    public int maxHealth;
    public int currentHealth;
    

    private void Start()
    {
        Time.timeScale = 1;
        currentHealth = maxHealth;
        healthBar.SetSliderMaxHealth(maxHealth);
    }

    public void AddPoints(int amount)
    {
        points += amount;
    }

    public void AddCoins(int amount)
    {
        coinsCollected += amount;
    }
    public void SubtractHealth(int amount)
    {
        currentHealth -= amount;
        healthBar.SetSliderHealth(currentHealth);
    }

    private void Update()
    {
        if (currentHealth <= 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        gameOverCanvas.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Camera.main.transform.rotation = Quaternion.Euler(0, 0, 0);
        Time.timeScale = 0;
        Destroy(bubbleSpanwer);
    }
}
