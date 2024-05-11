using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wrench : MonoBehaviour
{
    public OVRInput.Controller controller;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hitbox"))
        {
            Debug.Log("trigger");
            other.gameObject.GetComponent<VehicleHitbox>().ObjectHit();
            StartCoroutine(TriggerHaptics());
        }
    }
    IEnumerator TriggerHaptics()
    {
        OVRInput.SetControllerVibration(2.5f, 2.5f, controller);
        yield return new WaitForSeconds(1f);
        OVRInput.SetControllerVibration(0f, 0f, controller);
    }
}