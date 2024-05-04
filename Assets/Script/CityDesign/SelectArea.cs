using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selectArea : MonoBehaviour
{
    public ParticleSystem effect;
    public GameObject effectPrefab;
    GameObject instateEffectObj;

    // Collider ������Ʈ�� is Trigger�� false�� ���·� �浹�� �������� ��
    private void OnTriggerEnter(Collider Collider)
    { 
        if(Collider.tag == "Buildings")
        {
            EffectPlay();
        }
        
    }

    // Collider ������Ʈ�� is Trigger�� false�� ���·� �浹�� ������ ��
    private void OnTriggerExit(Collider Collider)
    {
        if (Collider.tag == "Buildings")
        {
            effect.Stop();
            Destroy(instateEffectObj);
        }
          
    }
    void EffectPlay()
    {
        instateEffectObj = Instantiate(effectPrefab, transform.position, Quaternion.identity);
        ParticleSystem instantEffect = instateEffectObj.GetComponent<ParticleSystem>();
        effect.transform.position = transform.position;
        effect.transform.localScale = new Vector3(0.08f, 0.08f, 0.08f);
        effect.Play();
    }

}
