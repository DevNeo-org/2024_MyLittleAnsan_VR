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


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("circle"))
        {
           
            StartCoroutine(TriggerHaptics());
            if (particlePrefab != null)
            {
                GameObject particleInstance = Instantiate(particlePrefab, other.transform.position, Quaternion.identity);
                particleInstance.transform.rotation = Quaternion.Euler(180f, 0f, 0f);
                particleInstance.transform.localScale = particleScale;
                
                Destroy(particleInstance, 1f);
            }
            other.gameObject.GetComponent<circlehit>().ObjectHit();
        }
       
    }
    IEnumerator TriggerHaptics()
    {
        yield return new WaitForSeconds(1f);
        OVRInput.SetControllerVibration(2.5f, 2.5f, controller);
        yield return new WaitForSeconds(1f);
        OVRInput.SetControllerVibration(0f, 0f, controller);
    }
}
