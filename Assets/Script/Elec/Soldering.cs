using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldering : MonoBehaviour
{
    // Start is called before the first frame update


    public OVRInput.Controller controller;
    
    public GameObject particlePrefab;
    private Vector3 particleScale = new Vector3(0.1f, 0.05f, 0.1f);
    private float enterTime = 0f;
    


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Circle"))
        {
            enterTime += Time.deltaTime;
            Debug.Log("trigger time" + enterTime);
            GameObject particleInstance = Instantiate(particlePrefab, other.transform.position, Quaternion.identity);
            OVRInput.SetControllerVibration(2.5f, 2.5f, controller);
            if (particlePrefab != null)
            {
                particleInstance.transform.rotation = Quaternion.Euler(180f, 0f, 0f);
                particleInstance.transform.localScale = particleScale;
                Destroy(particleInstance, 0.3f);
            }
            if (enterTime > 1f)
            {
                other.gameObject.GetComponent<Circlehit>().ObjectHit();
                OVRInput.SetControllerVibration(0f, 0f, controller);
                enterTime = 0f;
            }
        }
        else
        {
            OVRInput.SetControllerVibration(0f, 0f, controller);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        OVRInput.SetControllerVibration(0f, 0f, controller);
        enterTime = 0f;
    }
    private void OnTriggerExit(Collider other)
    {
        OVRInput.SetControllerVibration(0f, 0f, controller);
        enterTime = 0f;
    }

}
