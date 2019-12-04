using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformRigidBodyHandler : MonoBehaviour
{
    public bool useTriggerAsSensor = false;
    public List<Rigidbody2D> rigidbodies = new List<Rigidbody2D>();
    public Vector3 lastPosition;
    private Transform _transform;

    private void Start()
    {
        _transform = transform;
        lastPosition = _transform.position;
    }

    private void LateUpdate()
    {
        if (rigidbodies.Count > 0)
        {
            for (int i = 0; i < rigidbodies.Count; i++)
            {
                Rigidbody2D rigidbody2D = rigidbodies[i];
                Vector3 velocity = (_transform.position - lastPosition);
                rigidbody2D.transform.Translate(velocity);
            }
        }

        lastPosition = _transform.position;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Rigidbody2D rigidbody2D = other.collider.GetComponent<Rigidbody2D>();
        if (rigidbody2D != null)
        {
            AddRigidBody(rigidbody2D);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        Rigidbody2D rigidbody2D = other.collider.GetComponent<Rigidbody2D>();
        if (rigidbody2D != null)
        {
            RemoveRigidBody(rigidbody2D);
        }
    }

    private void AddRigidBody(Rigidbody2D rigidbody2D)
    {
        if (!rigidbodies.Contains(rigidbody2D))
        {
            rigidbodies.Add(rigidbody2D);
        }
    }

    private void RemoveRigidBody(Rigidbody2D rigidbody2D)
    {
        if (rigidbodies.Contains(rigidbody2D))
        {
            rigidbodies.Remove(rigidbody2D);
        }
    }

    //    private void OnCollisionEnter2D(Collision2D other) {
    //        if (other.transform.tag == "Player")
    //        {
    //            other.transform.SetParent(transform);
    //        }
    //    }

    //    private void OnCollisionExit2D(Collision2D other) {
    //        if (other.transform.tag == "Player")
    //        {
    //            other.transform.SetParent(null);
    //        }
    //    }
}
