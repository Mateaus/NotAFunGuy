using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public static bool invuln = false;
    private static bool active = false;
    public static bool shield = false;
    public GameObject invEffect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (invuln && !active)
        {
            active = true;
            StartCoroutine("Inv");
        }
    }

    IEnumerator Inv()
    {
        GameObject sparkle = Instantiate(invEffect, transform);
        yield return new WaitForSeconds(10f);
        Destroy(sparkle);
        invuln = false;
    }
}
