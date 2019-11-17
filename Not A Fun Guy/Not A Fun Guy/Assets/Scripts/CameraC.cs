using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraC : MonoBehaviour
{
    public GameObject character;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentPosition = new Vector3(character.transform.position.x, character.transform.position.y, transform.position.z);
        transform.position = currentPosition;
    }
}
