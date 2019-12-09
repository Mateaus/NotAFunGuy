using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health = 10;
    private float currentHealth;
    public static bool invuln = false;
    public static bool shield = false;
    public Image healthBar;

    private void Start() {
        currentHealth = health;
    }

    private void Update() {
        Debug.Log("Hero HP: " + health);
    }

    public void TakeDamage(int damage) 
    {
        health -= damage;

        if (healthBar != null)
        {
            healthBar.fillAmount = health / currentHealth;
        }

        if (health <= 0)
        {
            OnDeath();
        }
    }

    private void OnDeath()
    {
        // Implement what to do if hero dies.
    }
}
