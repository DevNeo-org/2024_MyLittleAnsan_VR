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
    public AudioSource effectsound;


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Circle")) //원에 닿았을때 파티클 on/off 여부
        {
            enterTime += Time.deltaTime;
            
            GameObject particleInstance = Instantiate(particlePrefab, other.transform.position, Quaternion.identity); //파티클 생성
            OVRInput.SetControllerVibration(2.5f, 2.5f, controller);
            
            if (particlePrefab != null) //파티클 위치 
            {
                particleInstance.transform.rotation = Quaternion.Euler(180f, 0f, 0f);
                particleInstance.transform.localScale = particleScale;
                Destroy(particleInstance, 0.3f);
            }
            if (enterTime > 1f) //일정 시간 뒤에 원이 꺼지게 함
            {
                other.gameObject.GetComponent<Circlehit>().ObjectHit();
                effectsound.Pause();
                OVRInput.SetControllerVibration(0f, 0f, controller);
                enterTime = 0f;
            }
        }
        else
        {
            OVRInput.SetControllerVibration(0f, 0f, controller);
        }
    }
    private void OnTriggerEnter(Collider other) //원에 닿았을 때 효과음 키기
    {
        if (other.gameObject.CompareTag("Circle"))
        {
            effectsound.Play();
        }
        OVRInput.SetControllerVibration(0f, 0f, controller);
        enterTime = 0f;
    }
    private void OnTriggerExit(Collider other) //빠져나왔을 때 효과음 끄기
    {
        OVRInput.SetControllerVibration(0f, 0f, controller);
        effectsound.Pause();
        enterTime = 0f;
    }
   
}
