using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selectArea : MonoBehaviour
{
    public ParticleSystem effect;
    public GameObject effectPrefab;
    GameObject instateEffectObj;
    public OVRInput.Controller controller;

    //buildingSample 오브젝트가 selectArea에 콜라이더 영역 안에 있을 때
    private void OnTriggerEnter(Collider collider)
    { 
        if(collider.tag == "Buildings" && System.Convert.ToBoolean(PlayerPrefs.GetInt("Token")))
        {
            //선택 효과 파티클 재생
            EffectPlay();
        }
        
    }

    //buildingSample 오브젝트가 selectArea에 콜라이더 영역 안에 없을 때
    private void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Buildings" && System.Convert.ToBoolean(PlayerPrefs.GetInt("Token")))
        {
            //선택 효과 파티클 정지 및 삭제
            effect.Stop();
            Destroy(instateEffectObj);
        }
          
    }

    void EffectPlay()
    {
        //파티클프리팹 생성
        instateEffectObj = Instantiate(effectPrefab, transform.position, Quaternion.identity);
        ParticleSystem instantEffect = instateEffectObj.GetComponent<ParticleSystem>();
        //파티클 생성 위치와 크기 설정
        effect.transform.position = transform.position;
        effect.transform.localScale = new Vector3(0.08f, 0.08f, 0.08f);
        //파티클 재생
        effect.Play();
        //햅틱 실행
        StartCoroutine(TriggerHaptics());
    }

    //햅틱
    IEnumerator TriggerHaptics()
    {
        OVRInput.SetControllerVibration(0.3f, 0.3f, controller);
        yield return new WaitForSeconds(1f);
        OVRInput.SetControllerVibration(0f, 0f, controller);
    }

}
