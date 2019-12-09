using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightControl : MonoBehaviour
{
    public GameObject[] enemies;
    public Transform spawnerLocation;
    public LayerMask whatIsEnemies;
    private int count;
    public int maxSpawn;
    private bool spawnable = true;
    private Vector2 randomLocation = Vector2.zero;
    private List<GameObject> enemiesList;
    private Animator bossAnimator;
    private float bossTimer = 5.0f;
    private float clearTimer = 0.0f;
    private float monsterSpawnTimer = 0.0f;
    private BoxCollider2D bossCollider;

    // Start is called before the first frame update
    void Start()
    {
        enemiesList = new List<GameObject>();
        bossAnimator = GetComponent<Animator>();
        bossCollider = GetComponent<BoxCollider2D>();
        bossAnimator.SetBool("isDarkIdle", true);
        bossCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (maxSpawn == 0 && enemiesList.Count == 0)
        {
            if (bossTimer <= 0)
            {
                maxSpawn = 5;
                // Hide boss here.
                bossCollider.enabled = false;
                bossAnimator.SetBool("bossHide", true);
                bossAnimator.SetBool("bossAppear", false);
                bossAnimator.SetBool("isDarkIdle", true);
                bossAnimator.SetBool("isIdle", false);

                bossTimer = 5.0f; // Time allowed for boss to appear
            }
            else
            {
                bossCollider.enabled = true;
                bossAnimator.SetBool("bossHide", false);
                bossAnimator.SetBool("bossAppear", true);
                bossAnimator.SetBool("isDarkIdle", false);
                bossAnimator.SetBool("isIdle", true);
                // Show boss here.
                bossTimer -= Time.deltaTime;
            }
        }

        if (maxSpawn != 0)
        {
            if (monsterSpawnTimer <= 0)
            {
                Spawn();
                monsterSpawnTimer = 1.0f;
            }
            else
            {
                monsterSpawnTimer -= Time.deltaTime;
            }
        }

        if (clearTimer <= 0)
        {
            ClearDeadEnemies();
            clearTimer = 0.5f;
        }
        else
        {
            clearTimer -= Time.deltaTime;
        }


        // count = GameObject.FindGameObjectsWithTag("Enemy").Length;
        // if (count < maxSpawn)
        // {
        //     if (spawnable)
        //     {
        //         spawnable = false;
        //         StartCoroutine("Spawn");
        //     }
        // }
    }

    private void Spawn()
    {
        float randomXLocation = Random.Range(0, 7);
        randomLocation = new Vector2(randomXLocation + spawnerLocation.position.x, spawnerLocation.position.y);

        Collider2D[] objectFound = Physics2D.OverlapBoxAll(randomLocation, new Vector2(2, 2), 0, whatIsEnemies);
        if (objectFound.Length != 0)
        {
            return;
        }

        int rand = Random.Range(0, 2);
        GameObject enemy = Instantiate(enemies[rand], transform);
        enemiesList.Add(enemy);
        enemy.transform.parent = null;
        enemy.transform.position = new Vector2(randomXLocation + spawnerLocation.position.x, spawnerLocation.position.y);
        enemy.transform.localScale = new Vector3(1, 1, 1);
        maxSpawn--;
    }

    private void ClearDeadEnemies()
    {
        for (int i = 0; i < enemiesList.Count; i++)
        {
            if (enemiesList[i] == null)
            {
                enemiesList.Remove(enemiesList[i]);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(randomLocation, new Vector3(2, 2, 1));
    }
}
