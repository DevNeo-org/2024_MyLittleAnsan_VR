using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleHitbox : MonoBehaviour
{
    [SerializeField] private GameObject correctEffectPrefab;
    [SerializeField] private GameObject wrongEffectPrefab;
    private bool isCorrect = false; // 오브젝트 위치가 점수 획득 가능한 위치인지 체크
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
        //파티클프리팹 생성
        instateEffectObj = Instantiate(correctEffectPrefab, transform.position, Quaternion.identity);
        ParticleSystem instantEffect = instateEffectObj.GetComponent<ParticleSystem>();
        //파티클 생성 위치와 크기 설정
        instateEffectObj.transform.position = transform.position;
        instateEffectObj.transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
        instateEffectObj.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        //파티클 재생
        instantEffect.Play();
        Destroy(instateEffectObj, 1f);
    }
    private void WrongEffectPlay()
    {
        //파티클프리팹 생성
        instateEffectObj = Instantiate(wrongEffectPrefab, transform.position, Quaternion.identity);
        ParticleSystem instantEffect = instateEffectObj.GetComponent<ParticleSystem>();
        //파티클 생성 위치와 크기 설정
        instateEffectObj.transform.position = transform.position;
        instateEffectObj.transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
        instateEffectObj.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        //파티클 재생
        instantEffect.Play();
        Destroy(instateEffectObj, 1f);
    }
    public void SetVehicleManager(VehicleManager temp)
    {
        vehicleManager = temp;
    }
}
