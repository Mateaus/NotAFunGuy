using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraC : MonoBehaviour
{
    public GameObject character;
    public float cameraY;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        Vector3 currentPosition = new Vector3(character.transform.position.x, character.transform.position.y + cameraY, transform.position.z);
        transform.position = currentPosition;
    }
}
