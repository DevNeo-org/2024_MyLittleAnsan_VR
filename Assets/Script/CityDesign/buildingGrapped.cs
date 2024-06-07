using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buildingGrapped : MonoBehaviour
{
    public OVRInput.Controller controller;

    private void OnTriggerEnter(Collider Collider)
    {
        //��ƽ ���
        //StartCoroutine(TriggerHaptics());
        //���� �浹�� �� ȿ���� ���
        if (Collider.gameObject.name == "Table")
            GetComponent<AudioSource>().Play();
    }

    IEnumerator TriggerHaptics()
    {
        OVRInput.SetControllerVibration(0.3f, 0.3f, controller);
        yield return new WaitForSeconds(0.1f);
        OVRInput.SetControllerVibration(0f, 0f, controller);
    }

}
