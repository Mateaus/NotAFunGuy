using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAttack : MonoBehaviour
{
    private bool canAttack;
    private AudioSource source;
    public AudioClip attack;

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
            GetComponent<Animator>().SetInteger("Attack", 1);
            StartCoroutine(attackCool());
        }
        else
        {
            GetComponent<Animator>().SetInteger("Attack", 0);
        }
    }

    IEnumerator attackCool()
    {
        yield return new WaitForSeconds(0.2f);
        canAttack = true;
    }
}
