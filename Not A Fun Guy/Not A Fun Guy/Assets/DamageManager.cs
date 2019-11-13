using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageManager : MonoBehaviour
{
    // Start is called before the first frame update
    
   
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Enemy")) 
        {
            Debug.Log("Collision Made with enemy");
           
            PlayerHealth.damagePlayer();
            Debug.Log("DamageTaken Health is now: " + PlayerHealth.getHealth());
        }
        
    }
}
