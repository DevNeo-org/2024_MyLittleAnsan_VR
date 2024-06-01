using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    // 일시정지 메뉴
    [SerializeField] GameObject menu;
    //컨트롤러
    [SerializeField] GameObject leftRayController;
    [SerializeField] GameObject rightRayController;
    [SerializeField] private OVRControllerHelper controllerHelperLeft;
    [SerializeField] private OVRControllerHelper controllerHelperRight;
    private bool isMenuOn = false;


    void Update()
    {
        //메뉴 온오프
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

        if (System.Convert.ToBoolean(PlayerPrefs.GetInt("GameClear")))
        {
            rightRayController.SetActive(true);
            leftRayController.SetActive(true);
            isMenuOn = true;
            menu.SetActive(true);
            controllerHelperLeft.m_showState = OVRInput.InputDeviceShowState.ControllerInHandOrNoHand;
            controllerHelperRight.m_showState = OVRInput.InputDeviceShowState.ControllerInHandOrNoHand;
        }
    }
    
    //씬 재시작
    public void RestartScene()
    {
        Time.timeScale = 1;
        int sceneNum = SceneManager.GetActiveScene().buildIndex;
        if(sceneNum == 1)
            PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(sceneNum);
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

    //타이틀 씬 로드
    public void LoadTitle()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Title");
    }

    //메뉴 활성화
    private void OpenMenu()
    {
        Time.timeScale = 0;
        isMenuOn = true;
        menu.SetActive(true);
        controllerHelperLeft.m_showState = OVRInput.InputDeviceShowState.ControllerInHandOrNoHand;
        controllerHelperRight.m_showState = OVRInput.InputDeviceShowState.ControllerInHandOrNoHand;
    }

    //메뉴 비활성화
    public void CloseMenu()
    {
        Time.timeScale = 1;
        isMenuOn = false;
        menu.SetActive(false);
        leftRayController.SetActive(false);
        rightRayController.SetActive(false);
    }

}
