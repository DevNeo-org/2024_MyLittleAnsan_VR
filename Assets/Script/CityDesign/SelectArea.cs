using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectArea : MonoBehaviour
{
    public ParticleSystem effect;
    public GameObject effectPrefab;
    public GameObject gameManager;
    GameObject instateEffectObj;
    public OVRInput.Controller controller;
        
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
    }

    //buildingSample 오브젝트가 selectArea에 콜라이더 영역 안에 있을 때
    private void OnTriggerEnter(Collider collider)
    { 
        if(collider.tag == "Buildings" && System.Convert.ToBoolean(PlayerPrefs.GetInt("Token")))
        {
            //isOnArea를 true로 설정
            gameManager.GetComponent<Lobby>().SetIsOnArea(true);
            //선택 효과 파티클 재생
            EffectPlay();
        }
        
    }

    void OnTriggerStay(Collider collider)
    {
        if (collider.tag == "Buildings" && System.Convert.ToBoolean(PlayerPrefs.GetInt("Token")))
        {
            //OVRInput.SetControllerVibration(2.5f, 2.5f, controller);
        }
    }

    //buildingSample 오브젝트가 selectArea에 콜라이더 영역 안에 없을 때
    private void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Buildings" && System.Convert.ToBoolean(PlayerPrefs.GetInt("Token")))
        {
            //isOnArea를 false로 설정
            gameManager.GetComponent<Lobby>().SetIsOnArea(false);
            //선택 효과 파티클 정지 및 삭제
            effect.Stop();
            Destroy(instateEffectObj);
            //OVRInput.SetControllerVibration(0f, 0f, controller);
        }
          
    }

    void EffectPlay()
    {
        //파티클프리팹 생성
        instateEffectObj = Instantiate(effectPrefab, transform.position, Quaternion.identity);
        ParticleSystem instantEffect = instateEffectObj.GetComponent<ParticleSystem>();
        //파티클 생성 위치와 크기 설정
        effect.transform.position = transform.position;
        effect.transform.localScale = new Vector3(0.056f, 0.056f, 0.056f);
        //파티클 재생
        effect.Play();
    }

}
