using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wrench : MonoBehaviour
{
    public OVRInput.Controller controller; // 좌우 개별적인 컨트롤러 할당
    void Start()
    {
        StartCoroutine(StartTriggerHaptics()); // 컨트롤러의 렌치 활성화
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hitbox")) // 히트박스 hit
        {
            other.gameObject.GetComponent<VehicleHitbox>().ObjectHit();
            GetComponent<AudioSource>().Play();
            StartCoroutine(TriggerHaptics());
        }
    }
    IEnumerator TriggerHaptics() // 렌치로 오브젝트 hit 시 진동
    {
        OVRInput.SetControllerVibration(2.5f, 2.5f, controller);
        yield return new WaitForSeconds(0.5f);
        OVRInput.SetControllerVibration(0f, 0f, controller);
    }
    IEnumerator StartTriggerHaptics() // 시작 트리거로 렌치를 집었을 시 진동
    {
        OVRInput.SetControllerVibration(2.5f, 2.5f, controller);
        yield return new WaitForSeconds(0.3f);
        OVRInput.SetControllerVibration(0f, 0f, controller);
    }
}
