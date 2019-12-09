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
    private float timeBtwAttack = 0.0f;

    public Transform attackPos;
    public float attackRangeX;
    public float attackRangeY;
    public LayerMask whatIsEnemies;
    public float startTimeBetweenAttacks = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        source = gameObject.GetComponents<AudioSource>()[0];
        canAttack = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeBtwAttack <= 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                source.PlayOneShot(attack);
                GetComponent<Animator>().SetTrigger("Attk");
                Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(attackPos.position, new Vector2(attackRangeX, attackRangeY), 0, whatIsEnemies);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    GameObject enemyGO = enemiesToDamage[i].gameObject;
                    if (enemyGO.tag == "Boss")
                    {
                        enemyGO.GetComponent<BossHealth>().damage(strength);
                    }
                    else
                    {
                        damage(enemiesToDamage[i].gameObject);
                    }
                }
                timeBtwAttack = startTimeBetweenAttacks;
            }
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }



        // if (Input.GetButtonDown("Fire1"))
        // {
        //     source.PlayOneShot(attack);
        //     canAttack = false;
        //     GetComponent<Animator>().SetBool("Attack", true);
        //     hit.SetActive(true);
        //     StartCoroutine(attackCool());
        // }
        // else
        // {
        //     hit.SetActive(false);
        //     GetComponent<Animator>().SetBool("Attack", false);
        // }
        if (attUp && !buff)
        {
            attUp = false;
            buff = true;
            StartCoroutine("AttackUp");
        }
    }

    public void damage(GameObject other)
    {
        EnemyHealth enemyHP = other.GetComponent<EnemyHealth>();
        Rigidbody2D enemyRB = other.GetComponent<Rigidbody2D>();
        Transform enemyTransform = other.GetComponent<Transform>();
        
        Vector2 knock = Vector2.zero;
        if (enemyTransform.position.x >= transform.position.x)
        {
            knock = new Vector2(1, 0.4f);
        }
        if (enemyTransform.position.x < transform.position.x) 
        {
            knock = new Vector2(-1, 0.4f);
        }
        
        other.GetComponent<EnemyAI>().StunEnemy();
        enemyHP.damage(strength);
        enemyRB.AddForce(knock * knockScale);
        //other.GetComponent<EnemyAI>()._StunEnemy();
        //StartCoroutine(EnemyStun(other));

        // other.GetComponent<EnemyHealth>().damage(strength);
        // other.GetComponent<Rigidbody2D>().AddForce();
        // if (other.transform.position.x > transform.position.x)
        // {
        //     Vector2 knock = new Vector2(1, 1);
        //     other.GetComponent<Rigidbody2D>().AddForce(knock * knockScale);
        //     if (other.GetComponent<EnemyHealth>().health != 0)
        //     {
        //         StartCoroutine(EnemyStun(other));
        //     }
        // }
        // if (other.transform.position.x < transform.position.x)
        // {
        //     Vector2 knock = new Vector2(-1, 1);
        //     other.GetComponent<Rigidbody2D>().AddForce(knock * knockScale);
        //     if (other.GetComponent<EnemyHealth>().health != 0)
        //     {
        //         StartCoroutine(EnemyStun(other));
        //     }
        // }
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPos.position, new Vector3(attackRangeX, attackRangeY, 1));
    }
}
