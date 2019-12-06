using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health;
    public GameObject AttackUp;

    // Start is called before the first frame update
    void Start()
    {
        
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
    }

    public void die()
    {
        int rand = Random.Range(0, 100);
        if (rand < 100)
        {
            GameObject inst = Instantiate(AttackUp, transform);
            inst.transform.parent = null;
            inst.transform.position += new Vector3(0, 0.1f, 0);
        }
        Destroy(gameObject);
    }
}
