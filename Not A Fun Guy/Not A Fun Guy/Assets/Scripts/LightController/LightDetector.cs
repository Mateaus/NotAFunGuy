using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDetector : MonoBehaviour
{
    //public Light light;
    public Transform globalLightSource;
    public bool isDark = false;
    public float xRotation;
    private Vector2 previousPosition = Vector2.zero;
    private bool hasEntered = false;
    private bool isReentering = false;

    private void Start()
    {
        xRotation = globalLightSource.eulerAngles.x;
    }

    private void Update()
    {
        xRotation = globalLightSource.eulerAngles.x;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            GameObject playerGO = other.gameObject;
            previousPosition = playerGO.transform.position;

            if (isBright())
            {
                isDark = false;
            }
            else
            {
                isDark = true;
            }


        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            GameObject playerGO = other.gameObject;
            previousPosition = playerGO.transform.position;

            if (isBright())
            {
                isDark = false;
            }
            else
            {
                isDark = true;
            }

        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject playerGO = other.gameObject;
            float lightXAngle = globalLightSource.eulerAngles.x;
            float playerXPosition = playerGO.transform.position.x;

            if (isBright())
            {
                Debug.Log("light not dark");
                globalLightSource.eulerAngles =
                new Vector3(Mathf.Clamp(lightXAngle + (playerXPosition - previousPosition.x) * 10, 0, 70), 0, 0);
            }
            else
            {
                if (isDark)
                {
                    Debug.Log("Dark");
                    globalLightSource.eulerAngles =
                    new Vector3(Mathf.Clamp(lightXAngle + (playerXPosition - previousPosition.x) * 10, 0, 70), 0, 0);
                }
                else
                {
                    Debug.Log("not dark");
                    globalLightSource.eulerAngles =
                    new Vector3(Mathf.Clamp(lightXAngle - (playerXPosition - previousPosition.x) * 10, 0, 70), 0, 0);
                }
            }

            previousPosition = playerGO.transform.position;
        }
    }

    private bool isBright()
    {
        if (globalLightSource.eulerAngles.x <= 0.1f)
        {
            isDark = false;
            return true;
        }
        else
        {
            isDark = true;
            return false;
        }
    }
}
