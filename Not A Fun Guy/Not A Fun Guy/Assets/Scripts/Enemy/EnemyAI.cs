using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform target;
    public float movementSpeed;
    public float attackRange;
    public float viewRange;
    private Rigidbody2D m_Rigidbody2D;
    private SpriteRenderer spriteRenderer;
    private PlayerHealth playerHealth;
    private Animator enemyAnimator;
    private float currentMovementSpeed;
    private bool hasAttacked = false;

    private void Start()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerHealth = target.GetComponent<PlayerHealth>();
        enemyAnimator = GetComponent<Animator>();
        currentMovementSpeed = movementSpeed;
    }

    private void Update()
    {
        float targetDistance = Vector2.Distance(transform.position, target.position);
        if (targetDistance > viewRange)
        {
            enemyAnimator.SetBool("isMoving", false);
            enemyAnimator.SetBool("isHitting", false);
        }
        else if (targetDistance > attackRange && targetDistance < viewRange)
        {
            MoveTowardsTarget();
            // Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);
            // transform.position = Vector2.MoveTowards(transform.position, targetPosition, currentMovementSpeed * Time.deltaTime);
            // enemyAnimator.SetBool("isMoving", true);
        }
        else if (targetDistance <= attackRange)
        {
            if (!hasAttacked)
            {
                hasAttacked = true;
                enemyAnimator.SetBool("isMoving", false);
                enemyAnimator.SetBool("isHitting", true);
                playerHealth.TakeDamage(1);
                StartCoroutine(AttackTarget());
            }
            Debug.Log("Attack!");
        }

        spriteRenderer.flipX = TargetDirection();
    }

    private void MoveTowardsTarget()
    {
        Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, currentMovementSpeed * Time.deltaTime);
    }

    IEnumerator AttackTarget()
    {
        yield return new WaitForSeconds(1.0f);
        //enemyAnimator.SetBool("isHitting", false);
        hasAttacked = false;
    }

    private bool TargetDirection()
    {
        return transform.position.x < target.position.x ? false : true;
    }

    public void _StunEnemy()
    {
        StartCoroutine(StunEnemy());
    }

    IEnumerator StunEnemy()
    {
        enemyAnimator.SetBool("isMoving", false);
        enemyAnimator.SetBool("isHitting", false);
        currentMovementSpeed = 0.0f;
        yield return new WaitForSeconds(0.5f);
        currentMovementSpeed = movementSpeed;
    }
}
