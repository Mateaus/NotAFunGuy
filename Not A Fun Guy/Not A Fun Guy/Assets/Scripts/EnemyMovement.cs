using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Transform target;
    private Rigidbody2D m_Rigidbody2D;
    private Vector3 m_Velocity = Vector3.zero;


    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;

    Animator a;
    float speed = 2f;
    float stop = 0.8f;

    void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
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
            float dir = 0;
            if (transform.position.x < target.position.x)
            {
                dir = 1;
            }
            if (transform.position.x > target.position.x)
            {
                dir = -1;
            }
            //transform.position = Vector2.MoveTowards(transform.position, stayOnY , speed * Time.deltaTime);
            // Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector2(dir * speed, m_Rigidbody2D.velocity.y);
            // And then smoothing it out and applying it to the character
            m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

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
