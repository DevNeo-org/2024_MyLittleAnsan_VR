using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    [SerializeField] private float generateTime = 0.5f; // 페인트 튀는 파티클 간격

    public GameObject[] particleList;   // 파티클 리스트

    private float nextGenerate;
    private ParticleSystem particle;

    // 파티클 재생 함수   
    public void PlayParticle(int particleID, Vector3 particlePos)
    {
        if (particleList != null && particleList[particleID] != null && Time.time > nextGenerate)
        {
            nextGenerate = Time.time + generateTime;
            particle = particleList[particleID].GetComponent<ParticleSystem>();
            if (particle.isPlaying)
                particle.Stop();

            // 파티클 오브젝트 생성 후 재생
            ParticleSystem tempParticle = Instantiate(particle, particlePos, new Quaternion());
            tempParticle.Play();
        }
    }
}
