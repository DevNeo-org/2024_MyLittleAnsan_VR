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
    [SerializeField] GameObject selectButtonUI_1;
    [SerializeField] GameObject buildModUI;
    [SerializeField] GameObject selectButtonUI_2;
    [SerializeField] GameObject gameClearUI;

    Dialogue[] dialogues;
    bool isNext = false;
    int lineCount = 0;//��ȭ ī��Ʈ
    int contextCount = 0; //��� ī��Ʈ
    public GameObject dialog;
    bool isDialogue = false;
    bool isTyping = false;

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
                buildModUI.SetActive(true);
        }

        
    }
    void Update()
    {
        //��ȭ ������ Ȯ��
        if (isDialogue)
        {
            //���� ���� �Ѿ ���
            if (isNext)
            {
                if (!isTyping)
                {
                    isTyping = true;
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
                            //dialogText.text = "�տ� �ִ� ��ư�� ����������";
                            EndDialogue();
                            ShowHowSelectButton();
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
        //t_ReplaceText = t_ReplaceText.Replace("'", ","); //'�� ,�� ġȯ
        for (int i = 0; i < t_ReplaceText.Length; i++)
        {
            dialogText.text += t_ReplaceText[i];
            //���� ��� �� ������
            yield return new WaitForSeconds(textDelay);
        }
        //��簡 ������ ���� ����
        isNext = true;
        isTyping = false;

        yield return null;
    }

    public void ShowHowSelectButton()
    {
        selectButtonUI_1.SetActive(true);
    }

    public void ActiveUI()
    {
        selectButtonUI_2.SetActive(true);
        buildModUI.SetActive(false);
    }
    public void SetClearUI()
    {
        gameClearUI.SetActive(true);
        buildModUI.SetActive(false);
        selectButtonUI_2.SetActive(false);
    }

}
