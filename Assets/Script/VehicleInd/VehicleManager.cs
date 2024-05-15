using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VehicleManager : MonoBehaviour
{
    [SerializeField] GameObject menu;
    [SerializeField] GameObject leftRayController;
    [SerializeField] GameObject rightRayController;
    [SerializeField] private OVRControllerHelper controllerHelperLeft;
    [SerializeField] private OVRControllerHelper controllerHelperRight;
    [SerializeField] private GameObject leftWrench;
    [SerializeField] private GameObject rightWrench;

    HitboxSpawner spawner;
    private int score;
    private bool isMenuOn = false;
    DataManager dataManager;
    void Start()
    {
        score = 0;
        dataManager = FindAnyObjectByType<DataManager>();
        menu.SetActive(false);
        spawner = FindAnyObjectByType<HitboxSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMenuOn)
        {
            if (OVRInput.GetDown(OVRInput.Button.One) ||  OVRInput.GetDown(OVRInput.Button.Three))
            {
                rightRayController.SetActive(true);
            }
            else if (OVRInput.GetDown(OVRInput.Button.Two) || OVRInput.GetDown(OVRInput.Button.Four))
            {
                leftRayController.SetActive(true);
            }
            OpenMenu();
            
        }
        if (score > 10)
        {
            dataManager.SetClear();
            //carClear = true;
        }
    }
    public void ScorePlus()
    {
        score++;
    }
    public int GetScore()
    {
        return score;
    }
    public void LoadScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Title");
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
        Time.timeScale = 0;
        isMenuOn = true;
        menu.SetActive(true); 
        leftWrench.gameObject.SetActive(false);
        rightWrench.gameObject.SetActive(false);
        controllerHelperLeft.m_showState = OVRInput.InputDeviceShowState.ControllerInHandOrNoHand;
        controllerHelperRight.m_showState = OVRInput.InputDeviceShowState.ControllerInHandOrNoHand;
    }
}
