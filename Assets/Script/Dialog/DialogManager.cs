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
    bool skip = false;

    private bool gameStart = false;

    void Awake()
    {
        DialogParser theParser = GetComponent<DialogParser>();
        Dialogue[] dialogues = theParser.Parse("MyLittleAnsan_Dialog");
        int sceneNum = SceneManager.GetActiveScene().buildIndex;

        for (int i = 0; i < dialogues.Length; i++)
        {
            dialogueDic.Add(i + 1, dialogues[i]);
        }
        isFinish = true;

        isDialogue = true;
        dialogUI.SetActive(true);

        //ó�� ������ ��
        if (!System.Convert.ToBoolean(PlayerPrefs.GetInt("Token")))
        {
            ShowDialogue(GetDialogue(sceneNum));
        }
        else
        {
            //��ū�� �����ϰ�, ���õ����� ���� ���
            if (sceneNum == 1)
            {
                ShowDialogue(GetDialogue(5));
            }
                
            //ü�� Ŭ���� �� ����� �� ��� ���
            else
            {
                ShowDialogue(GetDialogue(sceneNum));
            }
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

            nextButton.SetActive(true);

            //���� ���� �Ѿ ���
            if (isNext)
            {
                //��� �ѱ�� ��ư ������ ���
                if (skip)
                {
                    skip = false;
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
                            int sceneNum = SceneManager.GetActiveScene().buildIndex;
                            //��ū�� �����ϰ�, ���õ����� ���̸� �Ǽ� �Ŵ��� Ȱ��ȭ
                            if (sceneNum == 1 && System.Convert.ToBoolean(PlayerPrefs.GetInt("Token"))) 
                                buildManualUI.SetActive(true);
                            else 
                                ManualUI.SetActive(true);
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
        gameStart = true;
        nextButton.SetActive(false);
    }

    public void ShowDialogue(Dialogue[] p_dialogues)
    {
        dialogText.text = "";

        dialogues = p_dialogues;

        StartCoroutine(TypeWriter());
    }

    IEnumerator TypeWriter()//�ؽ�Ʈ�� ����ϴ� �ڷ�ƾ
    {
        string t_ReplaceText = dialogues[lineCount].contexts[contextCount];
        for (int i = 0; i < t_ReplaceText.Length; i++)
        {
            //��ŵ ��ư ���� �� ���� ����
            if (skip) break;

            dialogText.text += t_ReplaceText[i];
            //���� ��� �� ������
            yield return new WaitForSeconds(textDelay);
        }
        isNext = true;
        yield return null;
    }

    //��ư�� ������ ���� ����
    public void skipLine()
    {
        skip = true;
        dialogText.text = "";
        GetComponent<AudioSource>().Play();
    }

    public bool SendOnDialog()
    {
        return isDialogue;
    }

    public void CloseManualUI()
    {
        ManualUI.SetActive(false);
        //rayInteraction ��Ȱ��ȭ
        leftRayController.SetActive(false);
        rightRayController.SetActive(false);

        GetComponent<AudioSource>().Play();
    }

    public void ShowSelectButtonUI()
    {
        EndDialogue();
        dialogUI.SetActive(true);
        nextButton.SetActive(false);
        dialogText.text = "�ٽ� ��ư�� ���� ���� ü���� �����ϼ���";
        buildManualUI.SetActive(false);
    }
    public void SetClearUI()
    {
        buildManualUI.SetActive(false);
        dialogUI.SetActive(true);
        nextButton.SetActive(false);
        dialogText.text = "�����մϴ�! ������ �Ȼ�ð� �ϼ��Ǿ����!";
    }
    public bool SendStart()
    {
        return gameStart;
    }

}
