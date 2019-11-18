using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    private GameObject player;

    [SerializeField]
    public float xMin;

    [SerializeField]
    public float xMax;

    [SerializeField]
    public float yMin;

    [SerializeField]
    public float yMax;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void LateUpdate()
    {
        float x = Mathf.Clamp(player.transform.position.x, xMin, xMax);
        float y = Mathf.Clamp(player.transform.position.y, yMin, yMax);
        gameObject.transform.position = new Vector3(x, y, gameObject.transform.position.z);
    }
}
