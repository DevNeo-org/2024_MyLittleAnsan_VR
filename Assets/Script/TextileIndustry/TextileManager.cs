using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class TextileManager : MonoBehaviour
{
    [SerializeField] GameObject menu; // �Ͻ����� �޴�
    [SerializeField] GameObject resultMenu; // ���â
    [SerializeField] TextMeshPro timeText;  // Ÿ�̸�
    [SerializeField] GameObject leftRayController;  // ���� ray
    [SerializeField] GameObject rightRayController; // ������ ray
    [SerializeField] private OVRControllerHelper controllerHelperLeft;  // ���� ��Ʈ�ѷ�
    [SerializeField] private OVRControllerHelper controllerHelperRight; // ������ ��Ʈ�ѷ�
    [SerializeField] GameObject paintGun;   // ����Ʈ�� �� ������
    [SerializeField] GameObject line;       // ���ؼ� 
    [SerializeField] private GameObject closeButton;    // �޴� �ݱ� ��ư
    [SerializeField] private GameObject timerUI;        // Ÿ�̸� UI
    [SerializeField] ParticleSystem shine;              // ��ĥ �׵θ� ȿ��
    [SerializeField] private GameObject[] celebrates;   // ���� ���� ��ƼŬ
    [SerializeField] private GameObject[] buckets;      // �� ���� ����Ʈ��

    Timer timer;
    DataManager dataManager;
    Gun gun;
    Clothes clothes;
    private bool isMenuOn = false;
    private bool gameEnd = false; // ���� �ð� ���� ���� Ȯ��
    private bool isMenualClosed = false;


    private void Start()
    {
        dataManager = FindAnyObjectByType<DataManager>();
        clothes = FindAnyObjectByType<Clothes>();
        gun = FindAnyObjectByType<Gun>();

        // ���� �ʱ� ����
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
        if (gameEnd) // �ð� ���� ù Ȯ�� �� ����
        {
            dataManager.SetClear();
            gun.EndGame();

            // ���� ���� ��ƼŬ ����
            for (int i=0; i < celebrates.Length; i++)
            {
                ParticleSystem p = celebrates[i].GetComponent<ParticleSystem>();
                p.Play();
            }

            // �� ���� ����Ʈ�� �ڷ� �̵�
            for (int i=0; buckets.Length > i; i++)
            {
                Animator anim = buckets[i].GetComponent<Animator>();
                anim.SetBool("isClear", true);
            }

            // ���� ����
            clothes.PlayAnim();
            GetComponent<AudioSource>().Play();
            timerUI.gameObject.SetActive(false);
            closeButton.gameObject.SetActive(false);
            shine.Stop();
            rightRayController.SetActive(true);
            Invoke("OpenResultMenu", 3f);
            Invoke("OpenMenu", 3f);
        }

        // �޴� ���� ��ư ����
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

    // Ÿ��Ʋ �� �ε�
    public void LoadTitle()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Title");
    }

    // ���õ����� �� �ε�
    public void LoadCityScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("CityDesign");
    }

    // ���� �� �����
    public void RestartScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("TextileIndScene");
    }

    //���� ����
    public void EndGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // ���ø����̼� ����
#endif
    }

    // �޴� �ݱ�
    public void CloseMenu()
    {
        Time.timeScale = 1;
        isMenuOn = false;
        menu.SetActive(false); 

        // �޴����� �̹� ������ ���
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

    // �޴� ����
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

    // ���â ����
    private void OpenResultMenu()
    {
        resultMenu.SetActive(true);
    }

    // �޴� �����̴��� ����
    public bool IsMenuOn()
    {
        return isMenuOn;
    }

    // �޴��� �ݱ�
    public void EndManual()
    {
        // ���� ���� ����
        isMenualClosed = true;
        paintGun.gameObject.SetActive(true);
        line.gameObject.SetActive(true);
        controllerHelperLeft.m_showState = OVRInput.InputDeviceShowState.ControllerNotInHand;
        controllerHelperRight.m_showState = OVRInput.InputDeviceShowState.ControllerNotInHand;
        timer.StartGame();
        gun.StartGame();
    }
}
