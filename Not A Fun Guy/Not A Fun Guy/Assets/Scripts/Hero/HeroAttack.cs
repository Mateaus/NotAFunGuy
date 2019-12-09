using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAttack : MonoBehaviour
{
    private bool canAttack = false;
    private AudioSource source;
    public AudioClip attack;
    public GameObject hit;
    public float knockScale;
    public int strength;
    public static bool attUp = false;
    public bool buff = false;
    public GameObject atkEffect;

    // Start is called before the first frame update
    void Start()
    {
        source = gameObject.GetComponents<AudioSource>()[0];
        canAttack = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && canAttack)
        {
            source.PlayOneShot(attack);
            canAttack = false;
            GetComponent<Animator>().SetBool("Attack", true);
            hit.SetActive(true);
            StartCoroutine(attackCool());
        }
        else
        {
            hit.SetActive(false);
            GetComponent<Animator>().SetBool("Attack", false);
        }
        if (attUp && !buff)
        {
            attUp = false;
            buff = true;
            StartCoroutine("AttackUp");
        }
    }

    public void damage(GameObject other)
    {
        other.GetComponent<EnemyHealth>().damage(strength);
        if (other.transform.position.x > transform.position.x)
        {
            Vector2 knock = new Vector2(1, 1);
            other.GetComponent<Rigidbody2D>().AddForce(knock * knockScale);
            if (other.GetComponent<EnemyHealth>().health != 0)
            {
                StartCoroutine(EnemyStun(other));
            }
        }
        if (other.transform.position.x < transform.position.x)
        {
            Vector2 knock = new Vector2(-1, 1);
            other.GetComponent<Rigidbody2D>().AddForce(knock * knockScale);
            if (other.GetComponent<EnemyHealth>().health != 0)
            {
                StartCoroutine(EnemyStun(other));
            }
        }
    }

    public void damageBoss(GameObject other)
    {
        other.GetComponent<BossHealth>().damage(strength);
    }

    IEnumerator EnemyStun(GameObject other)
    {
        if (other != null)
        other.GetComponent<EnemyMovement>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        if (other != null)
        other.GetComponent<EnemyMovement>().enabled = true;
    }

    IEnumerator attackCool()
    {
        yield return new WaitForSeconds(0.2f);
        canAttack = true;
    }

    IEnumerator AttackUp()
    {
        GameObject sparkle = Instantiate(atkEffect, transform);
        strength = strength * 2;
        yield return new WaitForSeconds(10f);
        strength = strength / 2;
        buff = false;
        Destroy(sparkle);
    }
}
