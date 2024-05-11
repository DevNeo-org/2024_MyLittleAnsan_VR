using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleHitbox : MonoBehaviour
{
    [SerializeField] private GameObject correctEffectPrefab;
    [SerializeField] private GameObject wrongEffectPrefab;
    private bool isCorrect = false; // ������Ʈ ��ġ�� ���� ȹ�� ������ ��ġ���� üũ
    private GameObject instateEffectObj;
    private VehicleManager vehicleManager;
    public int ObjectHit()
    {
        if (isCorrect)
        {
            CorrectEffectPlay();
            vehicleManager.ScorePlus();
            Destroy(transform.parent.gameObject, 0.1f);
            return 0;
        }
        else
        {
            WrongEffectPlay();
            Destroy(transform.parent.gameObject, 0.1f);
            return 1;
        }
    }
    private void CorrectEffectPlay()
    {
        //��ƼŬ������ ����
        instateEffectObj = Instantiate(correctEffectPrefab, transform.position, Quaternion.identity);
        ParticleSystem instantEffect = instateEffectObj.GetComponent<ParticleSystem>();
        //��ƼŬ ���� ��ġ�� ũ�� ����
        instateEffectObj.transform.position = transform.position;
        instateEffectObj.transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
        instateEffectObj.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        //��ƼŬ ���
        instantEffect.Play();
        Destroy(instateEffectObj, 1f);
    }
    private void WrongEffectPlay()
    {
        //��ƼŬ������ ����
        instateEffectObj = Instantiate(wrongEffectPrefab, transform.position, Quaternion.identity);
        ParticleSystem instantEffect = instateEffectObj.GetComponent<ParticleSystem>();
        //��ƼŬ ���� ��ġ�� ũ�� ����
        instateEffectObj.transform.position = transform.position;
        instateEffectObj.transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
        instateEffectObj.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        //��ƼŬ ���
        instantEffect.Play();
        Destroy(instateEffectObj, 1f);
    }
    public void SetVehicleManager(VehicleManager temp)
    {
        vehicleManager = temp;
    }
}
