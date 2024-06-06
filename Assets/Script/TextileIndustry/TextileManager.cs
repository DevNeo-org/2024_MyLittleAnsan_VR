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
    [SerializeField] TextMeshPro timeText;  // 타이머
    [SerializeField] GameObject leftRayController;  // 왼쪽 ray
    [SerializeField] GameObject rightRayController; // 오른쪽 ray
    [SerializeField] private OVRControllerHelper controllerHelperLeft;  // 왼쪽 컨트롤러
    [SerializeField] private OVRControllerHelper controllerHelperRight; // 오른쪽 컨트롤러
    [SerializeField] GameObject paintGun;   // 페인트총 모델 프리팹
    [SerializeField] GameObject line;       // 조준선 
    [SerializeField] private GameObject closeButton;    // 메뉴 닫기 버튼
    [SerializeField] private GameObject timerUI;        // 타이머 UI
    [SerializeField] ParticleSystem shine;              // 색칠 테두리 효과
    [SerializeField] private GameObject[] celebrates;   // 게임 종료 파티클
    [SerializeField] private GameObject[] buckets;      // 색 변경 페인트통

    Timer timer;
    DataManager dataManager;
    Gun gun;
    Clothes clothes;
    private bool isMenuOn = false;
    private bool gameEnd = false; // 게임 시간 종료 여부 확인
    private bool isMenualClosed = false;


    private void Start()
    {
        dataManager = FindAnyObjectByType<DataManager>();
        clothes = FindAnyObjectByType<Clothes>();
        gun = FindAnyObjectByType<Gun>();

        // 게임 초기 설정
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
            gun.EndGame();

            // 게임 종료 파티클 실행
            for (int i=0; i < celebrates.Length; i++)
            {
                ParticleSystem p = celebrates[i].GetComponent<ParticleSystem>();
                p.Play();
            }

            // 색 변경 페인트통 뒤로 이동
            for (int i=0; buckets.Length > i; i++)
            {
                Animator anim = buckets[i].GetComponent<Animator>();
                anim.SetBool("isClear", true);
            }

            // 종료 설정
            clothes.PlayAnim();
            GetComponent<AudioSource>().Play();
            timerUI.gameObject.SetActive(false);
            closeButton.gameObject.SetActive(false);
            shine.Stop();
            rightRayController.SetActive(true);
            Invoke("OpenResultMenu", 3f);
            Invoke("OpenMenu", 3f);
        }

        // 메뉴 열기 버튼 감지
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

    // 타이틀 씬 로드
    public void LoadTitle()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Title");
    }

    // 도시디자인 씬 로드
    public void LoadCityScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("CityDesign");
    }

    // 현재 씬 재실행
    public void RestartScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("TextileIndScene");
    }

    //게임 종료
    public void EndGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 어플리케이션 종료
#endif
    }

    // 메뉴 닫기
    public void CloseMenu()
    {
        Time.timeScale = 1;
        isMenuOn = false;
        menu.SetActive(false); 

        // 메뉴얼을 이미 종료한 경우
        if (isMenualClosed)
        {
            leftRayController.SetActive(false);
            rightRayController.SetActive(false);
            controllerHelperLeft.m_showState = OVRInput.InputDeviceShowState.ControllerNotInHand;
            controllerHelperRight.m_showState = OVRInput.InputDeviceShowState.ControllerNotInHand;
            paintGun.gameObject.SetActive(true);
            line.gameObject.SetActive(true);
        }
    }

    // 메뉴 열기
    private void OpenMenu()
    {
        Time.timeScale = 0;
        isMenuOn = true;
        menu.SetActive(true);
        paintGun.gameObject.SetActive(false);
        line.gameObject.SetActive(false);
        controllerHelperLeft.m_showState = OVRInput.InputDeviceShowState.ControllerInHandOrNoHand;
        controllerHelperRight.m_showState = OVRInput.InputDeviceShowState.ControllerInHandOrNoHand;
    }

    // 결과창 열기
    private void OpenResultMenu()
    {
        resultMenu.SetActive(true);
    }

    // 메뉴 열려이는지 여부
    public bool IsMenuOn()
    {
        return isMenuOn;
    }

    // 메뉴얼 닫기
    public void EndManual()
    {
        // 게임 시작 세팅
        isMenualClosed = true;
        paintGun.gameObject.SetActive(true);
        line.gameObject.SetActive(true);
        controllerHelperLeft.m_showState = OVRInput.InputDeviceShowState.ControllerNotInHand;
        controllerHelperRight.m_showState = OVRInput.InputDeviceShowState.ControllerNotInHand;
        timer.StartGame();
        gun.StartGame();
    }
}
