using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackGroundMusic : MonoBehaviour // BGM 관리 스크립트
{
    AudioSource audioSourceMain, audioSourceVehicle, audioSourceElectric, audioSourceTextile;
    private void Awake()
    {
        // 사전에 씬 별 오디오 소스를 child 오브젝트에 설정
        audioSourceMain = transform.GetChild(0).GetComponent<AudioSource>();
        audioSourceVehicle = transform.GetChild(1).GetComponent<AudioSource>();
        audioSourceElectric = transform.GetChild(2).GetComponent<AudioSource>();
        audioSourceTextile = transform.GetChild(3).GetComponent<AudioSource>();
        var obj = FindObjectsOfType<BackGroundMusic>();
        if (obj.Length == 1) // 중복되는 BackGroundMusic 파괴
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Update() // 씬 별 BGM 할당
    {
        if (SceneManager.GetActiveScene().name == "AutomobIndScene")
        {
            audioSourceMain.Stop();
            if (!audioSourceVehicle.isPlaying)
            {
                audioSourceVehicle.Play();
            }
        }
        else if (SceneManager.GetActiveScene().name == "Electronic")
        {
            audioSourceMain.Stop();
            if (!audioSourceElectric.isPlaying)
            {
                audioSourceElectric.Play();
            }
        }
        else if (SceneManager.GetActiveScene().name == "TextileIndScene")
        {
            audioSourceMain.Stop();
            if (!audioSourceTextile.isPlaying)
            {
                audioSourceTextile.Play();
            }
        }
        else if (SceneManager.GetActiveScene().name == "CityDesign")
        {
            StopExceptMain();
            if (!audioSourceMain.isPlaying)
            {
                audioSourceMain.Play();
            }
        }
        else if (SceneManager.GetActiveScene().name == "Title")
        {
            StopExceptMain();
            if (!audioSourceMain.isPlaying)
            {
                audioSourceMain.Play();
            }
        }
    }
    private void StopExceptMain() // 메인 BGM 실행 시 이외의 BGM 정지
    {
        audioSourceVehicle.Stop();
        audioSourceElectric.Stop();
        audioSourceTextile.Stop();
    }
}
