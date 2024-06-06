using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    // �Ͻ����� �޴�
    [SerializeField] GameObject menu;

    public GameObject gameManager;

    //��Ʈ�ѷ�
    [SerializeField] GameObject leftRayController;
    [SerializeField] GameObject rightRayController;
    [SerializeField] private OVRControllerHelper controllerHelperLeft;
    [SerializeField] private OVRControllerHelper controllerHelperRight;
    private bool isMenuOn = false;


    void Update()
    {
        //�޴� �¿���
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
    
    //�� �����
    public void RestartScene()
    {
        gameManager.GetComponent<AudioManager>().PlaySound(3);
        Time.timeScale = 1;
        int sceneNum = SceneManager.GetActiveScene().buildIndex;
        if(sceneNum == 1)
            PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(sceneNum);

    }

    //���� ����
    public void EndGame()
    {
        gameManager.GetComponent<AudioManager>().PlaySound(3);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // ���ø����̼� ����
#endif
    }

    //Ÿ��Ʋ �� �ε�
    public void LoadTitle()
    {
        gameManager.GetComponent<AudioManager>().PlaySound(3);
        Time.timeScale = 1;
        SceneManager.LoadScene("Title");
    }

    //�޴� Ȱ��ȭ
    private void OpenMenu()
    {
        gameManager.GetComponent<AudioManager>().PlaySound(3);
        Time.timeScale = 0;
        isMenuOn = true;
        menu.SetActive(true);
        controllerHelperLeft.m_showState = OVRInput.InputDeviceShowState.ControllerInHandOrNoHand;
        controllerHelperRight.m_showState = OVRInput.InputDeviceShowState.ControllerInHandOrNoHand;
    }

    //�޴� ��Ȱ��ȭ
    public void CloseMenu()
    {
        gameManager.GetComponent<AudioManager>().PlaySound(3);
        Time.timeScale = 1;
        isMenuOn = false;
        menu.SetActive(false);
        leftRayController.SetActive(false);
        rightRayController.SetActive(false);
    }

}
