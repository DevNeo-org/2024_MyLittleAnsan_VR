using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Lobby : MonoBehaviour
{
    //건물 모형 프리팹
    public GameObject[] samplePrefabs;
    //건물 프리팹
    public GameObject[] buildingPrefabs;
    //씬 전환 버튼 오브젝트
    public GameObject[] buttonObjects;
    //건설 구역 오브젝트
    public GameObject[] AreaObjects;
    //저장 데이터 관리 오브젝트
    public GameObject dataManager;
    //건설 토큰
    public bool token;
    
    public string[] samples = new string[] { "sample1", "sample2", "sample3", "sample4", "sample5" };
    public string[] areas = new string[] { "area1","area2","area3"};
    
    public bool[] isClear = new bool[3];
    public bool[] sampleDestroyed = new bool[5];
    public int[] areaBuild = new int[3];

    public ParticleSystem completeEffect;
    public GameObject completeEffectPrefab;
    GameObject completeEffectObject;


    GameObject area;

    void Start()
    {
        //씬 로드 시 토큰이 없다면 맨 처음이므로 초기화
        //토큰이 없으면 건설 불가능 및 씬 전환 가능
        if (!(PlayerPrefs.HasKey("Token")))
        {
            //모든 저장값 삭제
            dataManager.GetComponent<DataManager>().DataClear();
            //토큰 초기화
            PlayerPrefs.SetInt("Token", System.Convert.ToInt16(false));
            token = false;
            //건물 모형 사용 여부 초기화
            for (int i = 0; i < 5; i++)
            {
                sampleDestroyed[i] = false;
            }
            
            for(int i = 0; i < 3; i++)
            {
                //씬 클리어 여부 초기화
                isClear[i] = false;
                //구역 건설 여부 초기화
                // 0 : 미건설 , 1 ~ 5 : 각 번호의 건물 건설
                areaBuild[i] = 0;
            }
        }

        //토큰이 있으면 건설 가능 및 씬 전환 불가능
        else
        {
            token = true;
            
            for (int i = 0; i < 3; i++)
            {
                //씬 클리어 여부 불러오기
                isClear[i] = dataManager.GetComponent<DataManager>().GetClear(i);
                //구역 건설 여부 불러오기
                areaBuild[i] = dataManager.GetComponent<DataManager>().GetAreaState(i);
            }
            for (int i = 0; i < 5; i++)
            {
                //건물 모형 사용 여부 불러오기
                sampleDestroyed[i] = dataManager.GetComponent<DataManager>().GetSampleDestroyed(i);
            }
        }
        makePrefab();
    }
    

    void makePrefab()
    {
        //건물 모형이 사용되지 않았으면 프리팹 생성
        for (int i = 0; i < 5; i++)
        {
            if (!sampleDestroyed[i])
            {
                Instantiate(samplePrefabs[i], samplePrefabs[i].transform.position, samplePrefabs[i].transform.rotation);
            }
        }
        
        for (int i = 0; i < 3; i++)
        {
            //씬 클리어 되었으면 씬 전환 버튼 비활성화
            if (isClear[i])
            {
                buttonObjects[i].SetActive(false);
            }
            //1. 구역에 건물이 건설되어있다면 구역오브젝트 비활성화
            //2. 해당 구역에 건설된 건물 프리팹 생성
            if (areaBuild[i] != 0)
            {
                AreaObjects[i].SetActive(false);
                Instantiate(buildingPrefabs[areaBuild[i]-1], AreaObjects[i].transform.position, buildingPrefabs[areaBuild[i]-1].transform.rotation);
            }
        }
    }
    //게임 클리어 이펙트
    public void playCompleteEffect()
    {
        //파티클프리팹 생성
        completeEffectObject = Instantiate(completeEffectPrefab, transform.position, Quaternion.identity);
        ParticleSystem instantEffect1 = completeEffectObject.GetComponent<ParticleSystem>();
        //파티클 생성 위치와 크기 설정
        completeEffect.transform.position = transform.position;
        completeEffect.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        //파티클 재생
        completeEffect.Play();
    }


}
