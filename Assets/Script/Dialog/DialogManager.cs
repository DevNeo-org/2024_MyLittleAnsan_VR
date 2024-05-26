using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class DialogManager : MonoBehaviour
{
    public static DatabaseManager instance;
    Dictionary<int, Dialogue> dialogueDic = new Dictionary<int, Dialogue>();

    public static bool isFinish = false;

    [SerializeField] DialogueEvent dialogue;
    //��ȭâ
    [SerializeField] GameObject dialogUI;
    //�ؽ�Ʈ
    [SerializeField] TextMeshProUGUI dialogText;
    //�ؽ�Ʈ ��� ������
    [SerializeField] float textDelay;
    //�ȳ� UI
    [SerializeField] GameObject ManualUI;
    [SerializeField] GameObject buildManualUI;
    [SerializeField] GameObject nextButton;
    //ray��Ʈ�ѷ�
    [SerializeField] GameObject leftRayController;
    [SerializeField] GameObject rightRayController;

    Dialogue[] dialogues;
    bool isNext = false;
    int lineCount = 0;//��ȭ ī��Ʈ
    int contextCount = 0; //��� ī��Ʈ
    public GameObject dialog;
    bool isDialogue = false;
    bool isTyping = false;
    private bool gamestart = false;

    void Awake()
    {
        DialogParser theParser = GetComponent<DialogParser>();
        Dialogue[] dialogues = theParser.Parse("MyLittleAnsan_Dialog");
        for (int i = 0; i < dialogues.Length; i++)
        {
            dialogueDic.Add(i + 1, dialogues[i]);
        }
        isFinish = true;

        //ó�� ������ ��
        if (!System.Convert.ToBoolean(PlayerPrefs.GetInt("Token")))
        {
            int sceneNum = SceneManager.GetActiveScene().buildIndex;
            dialogUI.SetActive(true);
            isDialogue = true;
            ShowDialogue(GetDialogue(sceneNum));
        }
        else
        {
            int sceneNum = SceneManager.GetActiveScene().buildIndex;
            if (sceneNum == 1)
                buildManualUI.SetActive(true);
        }

        
    }
    void Update()
    {
        //��ȭ ������ Ȯ��
        if (isDialogue)
        {
            //rayInteraction Ȱ��ȭ
            leftRayController.SetActive(true);
            rightRayController.SetActive(true);

            //���� ���� �Ѿ ���
            if (isNext)
            {
                if (!isTyping)
                {
                    isNext = false;
                    //��ȭ �ؽ�Ʈ ����
                    dialogText.text = "";
                    //��簡 �� �������� ���
                    if (++contextCount < dialogues[lineCount].contexts.Length)
                    {
                        StartCoroutine(TypeWriter());//Ÿ���� ȿ�� ����
                    }
                    else//��簡 ������ ���
                    {
                        contextCount = 0;
                        if (++lineCount < dialogues.Length)//��ȭ�� �������� ���
                        {
                            StartCoroutine(TypeWriter());//���� ��ȭ ���
                        }
                        else//��ȭ�� ��� ������ ���
                        {
                            EndDialogue();
                            ShowManualUI();
                        }
                    }
                
                }
            }
        }
    }

    public Dialogue[] GetDialogue(int id)
    {
        List<Dialogue> dialogueList = new List<Dialogue>();
        for (int i = 1; i < dialogueDic.Count; i++)
        {
            if (dialogueDic[i].id == id)
                dialogueList.Add(dialogueDic[i]);
        }
        return dialogueList.ToArray();
    }


    void EndDialogue()
    {
        dialogUI.SetActive(false);
        isDialogue = false;
        contextCount = 0;
        lineCount = 0;
        dialogues = null;
        isNext = false;
        gamestart = true;
    }

    public void ShowDialogue(Dialogue[] p_dialogues)
    {
        dialogText.text = "";

        dialogues = p_dialogues;

        StartCoroutine(TypeWriter());
    }

    IEnumerator TypeWriter()//�ؽ�Ʈ�� ����ϴ� �ڷ�ƾ
    {
        isTyping = true;
        string t_ReplaceText = dialogues[lineCount].contexts[contextCount];
        for (int i = 0; i < t_ReplaceText.Length; i++)
        {
            dialogText.text += t_ReplaceText[i];
            //���� ��� �� ������
            yield return new WaitForSeconds(textDelay);
        }
        nextButton.SetActive(true);
        isTyping = false;
        yield return null;
    }

    //��ư�� ������ ���� ����
    public void skipLine()
    {
        isNext = true;
        nextButton.SetActive(false);
    }
    public bool SendOnDialog()
    {
        return isDialogue;
    }


    public void ShowManualUI()
    {
        ManualUI.SetActive(true);
    }
    public void CloseManualUI()
    {
        ManualUI.SetActive(false);
        //rayInteraction ��Ȱ��ȭ
        leftRayController.SetActive(false);
        rightRayController.SetActive(false);
    }

    public void ShowSelectButtonUI()
    {
        dialogUI.SetActive(true);
        nextButton.SetActive(false);
        dialogText.text = "��ư�� ���� ü���� �����ϼ���";
        buildManualUI.SetActive(false);
    }
    public void SetClearUI()
    {
        buildManualUI.SetActive(false);
        dialogUI.SetActive(true);
        nextButton.SetActive(false);
        dialogText.text = "���� Ŭ����!";
    }
    public bool SendStart()
    {
        return gamestart;
    }

}
