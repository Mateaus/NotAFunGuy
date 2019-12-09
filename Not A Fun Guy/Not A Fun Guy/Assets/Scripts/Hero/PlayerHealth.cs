using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health = 10;
    private float currentHealth;
    public static bool invuln = false;
    private static bool active = false;
    public static bool shield = false;
    public Image healthBar;
    public GameObject invEffect;

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
        if (invuln && !active)
        {
            active = true;
            StartCoroutine("Inv");
        }
    }

    IEnumerator Inv()
    {
        GameObject sparkle = Instantiate(invEffect, transform);
        yield return new WaitForSeconds(10f);
        Destroy(sparkle);
        invuln = false;
    }
}
