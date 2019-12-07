using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    public Transform target;
    public Light light;

    private void Update()
    {
        if (target.position.y <= 0.0f)
        {
            transform.eulerAngles = new Vector3(Mathf.Clamp(Mathf.Abs(target.position.y * 11.0f), 0, 85.0f), 0, 0);
        }

        if (transform.eulerAngles.x > 60.0f)
        {
            if (light.range <= 20)
            {
                light.range += 10 * Time.deltaTime;
            }
        }
        else
        {
            if (light.range <= 30 && light.range >= 0)
            {
                light.range -= 15 * Time.deltaTime;
            }
        }
    }
}
