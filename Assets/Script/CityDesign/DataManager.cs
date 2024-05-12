using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;

public class DataManager : MonoBehaviour
{
    public bool autoMobClear;
    public bool elecClear;
    public bool textileClear;

    private void Start()
    {
        //저장된 데이터 값이 없다면 초기화
        if (!(PlayerPrefs.HasKey("AutoMobile") || PlayerPrefs.HasKey("Electronic") || PlayerPrefs.HasKey("Textile")))
        {
            autoMobClear = false;
            elecClear = false;
            textileClear = false;
        }
        //저장된 데이터가 있다면 불러오기
        else
        {
            autoMobClear = GetClear(1);
            elecClear = GetClear(2);
            textileClear = GetClear(3);
        }
    }

    //게임 시작시 모든 데이터 삭제
    public void DataClear()
    {
        PlayerPrefs.DeleteAll();
    }

    //각 체험의 클리어 여부 저장
    public void SetClear()
    {
        //현재 씬의 인덱스 불러오기
        int sceneNum = SceneManager.GetActiveScene().buildIndex;
        switch (sceneNum)
        {
            //각 씬을 클리어로 저장
            case 1:
                PlayerPrefs.SetInt("AutoMobile", System.Convert.ToInt16(true));
                break;
            case 2:
                PlayerPrefs.SetInt("Electronic", System.Convert.ToInt16(true));
                break;
            case 3:
                PlayerPrefs.SetInt("Textile", System.Convert.ToInt16(true));
                break;
            default:
                break;

        } 
    }

    //각 체험의 클리어 여부 반환
    public bool GetClear(int sceneNum)
    {
        bool isClear;
        switch (sceneNum)
        {
            //각 씬의 클리어 여부 반환
            case 1:
                isClear = System.Convert.ToBoolean(PlayerPrefs.GetInt("AutoMobile"));
                break;
            case 2:
                isClear = System.Convert.ToBoolean(PlayerPrefs.GetInt("Electronic"));
                break;
            case 3:
                isClear = System.Convert.ToBoolean(PlayerPrefs.GetInt("Textile"));
                break;
            default:
                isClear = false;
                break;
        }
        return isClear;
    }

}
