using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGrapped : MonoBehaviour
{
    public OVRInput.Controller controller;

    private void OnTriggerEnter(Collider Collider)
    {
        //���� �浹�� �� ȿ���� ���
        if (Collider.gameObject.name == "Table")
            GetComponent<AudioSource>().Play();
    }

}
