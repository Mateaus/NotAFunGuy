using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Transform target;
    Animator a;
    float speed = 2f;
    float stop = 0.8f;
    void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        a = GetComponent<Animator>();
    }

    private void Update()
    {
        enemyDirection();
       
    }


    public void enemyDirection()
    {
        Vector2 stayOnY = new Vector2(target.position.x, transform.position.y);
        float distanceBetween = Vector2.Distance(transform.position, target.position);
        bool attack = false;
        bool move = false;


        // Have enemies follow player only horizontally and only when player is at a certain distance
        if (distanceBetween > stop & distanceBetween <= 7.5)
        {
            transform.position = Vector2.MoveTowards(transform.position, stayOnY , speed * Time.deltaTime);
            move = true;
            attack = false;
        }
        else if (distanceBetween < stop)
        {
            //Play attack animation
            attack = true;
            move = false;
        }
        else
        {
            attack = false;
            move = false;
        }

        //print("Moving  " + move + "Attack  " + attack);
        a.SetBool("isHitting", attack);
        a.SetBool("isMoving", move);


    }


}
