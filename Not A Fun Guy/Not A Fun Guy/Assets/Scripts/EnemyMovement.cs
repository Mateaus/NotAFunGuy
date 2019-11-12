using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Transform target;
    float speed = 2f;
    float stop = 0.8f;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;

    }

    private void Update()
    {
        enemyDirection();
       
    }

    public void enemyDirection()
    {
        Vector2 stayOnY = new Vector2(target.position.x, transform.position.y);

        // Have enemies follow player only horizontally
        if (Vector2.Distance(transform.position, target.position) > stop)
        {
            transform.position = Vector2.MoveTowards(transform.position,stayOnY, speed * Time.deltaTime);
        }


    }


}
