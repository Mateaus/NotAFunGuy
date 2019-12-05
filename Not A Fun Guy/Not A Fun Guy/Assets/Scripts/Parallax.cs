using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length;
    private float startPosition;
    public GameObject cameraGO;
    public float parallaxEffect;

    private float startYPosition;

    private void Start()
    {
        startPosition = transform.position.x;
        startYPosition = transform.position.y;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void Update() {
        float temp = (cameraGO.transform.position.x * (1 - parallaxEffect));
        float distance = (cameraGO.transform.position.x * parallaxEffect);
        transform.position = new Vector3(startPosition + distance, startYPosition, transform.position.z);

        if (temp > startPosition + length) startPosition += length;
        else if (temp < startPosition - length) startPosition -= length;
    }
}
