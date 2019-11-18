using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private int startHealth;
    private static int playerHealth;
    // Start is called before the first frame update
    void Start()
    {
        playerHealth = startHealth;
    }

    public static void damagePlayer()
    {
        playerHealth -= 1;
    }
    
    public static void healPlayer(int healAmount)
    {
        playerHealth += healAmount;
    }

    public static int getHealth()
    {
        return PlayerHealth.playerHealth;
    }
}
