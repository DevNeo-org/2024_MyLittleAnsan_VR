using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ElecMenuManagement : MonoBehaviour    
{
    [SerializeField] GameObject menu; // �Ͻ����� �޴�
    [SerializeField] GameObject resultMenu; // ���â
    [SerializeField] GameObject timerUI; // Ÿ�̸�, ���� UI
    [SerializeField] GameObject leftRayController;
    [SerializeField] GameObject rightRayController;
    [SerializeField] private OVRControllerHelper controllerHelperLeft;
    [SerializeField] private OVRControllerHelper controllerHelperRight;
    [SerializeField] private GameObject leftSolder;
    [SerializeField] private GameObject rightSolder;
    [SerializeField] TextMeshPro scoreText;
    [SerializeField] GameObject celebratePrefeb;

    Timer timer;
    private int score;
    private bool isMenuOn = false;
    private bool gameEnd = false; // ���� �ð� ���� ���� Ȯ��
    DataManager datamanager;
    
   
    bool startgame = false;
    void Start()
    {
        score = 0;
        datamanager = FindAnyObjectByType<DataManager>();
        menu.SetActive(false);
        resultMenu.SetActive(false);
        scoreText.text = "";
        timer = FindAnyObjectByType<Timer>();
        
    }
    void Update()
    {
        //startgame = timer.StartGame();
        if (gameEnd) return;
        gameEnd = timer.GetBool();
        if (gameEnd) // �ð� ���� ù Ȯ�� �� ����
        {
            if (score >= 10)
            {
                datamanager.SetClear();
                resultMenu.SetActive(true);
                celebratePrefeb.GetComponent<ParticleSystem>().Play();
            }
            OpenMenu();
            
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
        scoreText.text = "����: " + score.ToString();
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
        SceneManager.LoadScene("Electronic");
    }
    public void EndGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // ���ø����̼� ����
#endif
    }
    public void CloseMenu()
    {
        timerUI.SetActive(true);
        Time.timeScale = 1;
        isMenuOn = false;
        menu.SetActive(false);
        if (startgame)
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
        leftRayController.SetActive(true);
        rightRayController.SetActive(true);
        leftSolder.gameObject.SetActive(false);
        rightSolder.gameObject.SetActive(false);
       
    }

    public void StartGame()
    {
        startgame = true;
    }



}