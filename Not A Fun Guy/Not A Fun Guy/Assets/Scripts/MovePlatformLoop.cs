using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatformLoop : MonoBehaviour
{
    public float delta = 1.5f;
    public float speed = 2.0f;
    public bool isHorizontal = true;
    private Vector2 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        Vector2 direction = startPosition;

        if (isHorizontal)
        {
            direction.x += delta * Mathf.Cos(Time.time * speed);
        }
        else
        {
            direction.y += delta * Mathf.Cos(Time.time * speed);
        }
        transform.position = direction;
    }
}
