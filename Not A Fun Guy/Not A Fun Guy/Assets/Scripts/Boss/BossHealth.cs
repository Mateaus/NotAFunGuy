using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossHealth : MonoBehaviour
{
    public Image bossHealth;
    public int health;
    private float currentHealth;
    private AudioSource source;
    public AudioClip hit;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = health;
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            die();
        }
    }

    public void damage(int val)
    {
        health -= val;
        source.PlayOneShot(hit);

        if (bossHealth != null)
        {
            bossHealth.fillAmount = health / currentHealth;
        }
    }

    public void die()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        StartCoroutine("End");
    }

    IEnumerator End()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("End");
    }
}
