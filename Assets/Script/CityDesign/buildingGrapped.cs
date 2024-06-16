using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGrapped : MonoBehaviour
{
    public OVRInput.Controller controller;

    private void OnTriggerEnter(Collider Collider)
    {
        //땅과 충돌할 때 효과음 재생
        if (Collider.gameObject.name == "Table")
            GetComponent<AudioSource>().Play();
    }

}
