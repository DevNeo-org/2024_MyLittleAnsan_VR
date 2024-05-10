using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Lobby : MonoBehaviour
{
    public GameObject[] samplePrefabs;
    public GameObject[] buttonObjects;
    public GameObject dataManager;

    public bool token;

    public string[] samples = new string[] { "sample1", "sample2", "sample3", "sample4", "sample5" };
    public bool[] isClear = new bool[3];
    public bool[] sampleDestroyed = new bool[5];

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
            //씬 클리어 여부 초기화
            for(int i = 0; i < 3; i++)
            {
                isClear[i] = false;
            }
        }

        //토큰이 있으면 건설 가능 및 씬 전환 불가능
        else
        {
            token = true;
            //씬 클리어 여부 불러오기
            for (int i = 0; i < 3; i++)
            {
                isClear[i] = dataManager.GetComponent<DataManager>().GetClear(i);
            }
            for (int i = 0; i < 5; i++)
            {
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
            //bool isDestroyed = System.Convert.ToBoolean(PlayerPrefs.GetInt(samples[i]));
            if (!sampleDestroyed[i])
            {
                Instantiate(samplePrefabs[i], samplePrefabs[i].transform.position, samplePrefabs[i].transform.rotation);
            }
        }
        //씬 클리어 되었으면 씬 전환 버튼 비활성화
        for (int i = 0; i < 3; i++)
        {
            if (isClear[i])
            {
                buttonObjects[i].SetActive(false);
            }
        }
    }

    
}
