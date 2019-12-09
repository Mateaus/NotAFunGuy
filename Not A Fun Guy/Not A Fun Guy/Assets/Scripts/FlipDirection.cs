using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipDirection : MonoBehaviour
{

    SpriteRenderer flip;
    float currentXpos;

    void Start()
    {
        currentXpos = transform.position.x;
        flip = GetComponent<SpriteRenderer>();

    }

    private void Update()
    {
        flipEnemy();

    }

    public void flipEnemy()
    {
        // Have enemies flip their facing direction if player goes left or right
        // Left
        if (transform.position.x < currentXpos)
        {
            flip.flipX = true;
            currentXpos = transform.position.x;
        }
        //Right
        else if (transform.position.x > currentXpos)
        {
            flip.flipX = false;
            currentXpos = transform.position.x;
        }

    }


}

