using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldering : MonoBehaviour
{
    // Start is called before the first frame update


    public OVRInput.Controller controller;
    int score=0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public int SendScore()
    {
        return score;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("circle"))
        {
            other.gameObject.GetComponent<circlehit>().ObjectHit();
            score++;
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
