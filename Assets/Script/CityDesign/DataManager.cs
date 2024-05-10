using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;

public class DataManager : MonoBehaviour
{
    string[] buildingSamples = new string[] { "sample1", "sample2", "sample3", "sample4", "sample5" };
    string[] SceneNames = new string[] { "AutoMobile", "Electronic", "Textile", "AutoMobile" };

    //게임 시작시 모든 데이터 삭제
    public void DataClear()
    {
        PlayerPrefs.DeleteAll();
    }

    //각 체험의 클리어 여부 저장
    public void SetClear()
    {
        //토큰 획득
        PlayerPrefs.SetInt("Token", System.Convert.ToInt16(true));
        //현재 씬의 인덱스 불러오기
        int sceneNum = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt(SceneNames[sceneNum - 1], System.Convert.ToInt16(true));
    }

    //각 체험의 클리어 여부 반환
    public bool GetClear(int sceneNum)
    {
        bool isClear = System.Convert.ToBoolean(PlayerPrefs.GetInt(SceneNames[sceneNum]));
        return isClear;
    }

    public bool GetSampleDestroyed(int sampleNum)
    {
        bool isDestroyed = System.Convert.ToBoolean(PlayerPrefs.GetInt(buildingSamples[sampleNum]));
        return isDestroyed;
    }

    //건물 모형 사용됨
    public void SampleDestroyed(int num)
    {
        PlayerPrefs.SetInt(buildingSamples[num], System.Convert.ToInt16(true));
    }
}
