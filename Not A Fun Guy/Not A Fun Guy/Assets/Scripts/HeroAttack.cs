using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAttack : MonoBehaviour
{
    bool canAttack;

    // Start is called before the first frame update
    void Start()
    {
        canAttack = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && canAttack)
        {
            canAttack = false;
            GetComponent<Animator>().SetTrigger("Attack");
            StartCoroutine(attackCool());
        }
    }

    IEnumerator attackCool()
    {
        yield return new WaitForSeconds(0.2f);
        canAttack = true;
    }
}
