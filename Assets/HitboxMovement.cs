using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxMovement : MonoBehaviour
{
    [SerializeField] float force;
    HitboxSpawner spawner;
    Transform finalPoint;
    Vector3 direction;
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        spawner = FindAnyObjectByType<HitboxSpawner>();
        finalPoint = spawner.finalPoints[Random.Range(0, spawner.finalPoints.Length)];
        direction = (finalPoint.position - rb.position).normalized;
        rb.velocity = direction * force;
    }

    void Update()
    {
        //rb.AddForce(direction);
        //Debug.Log(direction * force);
        if ((finalPoint.position - rb.position).magnitude < 0.01)
        {
            Destroy(gameObject);
        }
    }
}
