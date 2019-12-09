using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int health = 10;
    private float currentHealth;
    public static bool invuln = false;
    private static bool active = false;
    public static bool shield = false;
    public Image shieldSprite;
    public Image healthBar;
    public GameObject invEffect;
    public Animator animator;
    private bool dead = false;
    private CharacterMovement characterMov;

    private void Awake()
    {
        invuln = false;
        active = false;
        shield = false;
    }

    private void Start()
    {
        currentHealth = health;
        characterMov = GetComponent<CharacterMovement>();
    }

    private void Update()
    {
        //Debug.Log("Hero HP: " + health);
        if (shield)
        {
            shieldSprite.enabled = true;
        }
        else
        {
            shieldSprite.enabled = false;
        }

        if (invuln && !active)
        {
            active = true;
            StartCoroutine("Inv");
        }
        if (health <= 0 && !dead)
        {
            dead = true;
            StartCoroutine(Die());
        }
        if (transform.position.y <= -50)
        {
            dead = true;
            StartCoroutine(Die());
        }
    }

    public void TakeDamage(int damage)
    {
        if (invuln)
        {
            return;
        }
        if (shield)
        {
            shield = false;
            return;
        }
        health -= damage;

        if (healthBar != null)
        {
            healthBar.fillAmount = health / currentHealth;
        }
    }

    private void OnDeath()
    {

    }

    IEnumerator Die()
    {
        animator.SetBool("Dead", true);
        
        characterMov.death();
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator Inv()
    {
        GameObject sparkle = Instantiate(invEffect, transform);
        yield return new WaitForSeconds(10f);
        Destroy(sparkle);
        active = false;
        invuln = false;
    }
}
