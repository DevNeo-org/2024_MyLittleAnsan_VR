using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wrench : MonoBehaviour
{
    public OVRInput.Controller controller; // �¿� �������� ��Ʈ�ѷ� �Ҵ�
    void Start()
    {
        StartCoroutine(StartTriggerHaptics()); // ��Ʈ�ѷ��� ��ġ Ȱ��ȭ
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hitbox")) // ��Ʈ�ڽ� hit
        {
            other.gameObject.GetComponent<VehicleHitbox>().ObjectHit();
            GetComponent<AudioSource>().Play();
            StartCoroutine(TriggerHaptics());
        }
    }
    IEnumerator TriggerHaptics() // ��ġ�� ������Ʈ hit �� ����
    {
        OVRInput.SetControllerVibration(2.5f, 2.5f, controller);
        yield return new WaitForSeconds(0.5f);
        OVRInput.SetControllerVibration(0f, 0f, controller);
    }
    IEnumerator StartTriggerHaptics() // ���� Ʈ���ŷ� ��ġ�� ������ �� ����
    {
        OVRInput.SetControllerVibration(2.5f, 2.5f, controller);
        yield return new WaitForSeconds(0.3f);
        OVRInput.SetControllerVibration(0f, 0f, controller);
    }
}
