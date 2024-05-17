using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildPosition : MonoBehaviour
{

    public GameObject[] builidngPrefabs;
    public bool isBuildComplete = false;
    GameObject selctEffect;

    public GameObject gameManager;
    public GameObject dataManager;

    public ParticleSystem buildEffect;
    public GameObject buildEffectPrefab;
    GameObject buildEffectObject;
    public OVRInput.Controller controller;
    string[] buildingSamples = new string[] { "sample1", "sample2", "sample3", "sample4", "sample5" };
    string[] SampleNames = new string[] { "BuildingSample1(Clone)", "BuildingSample2(Clone)", "BuildingSample3(Clone)", "BuildingSample4(Clone)", "BuildingSample5(Clone)" };
    string[] areas = new string[] { "area1", "area2", "area3" };



    //buildPosition에 빌딩 오브젝트가 닿으면 빌딩 생성
    private void OnTriggerEnter(Collider Collider)
    {
        //토큰이 있는지 확인
        if (Collider.tag == "Buildings" && System.Convert.ToBoolean(PlayerPrefs.GetInt("Token")))
        {
            //효과음 재생
            gameManager.GetComponent<AudioManager>().PlaySound(2);
            //건설 이펙트
            playBuildEffect();
            //햅틱 실행
            StartCoroutine(TriggerHaptics());
            //buildPosition에 빌딩 프리팹 생성
            string bulidingType = Collider.gameObject.name;
            for(int i = 0; i<5; i++)
            {
                if (SampleNames[i] == bulidingType)
                {
                    //건물 모형 사용
                    PlayerPrefs.SetInt(buildingSamples[i], System.Convert.ToInt16(true));
                    //건물 생성
                    Instantiate(builidngPrefabs[i], transform.position, builidngPrefabs[i].transform.rotation);
                    //현재 구역 확인
                    string areaName = transform.parent.gameObject.name;
                    for (int j = 0; j < 3 ; j++){
                        if (areas[j] == areaName)
                        {
                            PlayerPrefs.SetInt(areas[j], i+1);
                        }
                    }
                }
            }
            //토큰 사용
            PlayerPrefs.SetInt("Token", System.Convert.ToInt16(false));
            //buildingSample 오브젝트 삭제
            Destroy(Collider.gameObject);
            //buildPosition의 콜라이더 비활성화
            gameObject.GetComponent<BoxCollider>().enabled = false;
            //SelectArea 오브젝트 삭제
            Destroy(transform.parent.gameObject);
            //선택 이펙트 삭제
            selctEffect = GameObject.Find("Area_circles_blue(Clone)");
            Destroy(selctEffect);

            if (PlayerPrefs.GetInt(areas[0]) != 0 && PlayerPrefs.GetInt(areas[1]) != 0 && PlayerPrefs.GetInt(areas[2]) != 0)
            {
                gameManager.GetComponent<Lobby>().playCompleteEffect();
                Debug.Log("게임 클리어");
            }
        }
    }

    void Update()
    {
        
    }

    public bool returnBuildComplete()
    {
        return isBuildComplete;
    }

    void playBuildEffect()
    {
        //파티클프리팹 생성
        buildEffectObject = Instantiate(buildEffectPrefab, transform.position, Quaternion.identity);
        ParticleSystem instantEffect = buildEffectObject.GetComponent<ParticleSystem>();
        //파티클 생성 위치와 크기 설정
        buildEffect.transform.position = transform.position;
        buildEffect.transform.localScale = new Vector3(0.08f, 0.08f, 0.08f);
        //파티클 재생
        buildEffect.Play();
    }

    //햅틱
    IEnumerator TriggerHaptics()
    {
        OVRInput.SetControllerVibration(1f, 1f, controller);
        yield return new WaitForSeconds(1f);
        OVRInput.SetControllerVibration(0f, 0f, controller);
    }
}
