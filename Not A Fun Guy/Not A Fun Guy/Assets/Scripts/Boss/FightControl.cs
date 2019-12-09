using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightControl : MonoBehaviour
{
    public GameObject mold;
    public GameObject shroom;
    private int count;
    public int maxSpawn;
    private bool spawnable = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        count = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if(count < maxSpawn)
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
        if(rand == 0)
        {
            GameObject enemy = Instantiate(mold, transform);
            enemy.transform.parent = null;
            enemy.transform.position -= new Vector3(Random.Range(5, 10), 0, 0);
            enemy.transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            GameObject enemy = Instantiate(shroom, transform);
            enemy.transform.parent = null;
            enemy.transform.position -= new Vector3(Random.Range(5, 10), 0, 0);
            enemy.transform.localScale = new Vector3(1,1,1);
        }
        yield return new WaitForSeconds(2f);
        spawnable = true;
    }
}
