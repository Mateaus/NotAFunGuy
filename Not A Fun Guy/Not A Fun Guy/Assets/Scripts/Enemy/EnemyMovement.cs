using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform target;
    private Rigidbody2D m_Rigidbody2D;
    private Vector3 m_Velocity = Vector3.zero;
    private float attackCoolDown = 0.0f;
    private bool hasAttacked = false;


    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;

    Animator a;
    float speed = 2f;
    public float stop = 1.5f;

    void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
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
            // Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector2(0 * speed, m_Rigidbody2D.velocity.y);
            // And then smoothing it out and applying it to the character
            m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

            //Play attack animation
            attack = true;
            move = false;


            // Cooldown attack happens here. I need to refactor this script
            // and docouple each action of the enemy to easily implement
            // new things.
            if (!hasAttacked)
            {
                hasAttacked = true;
                a.SetBool("isHitting", true);
                target.GetComponent<PlayerHealth>().TakeDamage(1);
                StartCoroutine(AttackTarget());
            }
        }
        else
        {
            // Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector2(0 * speed, m_Rigidbody2D.velocity.y);
            // And then smoothing it out and applying it to the character
            m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

            attack = false;
            move = false;
        }

        //print("Moving  " + move + "Attack  " + attack);
        //a.SetBool("isHitting", attack);
        a.SetBool("isMoving", move);
    }

    IEnumerator AttackTarget()
    {
        yield return new WaitForSeconds(1);
        a.SetBool("isHitting", false);
        hasAttacked = false;
    }
}
