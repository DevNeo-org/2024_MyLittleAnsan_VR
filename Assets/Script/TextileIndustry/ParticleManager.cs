using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public GameObject[] particleList;
    private ParticleSystem particle;
    [SerializeField] private float generateTime = 0.5f;
    private float nextGenerate;

    public void PlayParticle(int particleID, Vector3 particlePos)
    {
        if (particleList != null && particleList[particleID] != null && Time.time > nextGenerate)
        {
            nextGenerate = Time.time + generateTime;
            particle = particleList[particleID].GetComponent<ParticleSystem>();
            if (particle.isPlaying)
                particle.Stop();

            ParticleSystem tempParticle = Instantiate(particle, particlePos, new Quaternion());
            tempParticle.Play();
        }
    }
}
