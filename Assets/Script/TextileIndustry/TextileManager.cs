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
    [SerializeField] GameObject Line;
    [SerializeField] private float delayTime = 30f;
    [SerializeField] private GameObject closeButton;

    Timer timer;
    DataManager dataManager;
    private bool isMenuOn = false;
    private bool gameEnd = false; // 게임 시간 종료 여부 확인


    private void Start()
    {
        dataManager = FindAnyObjectByType<DataManager>();
        menu.SetActive(false);
        resultMenu.SetActive(false);
        timer = FindAnyObjectByType<Timer>();
        Invoke("StartDelay", delayTime);
        leftRayController.SetActive(false); 
        rightRayController.SetActive(false);
    }
    void Update()
    {
        if (gameEnd) return;
        gameEnd = timer.GetBool();
        if (gameEnd) // 시간 종료 첫 확인 시 실행
        {
            dataManager.SetClear();
            resultMenu.SetActive(true);
            closeButton.gameObject.SetActive(false);
            OpenMenu();
            rightRayController.SetActive(true);
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

    private void StartDelay()
    {
        timer.StartGame();
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
        menu.SetActive(false); leftRayController.SetActive(false); rightRayController.SetActive(false);

        controllerHelperLeft.m_showState = OVRInput.InputDeviceShowState.ControllerNotInHand;
        controllerHelperRight.m_showState = OVRInput.InputDeviceShowState.ControllerNotInHand;
        paintGun.gameObject.SetActive(true);
        Line.gameObject.SetActive(true);
    }
    private void OpenMenu()
    {
        timeText.text = "일시 정지";
        Time.timeScale = 0;
        isMenuOn = true;
        menu.SetActive(true);
        paintGun.gameObject.SetActive(false);
        Line.gameObject.SetActive(false);
        controllerHelperLeft.m_showState = OVRInput.InputDeviceShowState.ControllerInHandOrNoHand;
        controllerHelperRight.m_showState = OVRInput.InputDeviceShowState.ControllerInHandOrNoHand;
    }

    public bool IsMenuOn()
    {
        return isMenuOn;
    }
}
