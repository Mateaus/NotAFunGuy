using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroBuffs : MonoBehaviour
{
    private static bool attackUp;
    private static bool shield;
    private static bool invuln;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("AttackUp"))
        {
            Destroy(other.gameObject);
            HeroAttack.attUp = true;
            Debug.Log("AttackUp");
        }
        else if (other.gameObject.tag.Equals("Shield"))
        {
            Destroy(other.gameObject);
            Debug.Log("Shield");
        }
        else if (other.gameObject.tag.Equals("Invuln"))
        {
            Destroy(other.gameObject);
            Debug.Log("Invuln");
        }
    }
}
