using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hit : MonoBehaviour
{
    private HeroAttack attack;
    // Start is called before the first frame update
    void Start()
    {
        attack = GetComponentInParent<HeroAttack>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            //You will need a function on your parent object that applies damage to enemies you will want to pass the enemy to the parent so damage can be applied
            attack.damage(other.gameObject);
        }
    }
}
