using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VehicleManager : MonoBehaviour
{
    [SerializeField] GameObject menu; // 일시정지 메뉴
    [SerializeField] GameObject resultMenu; // 결과창
    [SerializeField] GameObject timerUI; // 타이머, 점수 UI
    [SerializeField] GameObject leftRayController;
    [SerializeField] GameObject rightRayController;
    [SerializeField] private OVRControllerHelper controllerHelperLeft;
    [SerializeField] private OVRControllerHelper controllerHelperRight;
    [SerializeField] private GameObject leftWrench;
    [SerializeField] private GameObject rightWrench;
    [SerializeField] TextMeshPro scoreText;

    Timer timer;
    HitboxSpawner spawner;
    private int score;
    private bool isMenuOn = false;
    private bool gameEnd = false; // 게임 시간 종료 여부 확인
    DataManager dataManager;
    void Start()
    {
        score = 0;
        dataManager = FindAnyObjectByType<DataManager>();
        menu.SetActive(false);
        resultMenu.SetActive(false);
        spawner = FindAnyObjectByType<HitboxSpawner>();
        timer = FindAnyObjectByType<Timer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameEnd) return;
        gameEnd = timer.GetBool();
        if (gameEnd) // 시간 종료 첫 확인 시 실행
        {
            if (score >= 10)
            {
                dataManager.SetClear();
                resultMenu.SetActive(true);
            }
            OpenMenu();
            menu.transform.GetChild(0).GetChild(1).GetChild(0).GetChild(0).gameObject.SetActive(false); // 일시정지 메뉴의 CloseButton 제거
            rightRayController.SetActive(true);
        }
        else if (score >= 20) // 20점 획득 시 종료
        {
            dataManager.SetClear();
            resultMenu.SetActive(true);
            OpenMenu();
            menu.transform.GetChild(0).GetChild(1).GetChild(0).GetChild(0).gameObject.SetActive(false); // 일시정지 메뉴의 CloseButton 제거
            rightRayController.SetActive(true);
            gameEnd = true;
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
    public void ScorePlus()
    {
        score++;
        scoreText.text = "점수: " + score.ToString();
    }
    public int GetScore()
    {
        return score;
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
        SceneManager.LoadScene("AutomobIndScene");
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
        timerUI.SetActive(true);
        Time.timeScale = 1;
        isMenuOn = false;
        menu.SetActive(false); leftRayController.SetActive(false); rightRayController.SetActive(false);
        if (spawner.leftWrenchOn)
        {
            leftWrench.gameObject.SetActive(true);
            controllerHelperLeft.m_showState = OVRInput.InputDeviceShowState.ControllerNotInHand;
        }
        if(spawner.rightWrenchOn)
        {
            rightWrench.gameObject.SetActive(true);
            controllerHelperRight.m_showState = OVRInput.InputDeviceShowState.ControllerNotInHand;
        }
    }
    private void OpenMenu()
    {
        timerUI.SetActive(false);
        Time.timeScale = 0;
        isMenuOn = true;
        menu.SetActive(true); 
        leftWrench.gameObject.SetActive(false);
        rightWrench.gameObject.SetActive(false);
        controllerHelperLeft.m_showState = OVRInput.InputDeviceShowState.ControllerInHandOrNoHand;
        controllerHelperRight.m_showState = OVRInput.InputDeviceShowState.ControllerInHandOrNoHand;
    }
}
