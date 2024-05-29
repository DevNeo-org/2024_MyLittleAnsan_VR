using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VehicleManager : MonoBehaviour
{
    [SerializeField] GameObject menu; // �Ͻ����� �޴�
    [SerializeField] GameObject resultMenu; // ���â
    [SerializeField] GameObject timerUI; // Ÿ�̸�, ���� UI
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
    private bool gameEnd = false; // ���� �ð� ���� ���� Ȯ��
    private bool isDialogEnd = false; // ���̾�α� ���� ���� Ȯ��
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
        if (gameEnd) // �ð� ���� ù Ȯ�� �� ����
        {
            if (score >= 10)
            {
                dataManager.SetClear(); // ��� �Ϸ� �� ��ū �Ϸ� ����
                resultMenu.SetActive(true);
            }
            OpenMenu();
            menu.transform.GetChild(0).GetChild(1).GetChild(0).GetChild(0).gameObject.SetActive(false); // �Ͻ����� �޴��� CloseButton ����
            rightRayController.SetActive(true);
        }
        else if (score >= 20) // 20�� ȹ�� �� ����
        {
            gameEnd = true;
            celebrate.GetComponent<ParticleSystem>().Play(); // ���â ���� ����
            GetComponent<AudioSource>().Play();
            Invoke("GameClear", 1f);
        }
        if (!isMenuOn)
        {
            if (OVRInput.GetDown(OVRInput.Button.One) || OVRInput.GetDown(OVRInput.Button.Two)) // ���� ��Ʈ�ѷ��� A,B ��ư
            {
                OpenMenu();
                rightRayController.SetActive(true);
            }
            else if (OVRInput.GetDown(OVRInput.Button.Three) || OVRInput.GetDown(OVRInput.Button.Four)) // ���� ��Ʈ�ѷ��� X,Y ��ư
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
    public void LoadTitle() // ���θ޴� �ε�
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Title");
    }
    public void LoadCityScene() // ���õ����� �� �ε�
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("CityDesign");
    }
    public void RestartScene() // �� �����
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("AutomobIndScene");
    }
    public void EndGame() // ���� ����
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // ���ø����̼� ����
#endif
    }
    public void CloseMenu() // �޴� �ݱ�(�Ͻ����� ����)
    {
        timerUI.SetActive(true);
        Time.timeScale = 1;
        isMenuOn = false;
        menu.SetActive(false); leftRayController.SetActive(false); rightRayController.SetActive(false);
        if (!isDialogEnd) { leftRayController.SetActive(true); rightRayController.SetActive(true); } // ���̾�α� ���� ������ ������Ʈ�ѷ� ����
        if (spawner.leftWrenchOn) // ������ ��ġ�� Ȱ��ȭ �Ǿ��ִ��� (����)
        {
            leftWrench.gameObject.SetActive(true);
            controllerHelperLeft.m_showState = OVRInput.InputDeviceShowState.ControllerNotInHand;
        }
        if(spawner.rightWrenchOn) // ������ ��ġ�� Ȱ��ȭ �Ǿ��ִ��� (����)
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
        controllerHelperLeft.m_showState = OVRInput.InputDeviceShowState.ControllerInHandOrNoHand; // ��Ʈ�ѷ� �׷��� Ȱ��ȭ
        controllerHelperRight.m_showState = OVRInput.InputDeviceShowState.ControllerInHandOrNoHand;
    }
    private void GameClear()
    {
        celebrate.gameObject.SetActive(false);
        dataManager.SetClear();
        resultMenu.SetActive(true);
        OpenMenu();
        menu.transform.GetChild(0).GetChild(1).GetChild(0).GetChild(0).gameObject.SetActive(false); // �Ͻ����� �޴��� CloseButton ����
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
        celebrate.GetComponent<ParticleSystem>().Play(); // ���â ���� ����
    }
}
