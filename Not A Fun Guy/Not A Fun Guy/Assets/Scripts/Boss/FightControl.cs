using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightControl : MonoBehaviour
{
    public GameObject mold;
    public GameObject shroom;
    public Transform spawnerLocation;
    public LayerMask whatIsEnemies;
    private int count;
    public int maxSpawn;
    private bool spawnable = true;
    private Vector2 randomLocation = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        count = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (count < maxSpawn)
        {
            if (spawnable)
            {
                spawnable = false;
                StartCoroutine("Spawn");
            }
        }
    }

    IEnumerator Spawn()
    {
        int rand = Random.Range(0, 2);
        float randomXLocation = Random.Range(0, 7);
        randomLocation = new Vector2(randomXLocation + spawnerLocation.position.x, spawnerLocation.position.y);

        Collider2D[] objectFound = Physics2D.OverlapBoxAll(randomLocation, new Vector2(2, 2), 0, whatIsEnemies);
        if (objectFound.Length != 0)
        {
            spawnable = true;
            yield break;
        }

        if (rand == 0)
        {
            GameObject enemy = Instantiate(mold, transform);
            enemy.transform.parent = null;
            enemy.transform.position = new Vector2(randomXLocation + spawnerLocation.position.x, spawnerLocation.position.y);
            enemy.transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            GameObject enemy = Instantiate(shroom, transform);
            enemy.transform.parent = null;
            enemy.transform.position = new Vector2(randomXLocation + spawnerLocation.position.x, spawnerLocation.position.y);
            enemy.transform.localScale = new Vector3(1, 1, 1);
        }
        yield return new WaitForSeconds(2f);
        spawnable = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(randomLocation, new Vector3(2, 2, 1));
    }
}
