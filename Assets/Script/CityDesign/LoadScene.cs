using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    bool isHovering = false;
    public int sceneNum;

    [SerializeField] GameObject menu; // 일시정지 메뉴
    [SerializeField] GameObject leftRayController;
    [SerializeField] GameObject rightRayController;
    [SerializeField] private OVRControllerHelper controllerHelperLeft;
    [SerializeField] private OVRControllerHelper controllerHelperRight;
    private bool isMenuOn = false;


    void Update()
    {
        //트리거 키로 버튼 누르기
        if (isHovering)
        {
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
                SceneTransform(sceneNum);
        }
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
    }
    //버튼을 누르면 씬을 불러오는 메소드
    void SceneTransform(int sceneNum)
    {
        SceneManager.LoadScene(sceneNum);
    }

    public void IsHovering(int n)
    {
        isHovering = !isHovering;
        sceneNum = n;
    }

    public void RestartScene()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(1);
    }

    public void EndGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 어플리케이션 종료
#endif
    }

    public void LoadTitle()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Title");
    }

    private void OpenMenu()
    {
        isMenuOn = true;
        menu.SetActive(true);
        controllerHelperLeft.m_showState = OVRInput.InputDeviceShowState.ControllerInHandOrNoHand;
        controllerHelperRight.m_showState = OVRInput.InputDeviceShowState.ControllerInHandOrNoHand;
    }

    public void CloseMenu()
    {
        isMenuOn = false;
        menu.SetActive(false);
        leftRayController.SetActive(false);
        rightRayController.SetActive(false);

    }
}
