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
    float rotationSpeedX, rotationSpeedY, rotationSpeedZ;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        direction = (finalPoint.position - rb.position).normalized;
        rb.velocity = direction * force;
        transform.GetChild(1).GetChild(Random.Range(0, 10)).gameObject.SetActive(true);
        rotationSpeedX = Random.Range(30,110);
        rotationSpeedY = Random.Range(30, 110);
        rotationSpeedZ = Random.Range(30, 110);
    }
    void Update()
    {
        //rb.AddForce(direction);
        //Debug.Log(direction * force);
        transform.Rotate(new Vector3(rotationSpeedX * Time.deltaTime, rotationSpeedY * Time.deltaTime, rotationSpeedZ * Time.deltaTime));
        if ((finalPoint.position - rb.position).magnitude < 0.05)
        {
            transform.GetChild(0).GetComponent<VehicleHitbox>().isCorrect = true;
            Destroy(gameObject, 1f);
        }
        else if ((finalPoint.position - rb.position).magnitude > 0.05)
        {
            transform.GetChild(0).GetComponent<VehicleHitbox>().isCorrect = false;
        }
    }
    public void SetFinalPoint(int num)
    {
        spawner = FindAnyObjectByType<HitboxSpawner>();
        finalPoint = spawner.finalPoints[num];
    }
    public Transform GetFinalPoint()
    {
        return finalPoint;
    }
}
