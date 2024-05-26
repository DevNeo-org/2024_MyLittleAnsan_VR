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
    //대화창
    [SerializeField] GameObject dialogUI;
    //텍스트
    [SerializeField] TextMeshProUGUI dialogText;
    //텍스트 출력 딜레이
    [SerializeField] float textDelay;
    //안내 UI
    [SerializeField] GameObject ManualUI;
    [SerializeField] GameObject buildManualUI;
    [SerializeField] GameObject nextButton;
    //ray컨트롤러
    [SerializeField] GameObject leftRayController;
    [SerializeField] GameObject rightRayController;

    Dialogue[] dialogues;
    bool isNext = false;
    int lineCount = 0;//대화 카운트
    int contextCount = 0; //대사 카운트
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

        //처음 시작일 때
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
        //대화 중인지 확인
        if (isDialogue)
        {
            //rayInteraction 활성화
            leftRayController.SetActive(true);
            rightRayController.SetActive(true);

            //다음 대사로 넘어갈 경우
            if (isNext)
            {
                if (!isTyping)
                {
                    isNext = false;
                    //대화 텍스트 비우기
                    dialogText.text = "";
                    //대사가 더 남아있을 경우
                    if (++contextCount < dialogues[lineCount].contexts.Length)
                    {
                        StartCoroutine(TypeWriter());//타이핑 효과 실행
                    }
                    else//대사가 끝났을 경우
                    {
                        contextCount = 0;
                        if (++lineCount < dialogues.Length)//대화가 남아있을 경우
                        {
                            StartCoroutine(TypeWriter());//다음 대화 출력
                        }
                        else//대화가 모두 끝났을 경우
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

    IEnumerator TypeWriter()//텍스트를 출력하는 코루틴
    {
        isTyping = true;
        string t_ReplaceText = dialogues[lineCount].contexts[contextCount];
        for (int i = 0; i < t_ReplaceText.Length; i++)
        {
            dialogText.text += t_ReplaceText[i];
            //글자 출력 간 딜레이
            yield return new WaitForSeconds(textDelay);
        }
        nextButton.SetActive(true);
        isTyping = false;
        yield return null;
    }

    //버튼을 누르면 다음 대사로
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
        //rayInteraction 비활성화
        leftRayController.SetActive(false);
        rightRayController.SetActive(false);
    }

    public void ShowSelectButtonUI()
    {
        dialogUI.SetActive(true);
        nextButton.SetActive(false);
        dialogText.text = "버튼을 눌러 체험을 선택하세요";
        buildManualUI.SetActive(false);
    }
    public void SetClearUI()
    {
        buildManualUI.SetActive(false);
        dialogUI.SetActive(true);
        nextButton.SetActive(false);
        dialogText.text = "게임 클리어!";
    }
    public bool SendStart()
    {
        return gamestart;
    }

}
