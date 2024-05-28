using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class TextileManager : MonoBehaviour
{
    [SerializeField] GameObject menu; // 일시정지 메뉴
    [SerializeField] GameObject resultMenu; // 결과창
    [SerializeField] TextMeshPro timeText;
    [SerializeField] GameObject leftRayController;
    [SerializeField] GameObject rightRayController;
    [SerializeField] private OVRControllerHelper controllerHelperLeft;
    [SerializeField] private OVRControllerHelper controllerHelperRight;
    [SerializeField] GameObject paintGun;
    [SerializeField] GameObject line;
    [SerializeField] private GameObject closeButton;
    [SerializeField] private GameObject Timer;
    [SerializeField] ParticleSystem shine;
    [SerializeField] private GameObject[] celebrates;
    [SerializeField] private GameObject[] buckets;

    Timer timer;
    DataManager dataManager;
    Gun gun;
    Clothes clothes;
    private bool isMenuOn = false;
    private bool gameEnd = false; // 게임 시간 종료 여부 확인


    private void Start()
    {
        dataManager = FindAnyObjectByType<DataManager>();
        clothes = FindAnyObjectByType<Clothes>();
        gun = FindAnyObjectByType<Gun>();
        menu.SetActive(false);
        resultMenu.SetActive(false);
        timer = FindAnyObjectByType<Timer>();
        leftRayController.SetActive(false); 
        rightRayController.SetActive(false);
        paintGun.gameObject.SetActive(false);
        line.gameObject.SetActive(false);
    }
    void Update()
    {
        if (gameEnd) return;
        gameEnd = timer.GetBool();
        if (gameEnd) // 시간 종료 첫 확인 시 실행
        {
            dataManager.SetClear();
            for (int i=0; i < celebrates.Length; i++)
            {
                ParticleSystem p = celebrates[i].GetComponent<ParticleSystem>();
                p.Play();
            }
            for (int i=0; buckets.Length > i; i++)
            {
                Animator anim = buckets[i].GetComponent<Animator>();
                anim.SetBool("isClear", true);
            }
            clothes.PlayAnim();
            GetComponent<AudioSource>().Play();
            resultMenu.SetActive(true);
            Timer.gameObject.SetActive(false);
            closeButton.gameObject.SetActive(false);
            shine.Stop();

            rightRayController.SetActive(false);
            Invoke("OpenMenu", 3f);
        }
        if (!isMenuOn)
        {
            if (OVRInput.GetDown(OVRInput.Button.One) || OVRInput.GetDown(OVRInput.Button.Two))
            {
                OpenMenu();
                rightRayController.SetActive(true);
            }
            else if (OVRInput.GetDown(OVRInput.Button.Three) || OVRInput.GetDown(OVRInput.Button.Four))
            {
                OpenMenu();
                leftRayController.SetActive(true);
            }
        }
    }


    public void LoadTitle()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Title");
    }
    public void LoadCityScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("CityDesign");
    }
    public void RestartScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("TextileIndScene");
    }
    public void EndGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 어플리케이션 종료
#endif
    }

    public void CloseMenu()
    {
        Time.timeScale = 1;
        isMenuOn = false;
        menu.SetActive(false); 
        leftRayController.SetActive(false); rightRayController.SetActive(false);

        controllerHelperLeft.m_showState = OVRInput.InputDeviceShowState.ControllerNotInHand;
        controllerHelperRight.m_showState = OVRInput.InputDeviceShowState.ControllerNotInHand;
        paintGun.gameObject.SetActive(true);
        line.gameObject.SetActive(true);
    }
    private void OpenMenu()
    {
        timeText.text = "일시 정지";
        Time.timeScale = 0;
        isMenuOn = true;
        menu.SetActive(true);
        paintGun.gameObject.SetActive(false);
        line.gameObject.SetActive(false);
        controllerHelperLeft.m_showState = OVRInput.InputDeviceShowState.ControllerInHandOrNoHand;
        controllerHelperRight.m_showState = OVRInput.InputDeviceShowState.ControllerInHandOrNoHand;
    }

    public bool IsMenuOn()
    {
        return isMenuOn;
    }

    public void EndManual()
    {
        paintGun.gameObject.SetActive(true);
        line.gameObject.SetActive(true);
        timer.StartGame();
        gun.StartGame();
    }
}
