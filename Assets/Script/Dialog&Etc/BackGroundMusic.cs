using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackGroundMusic : MonoBehaviour // BGM ���� ��ũ��Ʈ
{
    AudioSource audioSourceMain, audioSourceVehicle, audioSourceElectric, audioSourceTextile;
    private void Awake()
    {
        // ������ �� �� ����� �ҽ��� child ������Ʈ�� ����
        audioSourceMain = transform.GetChild(0).GetComponent<AudioSource>();
        audioSourceVehicle = transform.GetChild(1).GetComponent<AudioSource>();
        audioSourceElectric = transform.GetChild(2).GetComponent<AudioSource>();
        audioSourceTextile = transform.GetChild(3).GetComponent<AudioSource>();
        var obj = FindObjectsOfType<BackGroundMusic>();
        if (obj.Length == 1) // �ߺ��Ǵ� BackGroundMusic �ı�
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Update() // �� �� BGM �Ҵ�
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
    private void StopExceptMain() // ���� BGM ���� �� �̿��� BGM ����
    {
        audioSourceVehicle.Stop();
        audioSourceElectric.Stop();
        audioSourceTextile.Stop();
    }
}
