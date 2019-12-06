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
    }

    public void damage(GameObject other)
    {
        if (other.transform.position.x > transform.position.x)
        {
            Vector2 knock = new Vector2(1, 1);
            other.GetComponent<Rigidbody2D>().AddForce(knock * knockScale);
            StartCoroutine(EnemyStun(other));
        }
        if (other.transform.position.x < transform.position.x)
        {
            Vector2 knock = new Vector2(-1, 1);
            other.GetComponent<Rigidbody2D>().AddForce(knock * knockScale);
            StartCoroutine(EnemyStun(other));
        }
    }

    IEnumerator EnemyStun(GameObject other)
    {
        other.GetComponent<EnemyMovement>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        other.GetComponent<EnemyMovement>().enabled = true;
    }

    IEnumerator attackCool()
    {
        yield return new WaitForSeconds(0.2f);
        canAttack = true;
    }
}
