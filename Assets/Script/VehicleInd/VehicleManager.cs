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
    [SerializeField] GameObject wrenchObjects;
    [SerializeField] GameObject celebrate;

    Timer timer;
    HitboxSpawner spawner;
    private int score;
    private bool isMenuOn = false;
    private bool gameEnd = false; // 게임 시간 종료 여부 확인
    private bool isDialogEnd = false; // 다이얼로그 종료 여부 확인
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
                dataManager.SetClear(); // 결과 완료 시 토큰 완료 설정
                resultMenu.SetActive(true);
            }
            OpenMenu();
            menu.transform.GetChild(0).GetChild(1).GetChild(0).GetChild(0).gameObject.SetActive(false); // 일시정지 메뉴의 CloseButton 제거
            rightRayController.SetActive(true);
        }
        else if (score >= 20) // 20점 획득 시 종료
        {
            gameEnd = true;
            celebrate.GetComponent<ParticleSystem>().Play(); // 결과창 직전 실행
            GetComponent<AudioSource>().Play();
            Invoke("GameClear", 1f);
        }
        if (!isMenuOn)
        {
            if (OVRInput.GetDown(OVRInput.Button.One) || OVRInput.GetDown(OVRInput.Button.Two)) // 우측 컨트롤러의 A,B 버튼
            {
                OpenMenu();
                rightRayController.SetActive(true);
            }
            else if (OVRInput.GetDown(OVRInput.Button.Three) || OVRInput.GetDown(OVRInput.Button.Four)) // 좌측 컨트롤러의 X,Y 버튼
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
    public void LoadTitle() // 메인메뉴 로드
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Title");
    }
    public void LoadCityScene() // 도시디자인 씬 로드
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("CityDesign");
    }
    public void RestartScene() // 씬 재시작
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("AutomobIndScene");
    }
    public void EndGame() // 게임 종료
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 어플리케이션 종료
#endif
    }
    public void CloseMenu() // 메뉴 닫기(일시정지 종료)
    {
        timerUI.SetActive(true);
        Time.timeScale = 1;
        isMenuOn = false;
        menu.SetActive(false); leftRayController.SetActive(false); rightRayController.SetActive(false);
        if (!isDialogEnd) { leftRayController.SetActive(true); rightRayController.SetActive(true); } // 다이얼로그 종료 전에는 레이컨트롤러 유지
        if (spawner.leftWrenchOn) // 이전에 렌치가 활성화 되어있는지 (좌측)
        {
            leftWrench.gameObject.SetActive(true);
            controllerHelperLeft.m_showState = OVRInput.InputDeviceShowState.ControllerNotInHand;
        }
        if(spawner.rightWrenchOn) // 이전에 렌치가 활성화 되어있는지 (우측)
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
        controllerHelperLeft.m_showState = OVRInput.InputDeviceShowState.ControllerInHandOrNoHand; // 컨트롤러 그래픽 활성화
        controllerHelperRight.m_showState = OVRInput.InputDeviceShowState.ControllerInHandOrNoHand;
    }
    private void GameClear()
    {
        celebrate.gameObject.SetActive(false);
        dataManager.SetClear();
        resultMenu.SetActive(true);
        OpenMenu();
        menu.transform.GetChild(0).GetChild(1).GetChild(0).GetChild(0).gameObject.SetActive(false); // 일시정지 메뉴의 CloseButton 제거
        rightRayController.SetActive(true);
    }
    public bool IsRayControllerOn()
    {
        return leftRayController.activeSelf || rightRayController.activeSelf;
    }
    public void DialogEnd()
    {
        isDialogEnd = true;
        wrenchObjects.SetActive(true);
    }
    public void PlayConfetti()
    {
        celebrate.GetComponent<ParticleSystem>().Play(); // 결과창 직전 실행
    }
}
