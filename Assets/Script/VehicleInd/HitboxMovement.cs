using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxMovement : MonoBehaviour
{
    [SerializeField] float force; // 이동속도
    [SerializeField] private Material[] materials;
    int index; // 부품 그래픽 child 순서
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
        // 랜덤 회전속도 설정
        rotationSpeedX = Random.Range(30,110);
        rotationSpeedY = Random.Range(30, 110);
        rotationSpeedZ = Random.Range(30, 110);
    }
    void Update()
    {
        transform.Rotate(new Vector3(rotationSpeedX * Time.deltaTime, rotationSpeedY * Time.deltaTime, rotationSpeedZ * Time.deltaTime)); // 랜덤하게 회전하며 이동
        if ((finalPoint.position - rb.position).magnitude < 0.05)
        {
            finalPoint.GetChild(0).GetChild(0).GetChild(1).GetChild(index).gameObject.SetActive(true); // Sub 오브젝트의 부품 그래픽 활성화
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
    public void TurnOff()
    {
        finalPoint.GetChild(0).GetChild(0).GetComponent<HitPoint>().Deactivate(materials[1]);
    }
    public void SetFinalPoint(int num) // 최종 도달 위치
    {
        spawner = FindAnyObjectByType<HitboxSpawner>();
        finalPoint = spawner.finalPoints[num];
        finalPoint.GetChild(0).GetChild(0).gameObject.SetActive(true);
        meshRenderer = finalPoint.GetChild(0).GetChild(0).GetChild(0).GetComponent<MeshRenderer>();
    }
    IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(1f); // 1초 대기
        Destroy(gameObject);
    }
    public Transform GetFinalPoint()
    {
        return finalPoint;
    }
    public void StopMovement() // hit된 이후 위치 고정
    {
        rb.velocity = Vector3.zero;
    }
}
