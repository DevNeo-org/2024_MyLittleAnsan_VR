using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxMovement : MonoBehaviour
{
    [SerializeField] float force;
    [SerializeField] private Material[] materials;
    int index; // ��ǰ �׷��� child ����
    HitboxSpawner spawner;
    Transform finalPoint;
    MeshRenderer meshRenderer;
    Vector3 direction;
    Rigidbody rb;
    float rotationSpeedX, rotationSpeedY, rotationSpeedZ;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        direction = (finalPoint.position - rb.position).normalized;
        rb.velocity = direction * force;
        index = Random.Range(0, 10);
        transform.GetChild(1).GetChild(index).gameObject.SetActive(true);
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
            finalPoint.GetChild(0).GetChild(0).GetChild(1).GetChild(index).gameObject.SetActive(true); // Sub ������Ʈ�� ��ǰ �׷��� Ȱ��ȭ
            meshRenderer.material = materials[0]; // SubObjectColorTrue
            transform.GetChild(0).GetComponent<VehicleHitbox>().isCorrect = true;
            finalPoint.GetChild(0).GetChild(0).GetComponent<HitPoint>().Deactivate(materials[1]);
            StartCoroutine(DestroyObject());
        }
        else if ((finalPoint.position - rb.position).magnitude > 0.05)
        {
            finalPoint.GetChild(0).GetChild(0).GetChild(1).GetChild(index).gameObject.SetActive(false);
            meshRenderer.material = materials[1]; // SubObjectColorFalse
            transform.GetChild(0).GetComponent<VehicleHitbox>().isCorrect = false;
        }
    }
    public void SetFinalPoint(int num)
    {
        spawner = FindAnyObjectByType<HitboxSpawner>();
        finalPoint = spawner.finalPoints[num];
        finalPoint.GetChild(0).GetChild(0).gameObject.SetActive(true);
        meshRenderer = finalPoint.GetChild(0).GetChild(0).GetChild(0).GetComponent<MeshRenderer>();
    }
    IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(1f); // 1�� ���
        Destroy(gameObject);
    }
    public Transform GetFinalPoint()
    {
        return finalPoint;
    }
    public void StopMovement()
    {
        rb.velocity = Vector3.zero;
    }
}
