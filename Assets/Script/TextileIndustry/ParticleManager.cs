using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    [SerializeField] private float generateTime = 0.5f; // ����Ʈ Ƣ�� ��ƼŬ ����

    public GameObject[] particleList;   // ��ƼŬ ����Ʈ

    private float nextGenerate;
    private ParticleSystem particle;

    // ��ƼŬ ��� �Լ�   
    public void PlayParticle(int particleID, Vector3 particlePos)
    {
        if (particleList != null && particleList[particleID] != null && Time.time > nextGenerate)
        {
            nextGenerate = Time.time + generateTime;
            particle = particleList[particleID].GetComponent<ParticleSystem>();
            if (particle.isPlaying)
                particle.Stop();

            // ��ƼŬ ������Ʈ ���� �� ���
            ParticleSystem tempParticle = Instantiate(particle, particlePos, new Quaternion());
            tempParticle.Play();
        }
    }
}
