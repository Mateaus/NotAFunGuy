using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDetector : MonoBehaviour
{
    public Light smallLightSource;
    public Transform globalLightSource;
    public bool isDark = false;
    public float xRotation;
    private Vector2 previousPosition = Vector2.zero;
    private bool hasEntered = false;
    private bool enteredFromLeft = false;

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

            if (previousPosition.x < transform.position.x)
            {
                enteredFromLeft = true;
            }
            else if (previousPosition.x > transform.position.x)
            {
                enteredFromLeft = false;
            }

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

            if (isDark)
            {
                if (enteredFromLeft)
                {
                    globalLightSource.eulerAngles =
                        new Vector3(Mathf.Clamp(lightXAngle - (playerXPosition - previousPosition.x) * 10, 0, 70), 0, 0);
                }
                else
                {
                    globalLightSource.eulerAngles =
                        new Vector3(Mathf.Clamp(lightXAngle + (playerXPosition - previousPosition.x) * 10, 0, 70), 0, 0);
                }
            }
            else
            {
                if (enteredFromLeft)
                {
                    globalLightSource.eulerAngles =
                        new Vector3(Mathf.Clamp(lightXAngle + (playerXPosition - previousPosition.x) * 10, 0, 70), 0, 0);
                }
                else
                {
                    globalLightSource.eulerAngles =
                        new Vector3(Mathf.Clamp(lightXAngle - (playerXPosition - previousPosition.x) * 10, 0, 70), 0, 0);
                }
            }

            smallLightSource.range = Mathf.Clamp((lightXAngle / 5), 0, 14);
            previousPosition = playerGO.transform.position;
        }
    }

    private bool isBright()
    {
        return globalLightSource.eulerAngles.x <= 30.0f;
    }
}
