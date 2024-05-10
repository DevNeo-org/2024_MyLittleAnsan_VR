using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selectArea : MonoBehaviour
{
    public ParticleSystem effect;
    public GameObject effectPrefab;
    GameObject instateEffectObj;
    public OVRInput.Controller controller;

    //buildingSample ������Ʈ�� selectArea�� �ݶ��̴� ���� �ȿ� ���� ��
    private void OnTriggerEnter(Collider collider)
    { 
        if(collider.tag == "Buildings" && System.Convert.ToBoolean(PlayerPrefs.GetInt("Token")))
        {
            //���� ȿ�� ��ƼŬ ���
            EffectPlay();
        }
        
    }

    //buildingSample ������Ʈ�� selectArea�� �ݶ��̴� ���� �ȿ� ���� ��
    private void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Buildings" && System.Convert.ToBoolean(PlayerPrefs.GetInt("Token")))
        {
            //���� ȿ�� ��ƼŬ ���� �� ����
            effect.Stop();
            Destroy(instateEffectObj);
        }
          
    }

    void EffectPlay()
    {
        //��ƼŬ������ ����
        instateEffectObj = Instantiate(effectPrefab, transform.position, Quaternion.identity);
        ParticleSystem instantEffect = instateEffectObj.GetComponent<ParticleSystem>();
        //��ƼŬ ���� ��ġ�� ũ�� ����
        effect.transform.position = transform.position;
        effect.transform.localScale = new Vector3(0.08f, 0.08f, 0.08f);
        //��ƼŬ ���
        effect.Play();
        //��ƽ ����
        StartCoroutine(TriggerHaptics());
    }

    //��ƽ
    IEnumerator TriggerHaptics()
    {
        OVRInput.SetControllerVibration(0.3f, 0.3f, controller);
        yield return new WaitForSeconds(1f);
        OVRInput.SetControllerVibration(0f, 0f, controller);
    }

}
