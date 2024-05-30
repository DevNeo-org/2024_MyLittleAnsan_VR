using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ElecMenuManagement : MonoBehaviour    
{
    [SerializeField] GameObject menu; // 일시정지 메뉴
    [SerializeField] GameObject resultMenu; // 결과창
    [SerializeField] GameObject timerUI; // 타이머, 점수 UI
    [SerializeField] GameObject leftRayController;
    [SerializeField] GameObject rightRayController;
    [SerializeField] private OVRControllerHelper controllerHelperLeft;
    [SerializeField] private OVRControllerHelper controllerHelperRight;
    [SerializeField] private GameObject leftSolder;
    [SerializeField] private GameObject rightSolder;
    [SerializeField] TextMeshPro scoreText;

    Timer timer;
    DialogManager dialogManager;
    private int score;
    private bool isMenuOn = false;
    private bool gameEnd = false; // 게임 시간 종료 여부 확인
    DataManager datamanager;
    
    private bool dialogOn = true;
    void Start()
    {
        score = 0;
        datamanager = FindAnyObjectByType<DataManager>();
        menu.SetActive(false);
        resultMenu.SetActive(false);
        dialogManager = FindAnyObjectByType<DialogManager>();
        timer = FindAnyObjectByType<Timer>();
       
        
    }
    void Update()
    {
        dialogOn = dialogManager.SendOnDialog();
        if (gameEnd) return;
        gameEnd = timer.GetBool();
        if (gameEnd) // 시간 종료 첫 확인 시 실행
        {
            if (score >= 10)
            {
                datamanager.SetClear();
                resultMenu.SetActive(true);
            }
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
        //토큰 초기화
        PlayerPrefs.SetInt("Token", System.Convert.ToInt16(false));
        Time.timeScale = 1;
        SceneManager.LoadScene("Electronic");
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
        menu.SetActive(false);
        if (!dialogOn)
        {
            leftRayController.SetActive(false);
            rightRayController.SetActive(false);
        }
        leftSolder.gameObject.SetActive(true);
        rightSolder.gameObject.SetActive(true);
        
    }
    private void OpenMenu()
    {
        timerUI.SetActive(false);
        Time.timeScale = 0;
        isMenuOn = true;
        menu.SetActive(true);
        leftSolder.gameObject.SetActive(false);
        rightSolder.gameObject.SetActive(false);
       
    }

   



}