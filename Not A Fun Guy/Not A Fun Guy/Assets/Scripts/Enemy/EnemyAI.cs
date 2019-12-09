using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform target;
    public float movementSpeed;
    public int attackDamage;
    public float attackRange;
    public float viewRange;
    public Transform colliderPositionLeft;
    public Transform colliderPositionRight;
    private Rigidbody2D m_Rigidbody2D;
    private SpriteRenderer spriteRenderer;
    private Animator enemyAnimator;
    private bool hasAttacked = false;
    private bool correctDirection = true;
    private float currentMovementSpeed;
    private float stunTime;
    public float startStunTime;
    private float awayDuration = 0.0f;
    private float scoutDuration = 0.0f;
    private float idleDuration = 0.0f;
    private int randomXValue = 0;
    public LayerMask whatIsEnemies;
    public float colliderRangeX;
    public float colliderRangeY;
    private Vector2 targetPosition = Vector2.zero;
    private float cooldownSearch = 0.0f;

    private void Start()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyAnimator = GetComponent<Animator>();
        currentMovementSpeed = movementSpeed;

        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        // Check if enemy gets stunned.
        if (stunTime <= 0)
        {
            currentMovementSpeed = movementSpeed;
        }
        else
        {
            currentMovementSpeed = 0;
            stunTime -= Time.deltaTime;
        }


        float targetDistance = Vector2.Distance(transform.position, target.position);
        if (targetDistance > viewRange)
        {
            if (scoutDuration <= 0)
            {
                if (idleDuration <= 0)
                {
                    Debug.Log("CHANGING VALUES");
                    randomXValue = Random.Range(-5, 5);
                    scoutDuration = Random.Range(0, 5);
                    idleDuration = Random.Range(0, 3);
                }
                else
                {
                    Debug.Log("Resting!");
                    enemyAnimator.SetBool("isMoving", false);
                    enemyAnimator.SetBool("isHitting", false);
                    transform.position = Vector2.MoveTowards(transform.position, transform.position, currentMovementSpeed * Time.deltaTime);
                    idleDuration -= Time.deltaTime;
                }
            }
            else
            {
                Debug.Log("MOVING!");
                Collider2D[] leftBlockages = Physics2D.OverlapBoxAll(colliderPositionLeft.position, new Vector2(colliderRangeX, colliderRangeY), 0, whatIsEnemies);
                for (int i = 0; i < leftBlockages.Length; i++)
                {
                    GameObject enemyGO = leftBlockages[i].gameObject;
                    if (enemyGO.tag == "Ground" || enemyGO.tag == "Enemy")
                    {
                        Debug.Log(randomXValue);
                        Debug.Log("Value founded");
                        randomXValue = -randomXValue;
                    }
                }

                Collider2D[] rightBlockages = Physics2D.OverlapBoxAll(colliderPositionRight.position, new Vector2(colliderRangeX, colliderRangeY), 0, whatIsEnemies);
                for (int i = 0; i < rightBlockages.Length; i++)
                {
                    GameObject enemyGO = rightBlockages[i].gameObject;
                    if (enemyGO.tag == "Ground" || enemyGO.tag == "Enemy")
                    {
                        Debug.Log(randomXValue);
                        Debug.Log("Value founded");
                        randomXValue = -randomXValue;
                    }
                }

                enemyAnimator.SetBool("isMoving", true);
                Vector2 randomPosition = new Vector2(transform.position.x + randomXValue, transform.position.y);
                transform.position = Vector2.MoveTowards(transform.position, randomPosition, currentMovementSpeed * Time.deltaTime);
                //Debug.Log(randomPosition.x);
                spriteRenderer.flipX = transform.position.x <= randomPosition.x ? false : true;

                scoutDuration -= Time.deltaTime;
            }
        }
        else if (targetDistance > attackRange && targetDistance < viewRange)
        {
            MoveTowardsTarget();
            //spriteRenderer.flipX = TargetDirection();
        }
        else if (targetDistance <= attackRange)
        {
            if (!hasAttacked)
            {
                hasAttacked = true;
                StartCoroutine(AttackTarget());
            }
            Debug.Log("Attack!");
            spriteRenderer.flipX = TargetDirection();
        }

        //spriteRenderer.flipX = TargetDirection();
    }

    private void MoveTowardsTarget()
    {
        bool isFound = false;
        Collider2D[] leftBlockages = Physics2D.OverlapBoxAll(colliderPositionLeft.position, new Vector2(colliderRangeX, colliderRangeY), 0, whatIsEnemies);
        for (int i = 0; i < leftBlockages.Length; i++)
        {
            GameObject enemyGO = leftBlockages[i].gameObject;
            if (enemyGO.tag == "Ground" || enemyGO.tag == "Enemy")
            {
                correctDirection = false;
                awayDuration = 1.5f;
                Debug.Log("Collided with left wall");
                targetPosition = new Vector2(transform.position.x + 1, transform.position.y);
                isFound = true;
            }
        }

        Collider2D[] rightBlockages = Physics2D.OverlapBoxAll(colliderPositionRight.position, new Vector2(colliderRangeX, colliderRangeY), 0, whatIsEnemies);
        for (int i = 0; i < rightBlockages.Length; i++)
        {
            GameObject enemyGO = rightBlockages[i].gameObject;
            if (enemyGO.tag == "Ground" || enemyGO.tag == "Enemy")
            {
                correctDirection = false;
                awayDuration = 1.5f;
                Debug.Log("Collided with right wall");
                targetPosition = new Vector2(transform.position.x - 1, transform.position.y);
                isFound = true;
            }
        }

        if (cooldownSearch <= 0)
        {
            if (!isFound)
            {
                targetPosition = new Vector2(target.position.x, transform.position.y);
            }

            cooldownSearch = 1.0f;
        }
        else
        {
            cooldownSearch -= Time.deltaTime;
        }

        if (transform.position.x > targetPosition.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }

        Debug.Log(targetPosition.x);
        enemyAnimator.SetBool("isMoving", true);
        //Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, currentMovementSpeed * Time.deltaTime);
    }

    IEnumerator AttackTarget()
    {
        enemyAnimator.SetBool("isMoving", false);
        enemyAnimator.SetBool("isHitting", true);
        yield return new WaitForSeconds(0.5f);
        if (Mathf.Abs(target.transform.position.x - transform.position.x) < attackRange &&
            Mathf.Abs(target.transform.position.y - transform.position.y) < attackRange)
        {
            target.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
        }
        enemyAnimator.SetBool("isMoving", true);
        enemyAnimator.SetBool("isHitting", false);
        yield return new WaitForSeconds(1f);
        //enemyAnimator.SetBool("isHitting", false);
        hasAttacked = false;
    }

    private bool TargetDirection()
    {
        return transform.position.x < target.position.x ? false : true;
    }

    public void StunEnemy()
    {
        stunTime = startStunTime;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(colliderPositionLeft.position, new Vector3(colliderRangeX, colliderRangeY, 1));
        Gizmos.DrawWireCube(colliderPositionRight.position, new Vector3(colliderRangeX, colliderRangeY, 1));
    }
}
